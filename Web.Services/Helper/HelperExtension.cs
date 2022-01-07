using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Services.Helper
{
    public static class HelperExtension
    {

        public static string getFileExtension(this string FileType)
        {
            string a="";
            if(FileType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                a = ".docx";
            }
            else if (FileType == "application/msword")
            {
                a= ".doc";
            }
            else if (FileType == "application/jpeg" || FileType == "application/jpg" || FileType == "application/png")
            {
                a= ".jpg";
            }
            else if (FileType == "application/pdf")
            {
                a= ".pdf";
            }
            return a;

        }
    }
}
