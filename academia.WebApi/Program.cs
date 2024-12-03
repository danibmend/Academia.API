using System.Text;
using academia.Application.Mappings;
using academia.Infrastructure;
using academia.Infrastructure.Persistence;
using academia.WebApi.Controllers.Base;
using academia.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//conexão com o banco
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<ApplicationDbContext>(x => x.Database.Migrate());

builder.Services.AddScoped<IResponseFactory, ResponseFactory>();
builder.Services.AddAutoMapper(typeof(MappingDtoProfiles).Assembly);
//registrando as dependencias e servicos
builder.Services.LoadRepositories();
builder.Services.LoadServices();
builder.Services.LoadValidators();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
//habilitando cors para consumo externo 
app.UseCors(options => options
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());
//setando o nosso handler de exceptions
app.UseMiddleware(typeof(ErrorHandlerMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
