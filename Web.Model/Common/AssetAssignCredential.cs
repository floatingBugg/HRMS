using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssetAssignCredential
    {
        public int assignid { get; set; }
        public int assetid { get; set; }
        public int categoryid { get; set; }
        public int empid { get; set; }
        public int quantity { get; set; }
        
        public string createdby { get; set; }

        public string createdbyname { get; set; }

        public string modifiedby { get; set; }

        public string modifiedbyname { get; set; }

        public DateTime assigndate { get; set; }
        public DateTime purchaseddate { get; set; }
    }
}
