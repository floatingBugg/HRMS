﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Web.Data.Models
{
    public partial class LmsEmployeeLeave
    {
        public int LmselLeaveId { get; set; }
        public int? LmselEtedEmployeeId { get; set; }
        public int? LmselLeaveType { get; set; }
        public DateTime? LmselStartDate { get; set; }
        public DateTime? LmselEndDate { get; set; }
        public int? LmselDays { get; set; }
        public string LmselCreatedBy { get; set; }
        public string LmselCreatedByName { get; set; }
        public DateTime? LmselCreatedByDate { get; set; }
        public bool? LmselIsDelete { get; set; }
        public string LmselReason { get; set; }

        public virtual EmsTblEmployeeDetails LmselEtedEmployee { get; set; }
        public virtual LmsLeaveType LmselLeaveTypeNavigation { get; set; }
    }
}