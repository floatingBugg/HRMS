using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class DisplayAssetAssignCredential
    {
        public int assignid { get; set; }
        public int assetid { get; set; }
        public string assetname { get; set; }
        public int? quantity { get; set; }
        public string assingedname { get; set; }
    }
}
