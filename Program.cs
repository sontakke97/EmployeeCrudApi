using EmployeeCrud.Data;
using EmployeeCrud.Repositories;
using EmployeeCrud.Services;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<EmployeeContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<ExportEmployeePdfService>();

builder.Services.AddCors(options=> {
    options.AddPolicy("AllowSPecific", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();

    });
});

QuestPDF.Settings.License = LicenseType.Community;


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSPecific");
app.UseAuthorization();

app.MapControllers();

app.Run();
