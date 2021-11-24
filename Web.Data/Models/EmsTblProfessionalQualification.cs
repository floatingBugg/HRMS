﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Web.DLL.Models
{
    [Table("ems_tbl_professional_qualification")]
    public partial class EmsTblProfessionalQualification
    {
        [Key]
        [Column("etpq_pq_id")]
        public int EtpqPqId { get; set; }
        [Required]
        [Column("etpq_certification")]
        [StringLength(100)]
        public string EtpqCertification { get; set; }
        [Column("etpq_strat_date", TypeName = "datetime")]
        public DateTime EtpqStratDate { get; set; }
        [Column("etpq_end_date", TypeName = "datetime")]
        public DateTime EtpqEndDate { get; set; }
        [Required]
        [Column("etpq_institute_name")]
        [StringLength(100)]
        public string EtpqInstituteName { get; set; }
        [Required]
        [Column("etpq_documents")]
        public byte[] EtpqDocuments { get; set; }
        [Column("eted_employee_id")]
        public int EtedEmployeeId { get; set; }
        [Required]
        [Column("etpq_created_by")]
        [StringLength(100)]
        public string EtpqCreatedBy { get; set; }
        [Required]
        [Column("etpq_created_by_name")]
        [StringLength(100)]
        public string EtpqCreatedByName { get; set; }
        [Column("etpq_created_by_date", TypeName = "datetime")]
        public DateTime EtpqCreatedByDate { get; set; }
        [Required]
        [Column("etpq_modified_by")]
        [StringLength(100)]
        public string EtpqModifiedBy { get; set; }
        [Required]
        [Column("etpq_modified_by_name")]
        [StringLength(100)]
        public string EtpqModifiedByName { get; set; }
        [Column("etpq_modified_by_date", TypeName = "datetime")]
        public DateTime EtpqModifiedByDate { get; set; }
        [Required]
        [Column("etpq_is_delete")]
        [StringLength(100)]
        public string EtpqIsDelete { get; set; }

        [ForeignKey(nameof(EtedEmployeeId))]
        [InverseProperty(nameof(EmsTblEmployeeDetails.EmsTblProfessionalQualification))]
        public virtual EmsTblEmployeeDetails EtedEmployee { get; set; }
    }
}