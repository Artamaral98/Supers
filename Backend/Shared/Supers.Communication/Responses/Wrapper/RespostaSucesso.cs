namespace Supers.Communication.Responses.Wrapper
{
    public class RespostaSucesso<T>
    {
        public string Mensagem { get; set; }
        public T Dados { get; set; }
    }
}
