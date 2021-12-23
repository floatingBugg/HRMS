using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class EmsTblWorkingHistoryVM
    {
        public int EtwhWhId { get; set; }
        public int? EtwhEtedEmployeeId { get; set; }
        public string EtwhCompanyName { get; set; }
        public string EtwhWorkDesignation { get; set; }
        public DateTime? EtwhStratDate { get; set; }
        public DateTime? EtwhEndDate { get; set; }
        public string EtwhDuration { get; set; }
        public string EtwhExperienceLetterurl { get; set; }

        public byte[] EtwhExperienceLetter { get; set; }
        public string EtwhCreatedBy { get; set; }
        public string EtwhCreatedByName { get; set; }
        public DateTime? EtwhCreatedByDate { get; set; }
        public string EtwhModifiedBy { get; set; }
        public string EtwhModifiedByName { get; set; }
        public DateTime? EtwhModifiedByDate { get; set; }
        public bool? EtwhIsDelete { get; set; }
    }
}
