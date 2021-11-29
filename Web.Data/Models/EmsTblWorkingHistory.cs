using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web.Data.Models
{
    [Table("ems_tbl_working_history")]
    public partial class EmsTblWorkingHistory
    {
        [Key]
        [Column("etwh_wh_id")]
        public int EtwhWhId { get; set; }
        [Column("eted_employee_id")]
        public int EtedEmployeeId { get; set; }
        [Column("etwh_company_name")]
        public string EtwhCompanyName { get; set; }
        [Column("etwh_designation")]
        public string EtwhDesignation { get; set; }
        [Column("etwh_strat_date", TypeName = "datetime")]
        public DateTime? EtwhStratDate { get; set; }
        [Column("etwh_end_date", TypeName = "datetime")]
        public DateTime? EtwhEndDate { get; set; }
        [Column("etwh_duration")]
        public string EtwhDuration { get; set; }
        [Column("etwh_experience_letter")]
        public string EtwhExperienceLetter { get; set; }
        [Required]
        [Column("etwh_is_delete")]
        public bool? EtwhIsDelete { get; set; }
        [Column("etwh_created_by")]
        public string EtwhCreatedBy { get; set; }
        [Column("etwh_created_by_name")]
        public string EtwhCreatedByName { get; set; }
        [Column("etwh_created_by_date", TypeName = "datetime")]
        public DateTime? EtwhCreatedByDate { get; set; }
        [Column("etwh_modified_by")]
        public string EtwhModifiedBy { get; set; }
        [Column("etwh_modified_by_name")]
        public string EtwhModifiedByName { get; set; }
        [Column("etwh_modified_by_date", TypeName = "datetime")]
        public DateTime? EtwhModifiedByDate { get; set; }

        [ForeignKey(nameof(EtedEmployeeId))]
        [InverseProperty(nameof(EmsTblEmployeeDetails.EmsTblWorkingHistory))]
        public virtual EmsTblEmployeeDetails EtedEmployee { get; set; }
    }
}
