using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PortalServico.Application.Interfaces.IServices.Base;

namespace academia.Application.Services.Base
{
    public class ServiceBase : IServiceBase
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public ServiceBase(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        public Expression<Func<TEntity, bool>> CreateExpression<TEntity>()
        {
            return null!;
        }
    }
}
