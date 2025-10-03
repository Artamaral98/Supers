using AutoMapper;
using Supers.Communication.Requests;
using Supers.Domain.Entidades;

namespace Supers.Application.Utils.AutoMapper
{
    class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            CreateMap<CadastroSuperRequest, SuperHeroi>()
                .ForMember(dest => dest.HeroisSuperPoderes, opt => opt.MapFrom(src => src.SuperPoderes.Select
                (poderNome => new HeroiSuperPoder
                {
                    SuperPoder = new SuperPoder { SuperPoderNome = poderNome }
                }).ToList()
            )); ;
        }
    }
} 
