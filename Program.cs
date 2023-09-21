global using BackendApi.Models;
global using BackendApi.Context;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using BackendApi.Services;
using BackendApi.Controllers;

var builder = WebApplication.CreateBuilder(args);


/* builder.Services.AddScoped<ICursoDetalleService, CursoDetalleService>();
 builder.Services.AddScoped<ICursoService, CursoService>();
 builder.Services.AddScoped<IEstudianteService, EstudianteService>();
 builder.Services.AddScoped<IProfesorService, ProfesorService>();*/

        builder.Services.AddScoped<IProfesorService, ProfesorService>();
        builder.Services.AddScoped<ICursoService, CursoService>();
        builder.Services.AddScoped<IEstudianteService, EstudianteService>();


builder.Services.AddDbContext<DataContext>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddHttpClient();

var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    
