using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssetLaptopCredential
    {
        public int assestID { get; set; }
        public string assetName { get; set; }

        public int quantity { get; set; }

        public int cost { get; set; }

        public DateTime purchaseDate = new DateTime();

        public string model { get; set; }

        public string type { get; set; }

        public string description { get; set; }
   


        public int categoryId { get; set; }

        public string serialno { get; set; }

        public string generation { get; set; }

        public string ram { get; set; }

        public string processor { get; set; }

        public string hdd { get; set; }

        public string companyName { get; set; }

        public string createdby { get; set; }

        public string createdbyname { get; set; }

        public string modifiedby { get; set; }

        public string modifiedbyname { get; set; }

    }
}
