using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssetAssignGrid
    {
        public int empid { get; set; }
        public int assetid { get; set; }
        public string assetname { get; set; }

        public string processor { get; set; }

        public string ram { get; set; }

        public string storage { get; set; }

        public string generation { get; set; }
        public string model { get; set; }
        public string companyname { get; set; }
        public int? quantity { get; set; }
        public string type { get; set; }
        
        public string assingeto { get; set; }
    }
}
