using Harmonia.API.DTOs;
using Harmonia.API.Exceptions;
using Harmonia.API.Models;
using Harmonia.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Harmonia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class AutenticacaoController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AutenticacaoController(ITokenService tokenService,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<IdentityRole> roleManager,
                          IConfiguration configuration)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Cria um novo cargo no sistema.
    /// </summary>
    /// <param name="cargo">Nome do cargo que deseja criar.</param>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost("Cargo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateRole(string cargo)
    {
        var roleExists = await _roleManager.RoleExistsAsync(cargo);

        if (!roleExists)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(cargo));

            if (result.Succeeded)
            {
                return Ok(new { Status = StatusCodes.Status200OK, Title = $"Cargo {cargo} criado com sucesso." });
            }
            else
            {
                throw new BadRequestException($"Erro ao criar cargo {cargo}.");
            }
        }

        throw new BadRequestException($"Cargo {cargo} já existe.");
    }

    /// <summary>
    /// Atribui um cargo a um usuário existente.
    /// </summary>
    /// <param name="username">Nome de usuário ao qual deseja atribuir o cargo.</param>
    /// <param name="cargo">Cargo que será atribuído ao usuário.</param>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost("Atribuir")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddUserToRole(string username, string cargo)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null)
        {
            throw new NotFoundException($"Usuário {username} não encontrado.");
        }

        var roleExists = await _roleManager.RoleExistsAsync(cargo);

        if (!roleExists)
        {
            throw new NotFoundException($"Cargo {cargo} não encontrado.");
        }

        var userAlreadyInRole = await _userManager.IsInRoleAsync(user, cargo);

        if (userAlreadyInRole)
        {
            throw new BadRequestException($"Usuário {username} já possui o cargo {cargo}.");
        }

        var result = await _userManager.AddToRoleAsync(user, cargo);

        if (!result.Succeeded)
        {
            throw new BadRequestException($"Erro ao atribuir cargo {cargo} a {username}.");
        }

        return Ok(new
        {
            Status = StatusCodes.Status200OK,
            Title = $"Cargo {cargo} atribuído a {username} com sucesso."
        });
    }

    /// <summary>
    /// Realiza a autenticação do usuário e gera um token de acesso.
    /// </summary>
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Login(LoginRequestDTO model)
    {
        var user = await _userManager.FindByNameAsync(model.Username!);

        if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("id", user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo.ToLocalTime(),
            });
        }

        return Unauthorized();
    }

    /// <summary>
    /// Registra um novo usuário no sistema.
    /// </summary>
    [HttpPost("Cadastro")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Register(CadastroRequestDTO model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username!);
        var emailExists = await _userManager.FindByEmailAsync(model.Email!);

        if (userExists is not null)
        {
            throw new BadRequestException($"Usuário {model.Username} já existe.");
        }

        if (emailExists is not null)
        {
            throw new BadRequestException($"Email {model.Email} já está em uso.");
        }

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            throw new BadRequestException($"Erro ao criar usuário {model.Username}.");
        }

        return Ok(new { Status = StatusCodes.Status200OK, Title = $"Usuário {model.Username} criado com sucesso." });
    }

    /// <summary>
    /// Gera um novo token de acesso utilizando um refresh token.
    /// </summary>
    [HttpPost("Atualizar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> RefreshToken(TokenRequestDTO model)
    {
        if (model is null)
        {
            throw new BadRequestException("Requisição inválida.");
        }

        string? accessToken = model.AccessToken ?? throw new ArgumentNullException(nameof(model.AccessToken));

        string? refreshToken = model.RefreshToken ?? throw new ArgumentNullException(nameof(model.RefreshToken));

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

        if (principal is null)
        {
            throw new BadRequestException("AccessToken/RefreshToken inválido.");
        }

        string username = principal.Identity!.Name!;

        var user = await _userManager.FindByNameAsync(username!);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new BadRequestException("AccessToken/RefreshToken inválido.");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
        });
    }

    /// <summary>
    /// Revoga o refresh token de um usuário específico.
    /// </summary>
    /// <param name="username">Nome do usuário cujo refresh token será revogado.</param>
    [Authorize(Policy = "ExclusiveOnly")]
    [HttpPost("Revogar/{username}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username!);

        if (user is null)
        {
            throw new NotFoundException($"Usuário {username} não encontrado.");
        }

        user.RefreshToken = null;

        await _userManager.UpdateAsync(user);

        return NoContent();
    }
}
