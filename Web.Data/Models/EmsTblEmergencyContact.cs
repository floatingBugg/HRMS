﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Web.Data.Models
{
    public partial class EmsTblEmergencyContact
    {
        public int EtecEcId { get; set; }
        public int? EtecEtedEmployeeId { get; set; }
        public string EtecFirstName { get; set; }
        public string EtecLastName { get; set; }
        public string EtecRelation { get; set; }
        public string EtecContactNumber { get; set; }
        public string EtecAddress { get; set; }
        public string EtecCreatedBy { get; set; }
        public string EtecCreatedByName { get; set; }
        public DateTime? EtecCreatedByDate { get; set; }
        public string EtecModifiedBy { get; set; }
        public string EtecModifiedByName { get; set; }
        public DateTime? EtecModifiedByDate { get; set; }
        public bool? EtecIsDelete { get; set; }

        public virtual EmsTblEmployeeDetails EtecEtedEmployee { get; set; }
    }
}