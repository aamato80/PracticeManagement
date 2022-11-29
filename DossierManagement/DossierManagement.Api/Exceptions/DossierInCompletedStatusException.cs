namespace DossierManagement.Api.Exceptions
{
    public class DossierInCompletedStatusException : Exception
    {
        public DossierInCompletedStatusException() { }

        public DossierInCompletedStatusException(string message)
            : base(message) { }

        public DossierInCompletedStatusException(string message, Exception inner)
            : base(message, inner) { }
    }
}
