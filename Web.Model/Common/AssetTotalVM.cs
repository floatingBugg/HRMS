using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssetTotalVM
    {
        public int? quantity { get; set; }
        public long? cost { get; set; }

        public int? totalCost { get; set; }

        public long? subTotalCost { get; set; }
    }
}
