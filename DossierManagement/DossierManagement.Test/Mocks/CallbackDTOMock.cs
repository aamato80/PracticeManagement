using DossierManagement.Api.DTOs;
using DossierManagement.Dal.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierManagement.Test.Mocks
{
    internal class CallbackDTOMock
    {
        internal static CallbackDTO Create(int dossierId, DossierStatus status, DossierResult result)
        {
            return new CallbackDTO(dossierId, status, result);
            
        }
    }
}
