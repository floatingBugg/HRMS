﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web.Data.Models
{
    public partial class EmsTblHrmsUser
    {
        public int EthuUserId { get; set; }
        public int EtrEthuRoleId { get; set; }
        public string EthuFullName { get; set; }
        public string EthuUserName { get; set; }
        public bool? EthuUserStatus { get; set; }
        public string EthuEmailAddress { get; set; }
        public string EthuPhoneNumber { get; set; }
        public string EthuPassword { get; set; }
        public string EthuGender { get; set; }
        public int EtedEthuEmpId { get; set; }
        public string EthuCreatedBy { get; set; }
        public string EthuCreatedByName { get; set; }
        public DateTime? EthuCreatedByDate { get; set; }
        public string EthuModifiedBy { get; set; }
        public string EthuModifiedByName { get; set; }
        public DateTime? EthuModifiedByDate { get; set; }
        public bool? EthuIsDelete { get; set; }
        [JsonIgnore]
        public virtual EmsTblEmployeeDetails EtedEthuEmp { get; set; }
        [JsonIgnore]
        public virtual EmsTblRoles EtrEthuRole { get; set; }
    }
}