namespace Supers.Domain.Entidades
{
    public class SuperPoder
    {
        public long Id { get; set; }
        public string SuperPoderNome { get; set; }
        public string Descricao { get; set; }

        public ICollection<HeroiSuperPoder> HeroisSuperPoderes { get; set; } = new List<HeroiSuperPoder>();
    }
}
