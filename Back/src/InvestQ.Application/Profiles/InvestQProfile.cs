using AutoMapper;
using InvestQ.Application.Dtos;
using InvestQ.Application.Dtos.Enum;
using InvestQ.Application.Helpers;
using InvestQ.Domain.Entities;
using InvestQ.Domain.Entities.Acoes;
using InvestQ.Domain.Entities.Ativos;
using InvestQ.Domain.Entities.Clientes;
using InvestQ.Domain.Enum;
using InvestQ.Domain.Entities.FundosImobiliarios;
using InvestQ.Domain.Entities.TesourosDiretos;
using InvestQ.Domain.Identity;
using InvestQ.Application.Dtos.Clientes;
using InvestQ.Application.Dtos.Acoes;
using InvestQ.Application.Dtos.Ativos;
using InvestQ.Application.Dtos.FundosImobiliarios;
using InvestQ.Application.Dtos.TesourosDiretos;
using InvestQ.Application.Dtos.Identity;

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

            CreateMap<Carteira, CarteiraDto>().ReverseMap();

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