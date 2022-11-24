using PracticeManagement.Dal.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement.Dal.Models
{
    public class PracticeChangeStatus
    {
        public int Id { get; set; }
        public int PracticeId { get; set; }
        public PracticeStatus Status { get; set; }
        public PracticeResult Result { get; set; }
        public DateTime? Date { get; set; }
    }
}
