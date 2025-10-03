namespace Supers.Domain.Entidades
{
    public class SuperHeroi
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string NomeHeroi { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public ICollection<HeroiSuperPoder> HeroisSuperPoderes { get; set; } = new List<HeroiSuperPoder>();
    }
}
