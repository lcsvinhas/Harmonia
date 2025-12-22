using Asp.Versioning;
using Harmonia.API.Context;
using Harmonia.API.DTOs.Mappings;
using Harmonia.API.Filters;
using Harmonia.API.RateLimitOptions;
using Harmonia.API.Repositories;
using Harmonia.API.Services;
using Harmonia.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.PostgreSQL(
        restrictedToMinimumLevel: LogEventLevel.Warning,
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        tableName: "Logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            ["trace_id"] = new SinglePropertyColumnWriter("TraceId"),
            ["level"] = new LevelColumnWriter(),
            ["exception"] = new ExceptionColumnWriter(),
            ["method"] = new SinglePropertyColumnWriter("Method"),
            ["path"] = new SinglePropertyColumnWriter("Path"),
            ["controller"] = new SinglePropertyColumnWriter("Controller"),
            ["action"] = new SinglePropertyColumnWriter("Action"),
            ["ip"] = new SinglePropertyColumnWriter("IP"),
            ["timestamp"] = new TimestampColumnWriter()
        })
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var myOptions = new MyRateLimitOptions();

builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = myOptions.AutoReplenishment,
                PermitLimit = myOptions.PermitLimit,
                QueueLimit = myOptions.QueueLimit,
                Window = TimeSpan.FromSeconds(myOptions.Window)
            }));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Harmonia",
        Description = "Harmonia é uma API RESTful para cadastro, consulta e gerenciamento de instrumentos musicais.",
        Contact = new OpenApiContact
        {
            Name = "Lucas Vinhas",
            Email = "l.vinhas.lv@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/lucas-vinhas-/")
        }
    });
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var PostgresConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(PostgresConnection));

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IInstrumentoRepository, InstrumentoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IInstrumentoService, InstrumentoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddAutoMapper(typeof(DTOMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Harmonia v1");
    });
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
