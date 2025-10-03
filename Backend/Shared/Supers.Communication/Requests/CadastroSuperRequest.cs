namespace Supers.Communication.Requests
{
    public class CadastroSuperRequest
    {
        public string Nome { get; set; } = string.Empty;

        public string NomeHeroi { get; set; } = string.Empty;

        public List<string> SuperPoderes { get; set; } = [];

        public DateTime DataNascimento { get; set; }

        public decimal Altura { get; set; }

        public decimal Peso { get; set; }
    }
}
