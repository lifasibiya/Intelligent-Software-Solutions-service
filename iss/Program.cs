using iss.Models;
using iss.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDocumentService, DocumentManagement>();
builder.Services.AddTransient<IUserService, UserManagement>();

var conn = builder.Configuration.GetSection("ConnectionStrings")["iss"];
builder.Services.AddDbContext<ISSDBContext>(option => option.UseSqlServer(conn));
builder.Services.AddCors(builder => builder.AddPolicy("ISSPOLICY", configurePolicy => configurePolicy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ISSPOLICY");

app.UseAuthorization();

app.MapControllers();

app.Run();
