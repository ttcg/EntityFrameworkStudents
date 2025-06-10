using Microsoft.EntityFrameworkCore;
using Students.Repository;
using Students.Web.DataSeeding;
using Students.Web.Services.Students;
using Students.Web.Services.Teachers;
using Students.Web.Swagger;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var schemaHelper = new SwaggerSchemaHelper();
    options.CustomSchemaIds(type => schemaHelper.GetSchemaId(type));
});


var apiKey = builder.Configuration["SeedTestData"];

builder.Services.AddDbContext<StudentsDbContext>(options =>
{
    var connectionString = @"Server=(localdb)\mssqllocaldb;Database=EfCoreStudentsDb;Trusted_Connection=True;";

    options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Students.Migrations"));
});

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await TestData.SeedAsync(app, builder.Configuration);

app.Run();

public partial class Program { }