using Bogus;
using Supers.Domain.Entidades;

namespace TestUtils.Fakers
{
    public static class SuperHeroiFaker
    {
        public static Faker<SuperHeroi> Gerar()
        {
            return new Faker<SuperHeroi>()
                .RuleFor(h => h.Id, f => f.IndexFaker + 1)
                .RuleFor(h => h.Nome, f => f.Person.FullName)
                .RuleFor(h => h.NomeHeroi, f => f.Name.JobTitle())
                .RuleFor(h => h.DataNascimento, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                .RuleFor(h => h.Altura, f => f.Random.Float(1.50f, 2.30f))
                .RuleFor(h => h.Peso, f => f.Random.Float(50.0f, 150.0f))
                .RuleFor(h => h.CriadoEm, () => DateTime.UtcNow)
                // Para a coleção de relacionamentos, podemos deixá-la vazia por padrão
                // ou criar fakes para ela também, se o teste precisar.
                .RuleFor(h => h.HeroisSuperPoderes, f => new List<HeroiSuperPoder>());
        }
    }
}