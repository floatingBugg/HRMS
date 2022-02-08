using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class LmsLeaveRecordVM
    {
        public int LmslrRecordId { get; set; }
        public int? LmslrEtedEmployeeId { get; set; }

        public string EmpDesignation { get; set; }
        public string? LmslrEtedEmployeeName { get; set; }

        public int? LmslrAnnualAssign { get; set; }
        public int? LmslrSickAssign { get; set; }
        public int? LmslrCasualAssign { get; set; }
        public int? LmslrTotalAssign { get; set; }
        public int? LmslrAnnualTaken { get; set; }
        public int? LmslrSickTaken { get; set; }
        public int? LmslrCasualTaken { get; set; }
        public int? LmslrTotalTaken { get; set; }
        public string LmslrCreatedBy { get; set; }
        public string LmslrCreatedByName { get; set; }
        public DateTime? LmslrCreatedByDate { get; set; }
        public bool? LmslrIsDelete { get; set; }
    }
}
