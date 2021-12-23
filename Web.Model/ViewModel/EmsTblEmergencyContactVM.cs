using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class EmsTblEmergencyContactVM
    {
        public int EtecEcId { get; set; }
        public int? EtecEtedEmployeeId { get; set; }
        public string EtecFirstName { get; set; }
        public string EtecLastName { get; set; }
        public string EtecRelation { get; set; }
        public string EtecContactNumber { get; set; }
        public string EtecAddress { get; set; }
        public string EtecCreatedBy { get; set; }
        public string EtecCreatedByName { get; set; }
        public DateTime? EtecCreatedByDate { get; set; }
        public string EtecModifiedBy { get; set; }
        public string EtecModifiedByName { get; set; }
        public DateTime? EtecModifiedByDate { get; set; }
        public bool? EtecIsDelete { get; set; }

    }
}
