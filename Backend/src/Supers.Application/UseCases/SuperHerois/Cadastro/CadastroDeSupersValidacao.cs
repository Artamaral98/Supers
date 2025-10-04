using FluentValidation;
using Supers.Communication.Requests;
using Supers.Exceptions;

namespace Supers.Application.UseCases.SuperHerois.Cadastro
{
    public class CadastroDeSupersValidacao : AbstractValidator<CadastroSuperRequest>
    {
        public CadastroDeSupersValidacao()
        {
            RuleFor(super => super.Nome).NotEmpty().WithMessage(Mensagens.NOME_VAZIO);
            RuleFor(super => super.Nome).Matches(@"^[a-zA-ZÀ-ÿ\s0-9]+$").WithMessage(Mensagens.NOME_CARACTERES_ESPECIAIS)
                .When(super => !string.IsNullOrEmpty(super.Nome));
            //Não permite caracteres especiais, impedindo SQL Injection
            RuleFor(super => super.NomeHeroi).Matches(@"^[a-zA-ZÀ-ÿ\s0-9]+$").WithMessage(Mensagens.NOME_CARACTERES_ESPECIAIS)
                .When(super => !string.IsNullOrEmpty(super.NomeHeroi));
            RuleFor(super => super.Nome).MaximumLength(120).WithMessage(Mensagens.NOME_TAMANHO_MAXIMO);
            RuleFor(super => super.NomeHeroi).NotEmpty().WithMessage(Mensagens.NOME_HEROI_VAZIO);
            RuleFor(super => super.NomeHeroi).MaximumLength(120).WithMessage(Mensagens.NOME_HEROI_TAMANHO_MAXIMO);
            RuleFor(super => super.SuperPoderes).NotEmpty().WithMessage(Mensagens.SUPER_PODERES_VAZIO);
            RuleFor(super => super.DataNascimento).NotEmpty().WithMessage(Mensagens.DATA_NASCIMENTO_VAZIO);
            RuleFor(super => super.Altura).NotEmpty().WithMessage(Mensagens.ALTURA_VAZIO);
            RuleFor(super => super.Peso).NotEmpty().WithMessage(Mensagens.PESO_VAZIO);
        }
    }
}