﻿namespace Supers.Communication.Requests
{
    public class RequestObterTodosOsSupers
    {
        public List<SumarioHerois> Herois { get; set; } = new();
    }

    public class SumarioHerois
    {
        public string Nome { get; set; } = string.Empty;
        public string NomeHeroi { get; set; } = string.Empty;
        public List<string> SuperPoderes { get; set; } = new();
        public DateTime DataDeDascimento { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
    }
}
