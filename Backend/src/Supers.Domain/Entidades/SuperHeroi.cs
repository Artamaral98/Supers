namespace Supers.Domain.Entidades
{
    public class SuperHeroi
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeHeroi { get; set; }
        public DateTime DataNascimento { get; set; }
        public float Altura { get; set; }
        public float Peso { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public ICollection<HeroiSuperPoder> HeroisSuperPoderes { get; set; } = new List<HeroiSuperPoder>();
    }
}
