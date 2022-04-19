using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
   public class leaveGridVM
    {
        public int empid { get; set; }
        public string EmployeeName { get; set; }
        public string empDesignation { get; set; }
        public int CasualTaken { get; set; }
        public int AnnualTaken { get; set; }
        public int lrSickTaken { get; set; }
        public int TotalTaken { get; set; }
        public int CasualAssign { get; set; }
        public int AnnualAssign { get; set; }
        public int SickAssign { get; set; }
        public int TotalAssign { get; set; }
    }

}
