namespace Supers.Domain.Entidades
{
    public class HeroiSuperPoder
    {
        public long HeroiId { get; set; }
        public SuperHeroi Heroi { get; set; }

        public long SuperPoderId { get; set; }
        public SuperPoder SuperPoder { get; set; }
    }
}
