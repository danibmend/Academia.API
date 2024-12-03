using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Application.DTOs;
using academia.Domain.Entidades;
using AutoMapper;

namespace academia.Application.Mappings
{
    internal class UsuarioRetornoMap : Profile
    {
        public UsuarioRetornoMap() 
        {
            //mapeando a entidade usuario usando o projection, para auxiliar no cache do banco de dados, retornando somente o necessario (RetornoDto)
            CreateProjection<Usuario, UsuarioRetornoDto>()
                .ForMember(c => c.Password, e => e.MapFrom(e => e.Senha));
        }
    }
}
