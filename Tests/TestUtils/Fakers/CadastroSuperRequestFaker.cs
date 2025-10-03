using Bogus;
using Supers.Communication.Requests;

namespace TestUtils.Fakers
{
    public static class CadastroSuperRequestFaker
    {
        public static Faker<CadastroSuperRequest> Gerar()
        {
            return new Faker<CadastroSuperRequest>()
                .RuleFor(r => r.Nome, f => f.Person.FullName)
                .RuleFor(r => r.NomeHeroi, f => f.Name.JobTitle())
                .RuleFor(r => r.DataNascimento, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                .RuleFor(r => r.Altura, f => f.Random.Decimal(1.50m, 2.30m))
                .RuleFor(r => r.Peso, f => f.Random.Decimal(50.0m, 150.0m))
                .RuleFor(r => r.SuperPoderes, f => f.Make(f.Random.Int(1, 5), () => f.Random.Int(1, 14)));
        }
    }
}
