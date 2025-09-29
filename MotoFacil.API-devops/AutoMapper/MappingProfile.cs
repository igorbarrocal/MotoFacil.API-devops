using AutoMapper;
using MotoFacil.API.Infrastructure.Persistence;
using MotoFacil.API.DTOs.Moto;
using MotoFacil.API.DTOs.Usuario;

namespace MotoFacil.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Moto, MotoDetalhesDTO>();
            CreateMap<MotoDTO, Moto>();
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (int)src.Tipo));
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (TipoUsuario)src.Tipo));
        }
    }
}