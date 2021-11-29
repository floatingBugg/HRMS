using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web.Data.Models
{
    [Table("ems_tbl_employee_professional_details")]
    public partial class EmsTblEmployeeProfessionalDetails
    {
        [Key]
        [Column("etepd_pd_id")]
        public int EtepdPdId { get; set; }
        [Column("eted_employee_id")]
        public int EtedEmployeeId { get; set; }
        [Column("etepd_designation")]
        public string EtepdDesignation { get; set; }
        [Column("etepd_salary")]
        public string EtepdSalary { get; set; }
        [Column("etepd_joining_date", TypeName = "datetime")]
        public DateTime? EtepdJoiningDate { get; set; }
        [Column("etepd_probation")]
        [StringLength(1)]
        public string EtepdProbation { get; set; }
        [Required]
        [Column("etepd_is_delete")]
        public bool? EtepdIsDelete { get; set; }
        [Column("etepd_created_by")]
        public string EtepdCreatedBy { get; set; }
        [Column("etepd_created_by_name")]
        public string EtepdCreatedByName { get; set; }
        [Column("etepd_created_by_date", TypeName = "datetime")]
        public DateTime? EtepdCreatedByDate { get; set; }
        [Column("etepd_modified_by")]
        public string EtepdModifiedBy { get; set; }
        [Column("etepd_modified_by_name")]
        public string EtepdModifiedByName { get; set; }
        [Column("etepd_modified_by_date", TypeName = "datetime")]
        public DateTime? EtepdModifiedByDate { get; set; }

        [ForeignKey(nameof(EtedEmployeeId))]
        [InverseProperty(nameof(EmsTblEmployeeDetails.EmsTblEmployeeProfessionalDetails))]
        public virtual EmsTblEmployeeDetails EtedEmployee { get; set; }
    }
}
