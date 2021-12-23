using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class EmsTblAcademicQualificationVM
    {
        public int EtaqAqId { get; set; }
        public int? EtaqEtedEmployeeId { get; set; }
        public string EtaqQualification { get; set; }
        public long EtaqPassingYear { get; set; }
        public double? EtaqCgpa { get; set; }
        public string EtaqInstituteName { get; set; }
        public string EtaqUploadDocumentsUrl { get; set; }
        public byte[] EtaqUploadDocuments { get; set; }
        public string EtaqCreatedBy { get; set; }
        public string EtaqCreatedByName { get; set; }
        public DateTime? EtaqCreatedByDate { get; set; }
        public string EtaqModifiedBy { get; set; }
        public string EtaqModifiedByName { get; set; }
        public DateTime? EtaqModifiedByDate { get; set; }
        public bool? EtaqIsDelete { get; set; }
    }
}
