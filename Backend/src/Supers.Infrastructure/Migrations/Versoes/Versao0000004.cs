using FluentMigrator;

namespace Supers.Infrastructure.Migrations.Versoes
{
    [Migration(4, "Adiciona dados iniciais na tabela SuperPoderes")]
    public class Versao0000004_SeedSuperpoderes : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("SuperPoderes")
                .Row(new { SuperPoder = "Super Força", Descricao = "Capacidade de exercer força física acima do normal." })
                .Row(new { SuperPoder = "Voo", Descricao = "Capacidade de voar sem auxílio mecânico." })
                .Row(new { SuperPoder = "Invisibilidade", Descricao = "Capacidade de se tornar invisível a olho nu." })
                .Row(new { SuperPoder = "Telepatia", Descricao = "Habilidade de ler os pensamentos de outras pessoas." })
                .Row(new { SuperPoder = "Super Velocidade", Descricao = "Capacidade de se mover em velocidades que excedem a do som." })
                .Row(new { SuperPoder = "Manipulação de Energia", Descricao = "Habilidade de controlar e gerar várias formas de energia." })
                .Row(new { SuperPoder = "Super Resistência", Descricao = "Capacidade de resistir a fortes impactos sem se ferir." })
                .Row(new { SuperPoder = "Visão de calor", Descricao = "Capacidade de desferir laser através dos olhos" })
                .Row(new { SuperPoder = "Dinheiro", Descricao = "Ser rico." })
                .Row(new { SuperPoder = "Super inteligência", Descricao = "QI extremamente elevado." })
                .Row(new { SuperPoder = "Raio", Descricao = "Capacidade de controlar e desferir poderes elétricos." })
                .Row(new { SuperPoder = "Fogo", Descricao = "Capacidade de controlar e desferir poderes de fogo." })
                .Row(new { SuperPoder = "Gelo", Descricao = "Capacidade de controlar e desferir poderes de gelo." })
                .Row(new { SuperPoder = "Aranha", Descricao = "Agilidade e percepção fora do comum ao ser picado por uma aranha e soltar teias." });
        }

        public override void Down()
        {
            Delete.FromTable("SuperPoderes").AllRows();
        }
    }
}