using AutoMapper;
using InvestQ.Application.Dtos;
using InvestQ.Application.Dtos.Enum;
using InvestQ.Application.Helpers;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Entities.Enum;
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

            CreateMap<Acao, AcaoDto>().ReverseMap();

            CreateMap<AdministradorDeFundoImobiliario, AdministradorDeFundoImobiliarioDto>().ReverseMap();

            CreateMap<FundoImobiliario, FundoImobiliarioDto>().ReverseMap();

            CreateMap<SegmentoAnbima, SegmentoAnbimaDto>().ReverseMap();

            CreateMap<TesouroDireto, TesouroDiretoDto>().ReverseMap();

            CreateMap<TipoDeInvestimento, TipoDeInvestimentoDto>().ReverseMap();

            CreateMap<Provento, ProventoDto>().ReverseMap();
            
            CreateMap<Ativo, AtivoDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, UserUpdateDto>().ReverseMap();

            CreateMap<User, UserLoginDto>().ReverseMap();

            CreateMap<TipoDeAtivo, TipoDeAtivoDto>().ReverseMap();
            
        }
    }
}