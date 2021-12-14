﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Web.Data.Models
{
    public partial class EmsTblEmployeeDetails
    {
        public EmsTblEmployeeDetails()
        {
            EmsTblAcademicQualification = new HashSet<EmsTblAcademicQualification>();
            EmsTblEmergencyContact = new HashSet<EmsTblEmergencyContact>();
            EmsTblEmployeeProfessionalDetails = new HashSet<EmsTblEmployeeProfessionalDetails>();
            EmsTblProfessionalQualification = new HashSet<EmsTblProfessionalQualification>();
            EmsTblWorkingHistory = new HashSet<EmsTblWorkingHistory>();
            ImsTblAssign = new HashSet<ImsTblAssign>();
        }

        public int EtedEmployeeId { get; set; }
        public string EtedFirstName { get; set; }
        public string EtedLastName { get; set; }
        public string EtedEmailAddress { get; set; }
        public DateTime? EtedDob { get; set; }
        public string EtedContactNumber { get; set; }
        public string EtedAddress { get; set; }
        public string EtedGender { get; set; }
        public string EtedMaritalStatus { get; set; }
        public string EtedBloodGroup { get; set; }
        public string EtedPhotograph { get; set; }
        public long? EtedCnic { get; set; }
        public string EtedOfficialEmailAddress { get; set; }
        public string EtedReligion { get; set; }
        public string EtedNationality { get; set; }
        public string EtedStatus { get; set; }
        public string EtedCreatedBy { get; set; }
        public string EtedCreatedByName { get; set; }
        public DateTime? EtedCreatedByDate { get; set; }
        public string EtedModifiedBy { get; set; }
        public string EtedModifiedByName { get; set; }
        public DateTime? EtedModifiedByDate { get; set; }
        public bool? EtedIsDelete { get; set; }

        public virtual ICollection<EmsTblAcademicQualification> EmsTblAcademicQualification { get; set; }
        public virtual ICollection<EmsTblEmergencyContact> EmsTblEmergencyContact { get; set; }
        public virtual ICollection<EmsTblEmployeeProfessionalDetails> EmsTblEmployeeProfessionalDetails { get; set; }
        public virtual ICollection<EmsTblProfessionalQualification> EmsTblProfessionalQualification { get; set; }
        public virtual ICollection<EmsTblWorkingHistory> EmsTblWorkingHistory { get; set; }
        public virtual ICollection<ImsTblAssign> ImsTblAssign { get; set; }
    }
}