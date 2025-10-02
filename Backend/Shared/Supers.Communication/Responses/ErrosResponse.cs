namespace Supers.Communication.Responses
{
    public class ErrosResponse
    {
        public IList<string> Erros {get; set;}

        public ErrosResponse(IList<string> errors) => Erros = errors;

        public ErrosResponse(string erros)
        {
            Erros = new List<string> { erros };
        }
    }
}