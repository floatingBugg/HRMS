using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class UnAssignedAssetEmployeeGrid
    {
        public int assetid { get; set; }
        public int? categoryid { get; set; }
        public string category { get; set; }
        public string assetname { get; set; }

    }
}
