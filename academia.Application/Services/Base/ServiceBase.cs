using System.Linq.Expressions;
using academia.Domain.Interfaces;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PortalServico.Application.Interfaces.IServices.Base;

namespace academia.Application.Services.Base
{
    public class ServiceBase : IServiceBase
    {
        //Service base em que todas as classes services vao herdar, nela tem-se o IUnitOfWork (usado para conciliar as operações de repositório (acesso ao banco)), 
        //tem também o IMapper (usado para o projection, para mapear as entidades que retornam do banco para os Dtos de retorno

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
