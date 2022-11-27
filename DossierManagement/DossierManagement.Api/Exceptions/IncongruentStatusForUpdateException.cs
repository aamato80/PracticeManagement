namespace DossierManagement.Api.Exceptions
{
    public class IncongruentStatusForUpdateException : Exception
    {
        public IncongruentStatusForUpdateException() { }

        public IncongruentStatusForUpdateException(string message)
            : base(message) { }

        public IncongruentStatusForUpdateException(string message, Exception inner)
            : base(message, inner) { }
    }
}
