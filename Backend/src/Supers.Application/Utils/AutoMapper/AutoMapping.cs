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
            CreateMap<CadastroSuperRequest, SuperHeroi>();
        }

        private void DomainToResponse()
        {
            CreateMap<SuperHeroi, CadastroSuperResponse>()
                .ForMember(dest => dest.SuperPoderes, opt => opt.MapFrom(src => src.HeroisSuperPoderes.Select(p => p.SuperPoderes.SuperPoder).ToList()));

            CreateMap<SuperHeroi, SumarioHerois>().ForMember(dest => dest.SuperPoderes,opt => opt.MapFrom(src => src.HeroisSuperPoderes
                .Select(hsp => hsp.SuperPoderes.SuperPoder)
                .ToList()));

            CreateMap<SuperPoderes, SuperPoderResponse>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.SuperPoder));
        }
    }
} 
