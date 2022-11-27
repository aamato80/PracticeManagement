using DossierManagement.Api.DTOs;
using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierManagement.Test.Mocks
{
    internal class DossierChangeStatusMock
    {
        internal static IEnumerable<DossierChangeStatus> CreateRandomList(int dossierId, int itemNumber)
        {
            for (int i = 0; i < itemNumber; i++)
            {
                yield return new DossierChangeStatus()
                {
                    Id = Utils.CreateRandomNumber(),
                    Date = Utils.CreateRandomDate(null, null),
                    DossierId = dossierId,
                    Result = (DossierResult)Enum.ToObject(typeof(DossierResult), Utils.CreateRandomNumber(2)),
                    Status = (DossierStatus)Enum.ToObject(typeof(DossierStatus), Utils.CreateRandomNumber(2))
                };
            }
        }
    }
}
