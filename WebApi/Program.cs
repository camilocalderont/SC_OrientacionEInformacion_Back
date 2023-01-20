using Aplicacion.Mapping;
using Aplicacion.Services;
using Aplicacion.Services.AtencionesGrupales;
using Aplicacion.Services.AtencionesWeb;
using Aplicacion.Services.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using Persistencia.Repository;
using WebApi.Storage;
using WebApi.Validaciones;
using WebAPI.Midleware;
using Azure.Identity;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


DotNetEnv.Env.Load();

var keyVaultUrl = $"https://{Environment.GetEnvironmentVariable("KeyVaultUrl")}.vault.azure.net/";
var clientId = Environment.GetEnvironmentVariable("ClientId");
var clientSecret = Environment.GetEnvironmentVariable("ClientSecret");
var TenantId = Environment.GetEnvironmentVariable("TenantId");
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new ClientSecretCredential(TenantId, clientId, clientSecret));

string connectionString, blobStorageConnection = "";
bool env = builder.Environment.IsDevelopment();
if (!env)
{
    connectionString = builder.Configuration.GetValue<string>("CONNECTION-STRING-OEI");
    blobStorageConnection = builder.Configuration.GetValue<string>("CONNECTION-BLOB-STORAGE");
}else{
    connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    blobStorageConnection = Environment.GetEnvironmentVariable("CONNECTION_BLOB_STORAGE");
}


var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger("Program");

logger.LogInformation("CADENA DE CONEXION BLOB:" + blobStorageConnection);
logger.LogInformation("CADENA DE CONEXION DB: " + connectionString);



Environment.SetEnvironmentVariable("CONNECTION_BLOB_STORAGE", blobStorageConnection, EnvironmentVariableTarget.Process);


builder.Services.AddDbContext<OrientacionDbContext>(options =>
                       options.UseSqlServer(connectionString),
            ServiceLifetime.Transient);


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));


builder.Services.AddScoped(typeof(AtencionGrupalService), typeof(AtencionGrupalService));
builder.Services.AddScoped(typeof(AtencionGrupalRepository), typeof(AtencionGrupalRepository));
builder.Services.AddScoped(typeof(AtencionWebRepository), typeof(AtencionWebRepository));
builder.Services.AddScoped(typeof(PersonaWebRepository), typeof(PersonaWebRepository));
builder.Services.AddScoped(typeof(PersonaRepository), typeof(PersonaRepository));
builder.Services.AddScoped(typeof(PersonaAfiliacionRepository), typeof(PersonaAfiliacionRepository));
builder.Services.AddScoped(typeof(PersonaContactoRepository), typeof(PersonaContactoRepository));
builder.Services.AddScoped(typeof(AtencionIndividualRepository), typeof(AtencionIndividualRepository));


builder.Services.AddScoped(typeof(PersonaWebService), typeof(PersonaWebService));
builder.Services.AddScoped(typeof(AtencionWebService), typeof(AtencionWebService));
builder.Services.AddScoped(typeof(ValidacionCorreo), typeof(ValidacionCorreo));
builder.Services.AddScoped(typeof(AzureStorage), typeof(AzureStorage));

builder.Services.AddScoped(typeof(PersonaService), typeof(PersonaService));
builder.Services.AddScoped(typeof(PersonaAfiliacionService), typeof(PersonaAfiliacionService));
builder.Services.AddScoped(typeof(PersonaContactoService), typeof(PersonaContactoService));
builder.Services.AddScoped(typeof(AtencionIndividualService), typeof(AtencionIndividualService));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


builder.Services.AddCors(opt => {   
    opt.AddPolicy(name: myAllowSpecificOrigins,
        builder => {
            builder
            .WithOrigins("*")
            .WithHeaders("*")
            .AllowAnyMethod();
        });
});


var app = builder.Build();
app.UseMiddleware<ExcepcionErroresMidleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OrientacionDbContext>();
    context.Database.Migrate();
}


if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();
app.Run();


