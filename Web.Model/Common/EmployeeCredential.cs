using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class EmployeeCredential
    {
        public int empID { get; set; }
        public string firstname { get; set; }
        public string Lastname { get; set; }
        public string photograph { get; set; }
        public string personalemail { get; set; }

        public string officialemail { get; set; }
        public int cnic { get; set; }
        public DateTime dob { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string martialstatus { get; set; }
        public string bloodgroup { get; set; }
        public string religion { get; set; }
        public string nationality { get; set; }
        public string empstatus { get; set; }
        public string created { get; set; }
        public string createdName { get; set; }

        public DateTime createdDate = new DateTime();
        public string modified { get; set; }
        public string modifiedName { get; set; }

        public DateTime modifiedDate = new DateTime();
        public string isDelete { get; set; }

        //Academic Qualification

        
        public string Qualification { get; set; }
        public string PassingYear { get; set; }
        public string Cgpa { get; set; }
        public string InstituteName{get;set;}

        public string UploadDocuments { get; set; }

        //Professional Qualification
       
        public string Certification { get; set; }

        public DateTime StartDate = new DateTime();
        
        public DateTime EndDate = new DateTime();
        

        public string Documents { get; set; }

       

        //Emergency Contact
        public string emergencyfirstname { get; set; }

        public string emergencylastname { get; set; }

        public string emergencyrelation { get; set; }

        public string emergencycontact { get; set; }

        public string emergencyaddress { get; set; }
        // Working History
        public string companyname { get; set; }

        public string Olddesignation { get; set; }

        public DateTime startdate { get; set; }

        public DateTime enddate { get; set; }

        public string duration { get; set; }

        public string expletter { get; set; }


        //Professional Details

        

        public string Salary { get; set; }

        public string NewDesignation { get; set; }
        public DateTime JoiningDate { get; set; }

        public string Probation { get; set; }



    }

    public class AllTableDetails
    {
        public List<EmployeeCredential> _myPrEmployeeCredentialoperty { get; set; }
    }
}
