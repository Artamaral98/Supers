using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Supers.Domain.Entidades;

namespace Supers.Infrastructure.Configurations 
{
    public class HeroiSuperPoderConfiguration : IEntityTypeConfiguration<HeroiSuperPoder>
    {
        public void Configure(EntityTypeBuilder<HeroiSuperPoder> builder)
        {
            builder.ToTable("HeroiSuperPoder");

            builder.HasKey(hsp => new { hsp.HeroiId, hsp.SuperPoderId });

            builder.HasOne(hsp => hsp.Heroi)
                .WithMany(h => h.HeroisSuperPoderes)
                .HasForeignKey(hsp => hsp.HeroiId);

            builder.HasOne(hsp => hsp.SuperPoderes)
                .WithMany(sp => sp.HeroisSuperPoderes)
                .HasForeignKey(hsp => hsp.SuperPoderId);
        }
    }
}