using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvestQ.Application.Dtos;
using InvestQ.Application.Helpers;
using InvestQ.Data.Paginacao;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Identity;

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

            CreateMap<Setor, SetorDto>().ReverseMap();

            CreateMap<Subsetor, SubsetorDto>().ReverseMap();

            CreateMap<Segmento, SegmentoDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, UserUpdateDto>().ReverseMap();

            CreateMap<User, UserLoginDto>().ReverseMap();
            
        }
    }
}