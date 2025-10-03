namespace Supers.Communication.Responses
{
    public class CadastroSuperResponse
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string NomeHeroi { get; set; } = string.Empty;

        public List<string> SuperPoderes { get; set; } = [];

        public DateTime DataNascimento { get; set; }

        public float Altura { get; set; }

        public float Peso { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}
