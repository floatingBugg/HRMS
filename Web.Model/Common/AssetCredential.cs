using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssetCredential
    {
    

        public int assetid { get; set; }
        public string assetname { get; set; }
        public int categoryid { get; set; }
        public int assignid { get; set; }
        public int quantity { get; set; }
        public int cost { get; set; }
        public string serialno { get; set; }

        public string model { get; set; }
        public string type { get; set; }

        public string companyname { get; set; }

        public string assingedname { get; set; }
        public string description { get; set; }

        public DateTime purchaseddate = new DateTime();

        public string createdby { get; set; }

        public string createdbyname { get; set; }

        public string modifiedby { get; set; }

        public string modifiedbyname { get; set; }

    }
}
