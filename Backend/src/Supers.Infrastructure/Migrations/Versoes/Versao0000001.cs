using FluentMigrator;

namespace Supers.Infrastructure.Migrations.Versoes
{
    [Migration(1, "Criação da tabela de SuperHerois")]
    public class Versao0000001 : VersaoBase
    {
        public override void Up()
        {
            CreateTable("SuperHerois")
                .WithColumn("Nome").AsString(120).NotNullable()
                .WithColumn("NomeHeroi").AsString(120).NotNullable()
                .WithColumn("DataNascimento").AsCustom("datetime2(7)").NotNullable()
                .WithColumn("Altura").AsFloat().NotNullable()
                .WithColumn("Peso").AsFloat().NotNullable()
                .WithColumn("CriadoEm").AsCustom("datetime2(7)").NotNullable();
        }
    }
}
