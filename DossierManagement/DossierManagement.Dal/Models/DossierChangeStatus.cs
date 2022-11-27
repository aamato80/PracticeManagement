using DossierManagement.Dal.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DossierManagement.Dal.Models
{
    public class DossierChangeStatus
    {
        public int Id { get; set; }
        public int DossierId { get; set; }
        public DossierStatus Status { get; set; }
        public DossierResult Result { get; set; }
        public DateTime Date { get; set; }
    }
}
