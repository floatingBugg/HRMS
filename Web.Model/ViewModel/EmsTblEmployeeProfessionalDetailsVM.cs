using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class EmsTblEmployeeProfessionalDetailsVM
    {
        public int EtepdPdId { get; set; }
        public int? EtepdEtedEmployeeId { get; set; }
        public string EtepdProfDesignation { get; set; }
        public string EtepdSalary { get; set; }
        public DateTime? EtepdJoiningDate { get; set; }
        public string EtepdProbation { get; set; }
        public string EtepdCreatedBy { get; set; }
        public string EtepdCreatedByName { get; set; }
        public DateTime? EtepdCreatedByDate { get; set; }
        public string EtepdModifiedBy { get; set; }
        public string EtepdModifiedByName { get; set; }
        public DateTime? EtepdModifiedByDate { get; set; }
        public bool? EtepdIsDelete { get; set; }
    }
}
