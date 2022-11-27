namespace DossierManagement.Api.Exceptions
{
    public class DossierNotFoundException : Exception
    {
        public DossierNotFoundException() { }

        public DossierNotFoundException(string message)
            : base(message) { }

        public DossierNotFoundException(string message, Exception inner)
            : base(message, inner) { }
    }
}
