﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Web.Data.Models
{
    public partial class EmsTblWorkingHistory
    {
        public int EtwhWhId { get; set; }
        public int? EtwhEtedEmployeeId { get; set; }
        public string EtwhCompanyName { get; set; }
        public string EtwhDesignation { get; set; }
        public DateTime? EtwhStratDate { get; set; }
        public DateTime? EtwhEndDate { get; set; }
        public string EtwhDuration { get; set; }
        public string EtwhExperienceLetter { get; set; }
        public string EtwhCreatedBy { get; set; }
        public string EtwhCreatedByName { get; set; }
        public DateTime? EtwhCreatedByDate { get; set; }
        public string EtwhModifiedBy { get; set; }
        public string EtwhModifiedByName { get; set; }
        public DateTime? EtwhModifiedByDate { get; set; }
        public bool? EtwhIsDelete { get; set; }

        public virtual EmsTblEmployeeDetails EtwhEtedEmployee { get; set; }
    }
}