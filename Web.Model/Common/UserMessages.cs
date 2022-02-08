using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{

    public static class UserMessages
    {

        public static string strUserfound = "User Found";
        public static string strUsernotfound = "Invalid Username/Invalid Password";
        public static string strRefreshtoken = "Refresh Token Generated";
        public static string strStockEmpty = "Out of Stock/Zero Left";
        public static string strSuccess = "Success";
        public static string strError = "Error";
        public static string strAdded = "Data Added Successfully";
        public static string strDeleted = "Data Deleted successfully";
        public static string strUpdated = "Data Updated successfully";
        public static string strAlrdeleted = "Data Already Deleted";
        public static string strNotupdated = "Not Updated";
        public static string strEmailexist = "Email Already Exist";
        public static string strCnicexist = "Cnic Already Exist";
        public static string strNotinsert = "No Data Inserted";
        public static string strNotfound = "No Data Found";
        public static string strNoSick = "No Sick Leave Left";
        public static string strNoCasual = "No Casual Leave Left";
        public static string strNoAnnual = "No annual Leave Left";
        public static string strNoLeave = "No Leave Assigned";
      /*  public UserMessages(string value)
        {
            Value = value;
        }


        public string Value { get; private set; }

        public static UserMessages strSuccess { get { return new UserMessages("Success"); } }
        public static UserMessages strError { get { return new UserMessages("Error"); } }
        public static UserMessages strAdded { get { return new UserMessages("Data Added Succesfully"); } }
        public static UserMessages strDeleted { get { return new UserMessages("Data Deleted Successfuly"); } }
        public static UserMessages strUpdated { get { return new UserMessages("Data Updated Successfully"); } }*/

        
    }
}
