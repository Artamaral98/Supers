using FluentMigrator;

namespace Supers.Infrastructure.Migrations.Versoes
{
    [Migration(3, "Criação da tabela de relacionamento HeroiSuperPoder")]
    public class Versao0000003 : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("HeroiSuperPoder")
                .WithColumn("HeroiId").AsInt32().NotNullable()
                    .ForeignKey("SuperHerois", "Id")

                .WithColumn("SuperPoderId").AsInt32().NotNullable()
                    .ForeignKey("SuperPoderes", "Id");

            Create.PrimaryKey("PK_HeroiSuperPoder")
                .OnTable("HeroiSuperPoder")
                .Columns("HeroiId", "SuperPoderId");
        }
    }
}