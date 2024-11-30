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
            CreateProjection<Usuario, UsuarioRetornoDto>();
        }
    }
}
