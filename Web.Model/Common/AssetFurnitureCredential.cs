using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssetFurnitureCredential
    {
        public int assetID { get; set; }
        public string assetName { get; set; }
        public string type { get; set; }
        public int quantity { get; set; }
        public int cost { get; set; }

        public DateTime purchaseDate = new DateTime();
        public int assignedTo { get; set; }
    }
}
