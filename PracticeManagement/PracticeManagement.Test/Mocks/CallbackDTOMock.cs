using PracticeManagement.Api.DTOs;
using PracticeManagement.Dal.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement.Test.Mocks
{
    internal class CallbackDTOMock
    {
        internal static CallbackDTO Create(int practiceId, PracticeStatus status, PracticeResult result)
        {
            return new CallbackDTO(practiceId, status, result);
            
        }
    }
}
