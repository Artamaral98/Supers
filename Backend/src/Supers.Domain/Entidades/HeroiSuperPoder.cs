namespace Supers.Domain.Entidades
{
    public class HeroiSuperPoder
    {
        public int HeroiId { get; set; }
        public SuperHeroi Heroi { get; set; }

        public int SuperPoderId { get; set; }
        public SuperPoderes SuperPoderes { get; set; }
    }
}
