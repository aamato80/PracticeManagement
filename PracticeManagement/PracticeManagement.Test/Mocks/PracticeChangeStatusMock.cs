using PracticeManagement.Api.DTOs;
using PracticeManagement.Dal.Enums;
using PracticeManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement.Test.Mocks
{
    internal class PracticeChangeStatusMock
    {
        internal static IEnumerable<PracticeChangeStatus> CreateRandomList(int practiceId, int itemNumber)
        {
            for (int i = 0; i < itemNumber; i++)
            {
                yield return new PracticeChangeStatus()
                {
                    Id = Utils.CreateRandomNumber(),
                    Date = Utils.CreateRandomDate(null, null),
                    PracticeId = practiceId,
                    Result = (PracticeResult)Enum.ToObject(typeof(PracticeResult), Utils.CreateRandomNumber(2)),
                    Status = (PracticeStatus)Enum.ToObject(typeof(PracticeStatus), Utils.CreateRandomNumber(2))
                };
            }
        }
    }
}
