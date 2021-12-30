using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class RmsTblRecruiterVM
    {
        public int RtrRecId { get; set; }
        public int? RtpaPosId { get; set; }
        public string RtrEmail { get; set; }
        public long? RtrPhoneNo { get; set; }
        public string RtrLastDegree { get; set; }
        public string RtrLastCompany { get; set; }
        public string RtrExperience { get; set; }
        public long? RtrCurrentSalary { get; set; }
        public long? RtrExpectedSalary { get; set; }
        public bool? RtrResume { get; set; }
        public int? RtrRecruitStatus { get; set; }
        public DateTime? RtrInterviewDateTime { get; set; }
        public bool? RtrInterviewStatus { get; set; }
        public string RtrRecruitStatusRemarks { get; set; }
        public bool? RtrShortList { get; set; }
        public DateTime? RtrExpectedDate { get; set; }
        public int? RtrSalaryNegotiation { get; set; }
        public string RtrRecommendedBy { get; set; }
        public string RtrRecommendedPersonRemarks { get; set; }
        public bool? RtrHiringStatus { get; set; }
        public string RtrCreatedBy { get; set; }
        public string RtrCreatedByName { get; set; }
        public DateTime? RtrCreatedByDate { get; set; }
        public string RtrModifiedBy { get; set; }
        public string RtrModifiedByName { get; set; }
        public DateTime? RtrModifiedByDate { get; set; }
        public bool? RtrIsDelete { get; set; }

    }
}
