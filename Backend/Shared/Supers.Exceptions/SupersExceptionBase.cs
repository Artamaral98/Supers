namespace Supers.Exceptions
{
    public class SupersExceptionBase : SystemException
    {
        public SupersExceptionBase()
        {
        }
        public SupersExceptionBase(string message) : base(message)
        {
        }
    }

}
