namespace Supers.Exceptions
{
    public class ErrosEmValidacaoException : SupersExceptionBase
    {
        public IList<string> MensagensDeErros { get; set; }

        public ErrosEmValidacaoException(IList<string> erros)
        {
            MensagensDeErros = erros;
        }
    }
}
