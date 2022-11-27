namespace DossierManagement.Api.Exceptions
{
    public class NotCongruentDossierResultException :Exception
    {
        public NotCongruentDossierResultException() { }

        public NotCongruentDossierResultException(string message)
            : base(message) { }

        public NotCongruentDossierResultException(string message, Exception inner)
            : base(message, inner) { }
    }
}
