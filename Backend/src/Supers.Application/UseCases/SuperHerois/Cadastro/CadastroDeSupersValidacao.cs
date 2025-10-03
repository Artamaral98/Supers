using FluentValidation;
using Supers.Communication.Requests;
using Supers.Exceptions;

namespace Supers.Application.UseCases.SuperHerois.Cadastro
{
    public class CadastroDeSupersValidacao : AbstractValidator<CadastroSuperRequest>
    {
        public CadastroDeSupersValidacao()
        {
            RuleFor(user => user.Nome).NotEmpty().WithMessage(Mensagens.NOME_VAZIO);
            //Não permite caracteres especiais, impedindo SQL Injection
            RuleFor(user => user.NomeHeroi).Matches(@"^[a-zA-ZÀ-ÿ\s0-9]+$").WithMessage(Mensagens.NOME_CARACTERES_ESPECIAIS);
            RuleFor(user => user.Nome).MaximumLength(120).WithMessage(Mensagens.NOME_TAMANHO_MAXIMO);
            RuleFor(user => user.NomeHeroi).NotEmpty().WithMessage(Mensagens.NOME_HEROI_VAZIO);
            RuleFor(user => user.NomeHeroi).Matches(@"^[a-zA-ZÀ-ÿ\s0-9]+$").WithMessage(Mensagens.NOME_CARACTERES_ESPECIAIS);
            RuleFor(user => user.NomeHeroi).MaximumLength(120).WithMessage(Mensagens.NOME_HEROI_TAMANHO_MAXIMO);
            RuleFor(user => user.SuperPoderes).NotEmpty().WithMessage(Mensagens.SUPER_PODERES_VAZIO);
            RuleFor(user => user.DataNascimento).NotEmpty().WithMessage(Mensagens.DATA_NASCIMENTO_VAZIO);
            RuleFor(user => user.Altura).NotEmpty().WithMessage(Mensagens.ALTURA_VAZIO);
            RuleFor(user => user.Peso).NotEmpty().WithMessage(Mensagens.PESO_VAZIO);
        }
    }
}