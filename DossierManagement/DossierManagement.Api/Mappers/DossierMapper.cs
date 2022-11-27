using DossierManagement.Api.DTOs;
using DossierManagement.Dal.Models;
using System.CodeDom.Compiler;

namespace DossierManagement.Api.Mappers
{
    public class DossierMapper
    {
        public static GetDossierResponseDTO MapToGet(Dossier dossier)
        {
            if (dossier == null) {
                return null;
            }

            return new GetDossierResponseDTO(
                dossier.Id,
                dossier.FirstName,
                dossier.LastName,
                dossier.FiscalCode,
                dossier.BirthDate,
                dossier.Status,
                dossier.Result,
                PopulateChangesStatus(dossier));
        }

        private static IEnumerable<DossierChangeStatusDTO> PopulateChangesStatus(Dossier dossier)
        {
            foreach (var item in dossier.StatusChanges)
            {
                yield return new DossierChangeStatusDTO(item.Status, item.Result, item.Date);
            }
        }

        public static AddedDossierResponseDTO MapToAdd(Dossier dossier)
        {
            if (dossier == null)
            {
                return null;
            }

            return new AddedDossierResponseDTO(
                dossier.Id,
                dossier.FirstName,
                dossier.LastName,
                dossier.FiscalCode,
                dossier.BirthDate);
        }
    }
}
