using academia.Application.Mappings;
using academia.Infrastructure;
using academia.Infrastructure.Persistence;
using academia.WebApi.Controllers.Base;
using academia.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<ApplicationDbContext>(x => x.Database.Migrate());

builder.Services.AddScoped<IResponseFactory, ResponseFactory>();
builder.Services.AddAutoMapper(typeof(MappingDtoProfiles).Assembly);

builder.Services.LoadRepositories();
builder.Services.LoadServices();
builder.Services.LoadValidators();

var app = builder.Build();

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();

    app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
}

app.UseMiddleware(typeof(ErrorHandlerMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
