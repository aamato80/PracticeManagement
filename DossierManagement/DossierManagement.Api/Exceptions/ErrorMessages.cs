namespace DossierManagement.Api.Exceptions
{
    public class ErrorMessages
    {
        public const string NotCongruentDossierResult = "The new dossier's status is not congruent with his result";
        public const string IncongruentStatusForUpdate = "The dossier can be modified only if it is in Created status";
        public const string DossierNotFound = "The requested Dossier is Not Found";
        public const string DossierAlreadyInCompletedStatus = "The requested Dossier can't change status because is already in completed status";

    }
}
