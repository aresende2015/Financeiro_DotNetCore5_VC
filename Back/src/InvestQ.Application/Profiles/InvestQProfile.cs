using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos;
using InvestQ.Application.Helpers;
using InvestQ.Domain.Entities;

namespace InvestQ.Application.Profiles
{
    public class InvestQProfile : Profile
    {
        public InvestQProfile()
        {
            CreateMap<Cliente, ClienteDto>()
                .ForMember(
                    dest => dest.NomeCompleto,
                    opt => opt.MapFrom(src => $"{src.Nome} {src.SobreNome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(src => src.DataDeNascimento.GetCurrentAge())
                );

            CreateMap<ClienteDto, Cliente>();

            CreateMap<ClienteCorretora, ClienteCorretoraDto>().ReverseMap();

            CreateMap<Corretora, CorretoraDto>().ReverseMap();
            
        }
    }
}