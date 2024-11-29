using academia.Application.DTOs;
using academia.Application.Interfaces;
using academia.Application.Services;
using academia.Application.Validations;
using academia.Domain.Interfaces;
using academia.Domain.Interfaces.IRepository;
using academia.Infrastructure.Repository;
using FluentValidation;

namespace academia.WebApi.Extensions
{
    public static class ServicesLoaderExtension
    {
        public static void LoadRepositories(this IServiceCollection services) 
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
         d LoadValidators(this IServiceCollection services) 
        {
            services.AddScoped<IValidator<UsuarioCadastroDto>, UsuarioCadastroValidator>();
            services.AddScoped<IValidator<UsuarioAtualizarDto>, UsuarioAtualizarValidator>();
        }
    }
}
