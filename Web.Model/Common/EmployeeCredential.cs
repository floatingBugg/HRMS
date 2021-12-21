using System;
using System.Collections.Generic;

namespace Web.Model.Common
{
    public class EmployeeCredential
    {
        public int empID { get; set; }
        public string firstname { get; set; }
        public string Lastname { get; set; }
        public string personalemail { get; set; }
        public string officialemail { get; set; }
        public long cnic { get; set; }
        public DateTime dob { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string martialstatus { get; set; }
        public string bloodgroup { get; set; }
        public string religion { get; set; }
        public string nationality { get; set; }
        public string empstatus { get; set; }
        public string imageUrlPhoto { get; set; }

        public byte[] photograph { get; set; }

        public string created { get; set; }
        public string createdName { get; set; }

        public DateTime createdDate = new DateTime();
        public string modified { get; set; }
        public string modifiedName { get; set; }

        public DateTime modifiedDate = new DateTime();
        public bool isDelete { get; set; }

        //Academic Qualification


        public string Qualification { get; set; }
        public long PassingYear { get; set; }
        public float Cgpa { get; set; }
        public string AcademicInstituteName { get; set; }

        public string UploadDocuments { get; set; }
        public byte[] UploadDocumentAcad { get; set; }

        //Professional Qualification

        public string certification { get; set; }

        public DateTime profstartDate { get; set; }

        public DateTime profendDate { get; set; }

        public string ProfessionalInstituteName { get; set; }
        public string Documents { get; set; }

        public byte[] DocumentsProfQual { get; set; }



        //Emergency Contact
        public string emergencyfirstname { get; set; }

        public string emergencylastname { get; set; }

        public string emergencyrelation { get; set; }

        public string emergencycontact { get; set; }

        public string emergencyaddress { get; set; }
        // Working History
        public string companyname { get; set; }

        public string workdesignation { get; set; }

        public DateTime workstartdate { get; set; }

        public DateTime workenddate { get; set; }

        public string duration { get; set; }

        public string expletter { get; set; }

        public byte[] workexpletter { get; set; }

        //Professional Details


        public string Salary { get; set; }

        public string profdesignation { get; set; }
        public DateTime JoiningDate { get; set; }

        public string Probation { get; set; }

    }
}
