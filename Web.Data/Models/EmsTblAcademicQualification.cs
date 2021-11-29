using System;
using System.Collections.Generic;

#nullable disable

namespace Web.Data.Models
{
    public partial class EmsTblAcademicQualification
    {
        public int EtaqAqId { get; set; }
        public int EtedEmployeeId { get; set; }
        public string EtaqQualification { get; set; }
        public DateTime? EtaqPassingYear { get; set; }
        public double? EtaqCgpa { get; set; }
        public string EtaqInstituteName { get; set; }
        public string EtaqUploadDocuments { get; set; }
        public bool? EtaqIsDelete { get; set; }
        public string EtaqCreatedBy { get; set; }
        public string EtaqCreatedByName { get; set; }
        public DateTime? EtaqCreatedByDate { get; set; }
        public string EtaqModifiedBy { get; set; }
        public string EtaqModifiedByName { get; set; }
        public DateTime? EtaqModifiedByDate { get; set; }

        public virtual EmsTblEmployeeDetails EtedEmployee { get; set; }
    }
}
