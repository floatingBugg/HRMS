using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models
{
    [Table("ems_tbl_emergency_contact")]
    public partial class EmsTblEmergencyContact
    {
        [Key]
        [Column("etec_ec_id")]
        public int EtecEcId { get; set; }
        [Column("eted_employee_id")]
        public int EtedEmployeeId { get; set; }
        [Column("etec_first_name")]
        public string EtecFirstName { get; set; }
        [Column("etec_last_name")]
        public string EtecLastName { get; set; }
        [Column("etec_relation")]
        public string EtecRelation { get; set; }
        [Column("etec_contact_number")]
        public string EtecContactNumber { get; set; }
        [Column("etec_address")]
        public string EtecAddress { get; set; }
        [Required]
        [Column("etec_is_delete")]
        public bool? EtecIsDelete { get; set; }
        [Column("etec_created_by")]
        public string EtecCreatedBy { get; set; }
        [Column("etec_created_by_name")]
        public string EtecCreatedByName { get; set; }
        [Column("etec_created_by_date", TypeName = "datetime")]
        public DateTime? EtecCreatedByDate { get; set; }
        [Column("etec_modified_by")]
        public string EtecModifiedBy { get; set; }
        [Column("etec_modified_by_name")]
        public string EtecModifiedByName { get; set; }
        [Column("etec_modified_by_date", TypeName = "datetime")]
        public DateTime? EtecModifiedByDate { get; set; }

        [ForeignKey(nameof(EtedEmployeeId))]
        [InverseProperty(nameof(EmsTblEmployeeDetails.EmsTblEmergencyContact))]
        public virtual EmsTblEmployeeDetails EtedEmployee { get; set; }
    }
}
