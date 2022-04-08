using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public  class EmsEmployementStatusVM
    {

        public int EesEmployementId { get; set; }
        public int EesEtedEmployeeId { get; set; }
        public int EesEcsEmpstatusId { get; set; }
        public DateTime? EesStartDate { get; set; }
        public DateTime? EesEndDate { get; set; }
        public DateTime? EesClearenceDate { get; set; }
        public int? EesIncrement { get; set; }
        public DateTime? EesDateOfIncrement { get; set; }
        public string EesRemarks { get; set; }
        public string EesDuration { get; set; }
        public string EesContractType { get; set; }
        public int EesSalary { get; set; }
        public string EesCreatedBy { get; set; }
        public string EesCreatedByName { get; set; }
        public DateTime? EesCreatedByDate { get; set; }
        public string EesModifiedBy { get; set; }
        public string EesModifiedByName { get; set; }
        public DateTime? EesModifiedByDate { get; set; }
        public bool? EesIsDelete { get; set; }
        public string EesetedpartTimetype { get; set; }
        public string EesDays { get; set; }
    }
}
