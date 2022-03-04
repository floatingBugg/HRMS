using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class EmsTblEmployeeDetailsVM
    {
        public int? EtedManagerId { get; set; }
        public int EtedEmployeeId { get; set; }
        public int EtedEthuEmpId { get; set; }
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
        public string EthuPassword { get; set; }
        public int EtrEthuRoleId { get; set; }
        public string EtedCreatedBy { get; set; }
        public string EtedCreatedByName { get; set; }
        public DateTime? EtedCreatedByDate { get; set; }
        public string EtedModifiedBy { get; set; }
        public string EtedModifiedByName { get; set; }
        public DateTime? EtedModifiedByDate { get; set; }
        public bool? EtedIsDelete { get; set; }

        public List<EmsTblAcademicQualificationVM> EmsTblAcademicQualification { get; set; }
        public List<EmsTblEmergencyContactVM> EmsTblEmergencyContact { get; set; }
        public List<EmsTblEmployeeProfessionalDetailsVM> EmsTblEmployeeProfessionalDetails { get; set; }
        public List<EmsTblProfessionalQualificationVM> EmsTblProfessionalQualification { get; set; }
        public List<EmsTblWorkingHistoryVM> EmsTblWorkingHistory { get; set; }
        public List<ImsAssignVM> ImsAssign { get; set; }
        public List<EmsEmployementStatusVM> EmsTblPermanentEmployee { get; set; }
        //public List<LmsEmployeeLeaveVM> Empleaveassign { get; set; }
        public List<LmsLeaveRecordVM> Empleaveassign { get; set; }
    }
}
