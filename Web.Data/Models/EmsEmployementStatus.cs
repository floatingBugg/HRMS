﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Web.Data.Models
{
    public partial class EmsEmployementStatus
    {
        public int EesEmployementId { get; set; }
        public int EesEtedEmployeeId { get; set; }
        public int EesEcsEmpstatusId { get; set; }
        public DateTime? EesStartDate { get; set; }
        public DateTime? EesEndDate { get; set; }
        public DateTime? EesClearenceDate { get; set; }
        public int? EesIncrement { get; set; }
        public string EesDateOfIncrement { get; set; }
        public string EesRemarks { get; set; }
        public string EesDuration { get; set; }
        public string EesContractType { get; set; }
        public int EesSalary { get; set; }
        public string EesCreatedBy { get; set; }
        public string EesCreatedByName { get; set; }
        public DateTime? EesCreatedByDate { get; set; }
        public string EesModifiedBy { get; set; }
        public string EesModifiedByName { get; set; }
        public DateTime? EesModifiedByDate { get; set; }
        public bool? EesIsDelete { get; set; }

        public virtual EmsCategoryStatus EesEcsEmpstatus { get; set; }
        public virtual EmsTblEmployeeDetails EesEcsEmpstatusNavigation { get; set; }
    }
}