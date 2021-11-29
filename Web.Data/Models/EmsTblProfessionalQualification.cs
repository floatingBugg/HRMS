using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web.Data.Models
{
    [Table("ems_tbl_professional_qualification")]
    public partial class EmsTblProfessionalQualification
    {
        [Key]
        [Column("etpq_pq_id")]
        public int EtpqPqId { get; set; }
        [Column("eted_employee_id")]
        public int EtedEmployeeId { get; set; }
        [Column("etpq_certification")]
        public string EtpqCertification { get; set; }
        [Column("etpq_strat_date", TypeName = "datetime")]
        public DateTime? EtpqStratDate { get; set; }
        [Column("etpq_end_date", TypeName = "datetime")]
        public DateTime? EtpqEndDate { get; set; }
        [Column("etpq_institute_name")]
        public string EtpqInstituteName { get; set; }
        [Column("etpq_documents")]
        public string EtpqDocuments { get; set; }
        [Required]
        [Column("etpq_is_delete")]
        public bool? EtpqIsDelete { get; set; }
        [Column("etpq_created_by")]
        public string EtpqCreatedBy { get; set; }
        [Column("etpq_created_by_name")]
        public string EtpqCreatedByName { get; set; }
        [Column("etpq_created_by_date", TypeName = "datetime")]
        public DateTime? EtpqCreatedByDate { get; set; }
        [Column("etpq_modified_by")]
        public string EtpqModifiedBy { get; set; }
        [Column("etpq_modified_by_name")]
        public string EtpqModifiedByName { get; set; }
        [Column("etpq_modified_by_date", TypeName = "datetime")]
        public DateTime? EtpqModifiedByDate { get; set; }

        [ForeignKey(nameof(EtedEmployeeId))]
        [InverseProperty(nameof(EmsTblEmployeeDetails.EmsTblProfessionalQualification))]
        public virtual EmsTblEmployeeDetails EtedEmployee { get; set; }
    }
}
