using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web.Data.Models
{
    [Table("ems_tbl_hrms_user")]
    public partial class EmsTblHrmsUser
    {
        [Key]
        [Column("ethu_user_id")]
        public int EthuUserId { get; set; }
        [Column("ethu_full_name")]
        public string EthuFullName { get; set; }
        [Column("ethu_user_name")]
        public string EthuUserName { get; set; }
        [Column("ethu_email_address")]
        public string EthuEmailAddress { get; set; }
        [Column("ethu_phone_number")]
        public string EthuPhoneNumber { get; set; }
        [Column("ethu_password")]
        public string EthuPassword { get; set; }
        [Column("ethu_gender")]
        public string EthuGender { get; set; }
        [Required]
        [Column("ethu_is_delete")]
        public bool? EthuIsDelete { get; set; }
        [Column("ethu_created_by")]
        public string EthuCreatedBy { get; set; }
        [Column("ethu_created_by_name")]
        public string EthuCreatedByName { get; set; }
        [Column("ethu_created_by_date", TypeName = "datetime")]
        public DateTime? EthuCreatedByDate { get; set; }
        [Column("ethu_modified_by")]
        public string EthuModifiedBy { get; set; }
        [Column("ethu_modified_by_name")]
        public string EthuModifiedByName { get; set; }
        [Column("ethu_modified_by_date", TypeName = "datetime")]
        public DateTime? EthuModifiedByDate { get; set; }
    }
}
