using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web.Data.Models
{
    [Table("ems_tbl_employee_details")]
    public partial class EmsTblEmployeeDetails
    {
        public EmsTblEmployeeDetails()
        {
            EmsTblAcademicQualification = new HashSet<EmsTblAcademicQualification>();
            EmsTblEmergencyContact = new HashSet<EmsTblEmergencyContact>();
            EmsTblEmployeeProfessionalDetails = new HashSet<EmsTblEmployeeProfessionalDetails>();
            EmsTblProfessionalQualification = new HashSet<EmsTblProfessionalQualification>();
            EmsTblWorkingHistory = new HashSet<EmsTblWorkingHistory>();
        }

        [Key]
        [Column("eted_employee_id")]
        public int EtedEmployeeId { get; set; }
        [Column("eted_first_name")]
        [StringLength(50)]
        public string EtedFirstName { get; set; }
        [Column("eted_last_name")]
        [StringLength(50)]
        public string EtedLastName { get; set; }
        [Column("eted_email_address")]
        [StringLength(50)]
        public string EtedEmailAddress { get; set; }
        [Column("eted_dob", TypeName = "date")]
        public DateTime? EtedDob { get; set; }
        [Column("eted_contact_number")]
        [StringLength(50)]
        public string EtedContactNumber { get; set; }
        [Column("eted_address")]
        [StringLength(50)]
        public string EtedAddress { get; set; }
        [Column("eted_gender")]
        [StringLength(50)]
        public string EtedGender { get; set; }
        [Column("eted_marital_status")]
        [StringLength(50)]
        public string EtedMaritalStatus { get; set; }
        [Column("eted_blood_group")]
        [StringLength(50)]
        public string EtedBloodGroup { get; set; }
        [Column("eted_photograph")]
        public string EtedPhotograph { get; set; }
        [Column("eted_cnic")]
        public int? EtedCnic { get; set; }
        [Column("eted_official_email_address")]
        public string EtedOfficialEmailAddress { get; set; }
        [Column("eted_religion")]
        public string EtedReligion { get; set; }
        [Column("eted_nationality")]
        public string EtedNationality { get; set; }
        [Column("eted_status")]
        public string EtedStatus { get; set; }
        [Required]
        [Column("eted_is_delete")]
        public bool? EtedIsDelete { get; set; }
        [Column("eted_created_by")]
        public string EtedCreatedBy { get; set; }
        [Column("eted_created_by_name")]
        public string EtedCreatedByName { get; set; }
        [Column("eted_created_by_date", TypeName = "datetime")]
        public DateTime? EtedCreatedByDate { get; set; }
        [Column("eted_modified_by")]
        public string EtedModifiedBy { get; set; }
        [Column("eted_modified_by_name")]
        public string EtedModifiedByName { get; set; }
        [Column("eted_modified_by_date", TypeName = "datetime")]
        public DateTime? EtedModifiedByDate { get; set; }

        [InverseProperty("EtedEmployee")]
        public virtual ICollection<EmsTblAcademicQualification> EmsTblAcademicQualification { get; set; }
        [InverseProperty("EtedEmployee")]
        public virtual ICollection<EmsTblEmergencyContact> EmsTblEmergencyContact { get; set; }
        [InverseProperty("EtedEmployee")]
        public virtual ICollection<EmsTblEmployeeProfessionalDetails> EmsTblEmployeeProfessionalDetails { get; set; }
        [InverseProperty("EtedEmployee")]
        public virtual ICollection<EmsTblProfessionalQualification> EmsTblProfessionalQualification { get; set; }
        [InverseProperty("EtedEmployee")]
        public virtual ICollection<EmsTblWorkingHistory> EmsTblWorkingHistory { get; set; }
    }
}
