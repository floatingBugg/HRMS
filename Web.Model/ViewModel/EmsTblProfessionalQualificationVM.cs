using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class EmsTblProfessionalQualificationVM
    {
        public int EtpqPqId { get; set; }
        public int? EtpqEtedEmployeeId { get; set; }
        public string EtpqCertification { get; set; }
        public DateTime? EtpqStratDate { get; set; }
        public DateTime? EtpqEndDate { get; set; }
        public string EtpqInstituteName { get; set; }
        public string EtpqDocuments { get; set; }
        public string EtpqCreatedBy { get; set; }
        public string EtpqCreatedByName { get; set; }
        public DateTime? EtpqCreatedByDate { get; set; }
        public string EtpqModifiedBy { get; set; }
        public string EtpqModifiedByName { get; set; }
        public DateTime? EtpqModifiedByDate { get; set; }
        public bool? EtpqIsDelete { get; set; }
    }
}
