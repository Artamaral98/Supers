using AutoMapper;
using Supers.Communication.Requests;
using Supers.Communication.Responses;
using Supers.Domain.Entidades;

namespace Supers.Application.Utils.AutoMapper
{
    class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<CadastroSuperRequest, SuperHeroi>()
                .ForMember(dest => dest.HeroisSuperPoderes, opt => opt.MapFrom(src => src.SuperPoderes.Select
                (poderNome => new HeroiSuperPoder
                {
                    SuperPoderes = new SuperPoderes { SuperPoder = poderNome }
                }).ToList()
            )); ;
        }

        private void DomainToResponse()
        {
            CreateMap<SuperHeroi, CadastroSuperResponse>()
                .ForMember(dest => dest.SuperPoderes, opt => opt.MapFrom(src => src.HeroisSuperPoderes.Select(p => p.SuperPoderes.SuperPoder).ToList()));

            CreateMap<SuperHeroi, SumarioHerois>().ForMember(dest => dest.SuperPoderes,opt => opt.MapFrom(src => src.HeroisSuperPoderes
                .Select(hsp => hsp.SuperPoderes.SuperPoder)
                .ToList()));

        }
    }
} 
