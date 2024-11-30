using academia.Application.DTOs;
using academia.Application.Interfaces;
using academia.Application.Interfaces.Validator;
using academia.Application.Services;
using academia.Application.Validations;
using academia.Application.Validations.Utils;
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
        public static void LoadValidators(this IServiceCollection services) 
        {
            services.AddScoped<IValidator<UsuarioCadastroDto>, UsuarioCadastroValidator>();
            services.AddScoped<IValidator<UsuarioAtualizarDto>, UsuarioAtualizarValidator>();
            services.AddScoped<IValidator<UsuarioLoginDto>, UsuarioLoginValidator>();
            services.AddScoped<IUtilsValidator, UtilsValidator>();
        }

        public static void LoadServices (this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}
