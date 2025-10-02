namespace Supers.Communication.Responses
{
    public class CadastroSuperResponse
    {
        public long Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string NomeHeroi { get; set; } = string.Empty;

        public List<string> SuperPoderes { get; set; } = [];

        public DateTime DataDeNascimento { get; set; }

        public decimal Altura { get; set; }

        public decimal Peso { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}
