using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class DisplayAssetAssignCredential
    {
        public int ItasAssignId { get; set; }
        public int assignid { get; set; }
        public int assetid { get; set; }
        public string categoryname { get; set; }
        public int? itasItaAssetId { get; set; }
        public string assetName { get; set; }

        public int? itasItacCategoryId { get; set; }

        public string assetCatagoryName { get; set; }
        
        public int? itasQuantity { get; set; }
        public int? quantity { get; set; }
        public string assingedname { get; set; }

        public DateTime? assingedDate { get; set; }

        public DateTime? itasAssignedDate { get; set; }
    }
}
