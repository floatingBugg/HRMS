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
        public int categoryid { get; set; }
        public string assetname { get; set; }
        public int? quantity { get; set; }
        public long? cost { get; set; }
        public int? remaining { get; set; }
        public int assignQuan { get; set; }
        public string serialno { get; set; }
        public string model { get; set; }
        public string type { get; set; }
        public string companyname { get; set; }
        public string size { get; set; }
        public string condition { get; set; }
        public string generation { get; set; }
        public string ram { get; set; }
        public string processor { get; set; }
        public string storage { get; set; }


        public string hardtype { get; set; }

        public DateTime purchaseddate { get; set; }

        public string createdby { get; set; }

        public string createdbyname { get; set; }

        public string modifiedby { get; set; }

        public string modifiedbyname { get; set; }

        

    }
}
