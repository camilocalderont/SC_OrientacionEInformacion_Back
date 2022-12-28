using Aplicacion.Mapping;
using Aplicacion.Services;
using Aplicacion.Services.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using Persistencia.Repository;
using WebApi.Storage;
using WebApi.Validaciones;
using WebAPI.Midleware;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


DotNetEnv.Env.Load();

string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
builder.Services.AddDbContext<OrientacionDbContext>(options =>
                       options.UseSqlServer(connectionString),
            ServiceLifetime.Transient);


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

builder.Services.AddScoped(typeof(AtencionGrupalRepository), typeof(AtencionGrupalRepository));
builder.Services.AddScoped(typeof(AtencionWebRepository), typeof(AtencionWebRepository));
builder.Services.AddScoped(typeof(PersonaWebRepository), typeof(PersonaWebRepository));


builder.Services.AddScoped(typeof(PersonaWebService), typeof(PersonaWebService));

builder.Services.AddScoped(typeof(AtencionWebService), typeof(AtencionWebService));

builder.Services.AddScoped(typeof(ValidacionCorreo), typeof(ValidacionCorreo));

builder.Services.AddScoped(typeof(AzureStorage), typeof(AzureStorage));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);





builder.Services.AddCors(opt => {   
    opt.AddPolicy(name: myAllowSpecificOrigins,
        builder => {
            builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyMethod()
            .AllowAnyHeader();
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


