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
            CreateProjection<Usuario, UsuarioRetornoDto>()
                .ForMember(c => c.DataAtualizacao, d => d.MapFrom(e => e.Metadados!.DataAtualizacao))
                .ForMember(c => c.DataCriacao, d => d.MapFrom(e => e.Metadados!.DataCriacao));
        }
    }
}
