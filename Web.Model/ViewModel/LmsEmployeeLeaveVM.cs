using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class LmsEmployeeLeaveVM
    {
        public int LmselLeaveId { get; set; }
        public int? LmselEtedEmployeeId { get; set; }
        public int? LmselLeaveType { get; set; }
        public DateTime? LmselStartDate { get; set; }
        public DateTime? LmselEndDate { get; set; }
        public int? LmselDays { get; set; }
        public string LmselCreatedBy { get; set; }
        public string LmselCreatedByName { get; set; }
        public DateTime? LmselCreatedByDate { get; set; }
        public bool? LmselIsDelete { get; set; }
    }
}
