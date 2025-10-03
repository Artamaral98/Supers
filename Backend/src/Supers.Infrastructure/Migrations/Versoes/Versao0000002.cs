using FluentMigrator;

namespace Supers.Infrastructure.Migrations.Versoes
{
    [Migration(2, "Criação da tabela de SuperPoderes")]
    public class Versao0000002 : VersaoBase
    {
        public override void Up()
        {
            CreateTable("SuperPoderes")
                .WithColumn("SuperPoder").AsString(50).NotNullable()
                .WithColumn("Descricao").AsString(250).Nullable();
        }
    }
}
    