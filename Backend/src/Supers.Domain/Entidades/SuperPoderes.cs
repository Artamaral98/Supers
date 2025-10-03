namespace Supers.Domain.Entidades
{
    public class SuperPoderes
    {
        public int Id { get; set; }
        public string SuperPoder { get; set; }
        public string Descricao { get; set; }

        public ICollection<HeroiSuperPoder> HeroisSuperPoderes { get; set; } = new List<HeroiSuperPoder>();
    }
}
