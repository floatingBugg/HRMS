﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Web.Data.Models
{
    public partial class EmsTblRoles
    {
        public EmsTblRoles()
        {
            EmsTblHrmsUser = new HashSet<EmsTblHrmsUser>();
        }

        public int EtrRoleId { get; set; }
        public string EtrRoleName { get; set; }
        public string EtrCreatedBy { get; set; }
        public string EtrCreatedByName { get; set; }
        public DateTime? EtrCreatedByDate { get; set; }
        public bool? EtrIsDelete { get; set; }

        public virtual ICollection<EmsTblHrmsUser> EmsTblHrmsUser { get; set; }
    }
}