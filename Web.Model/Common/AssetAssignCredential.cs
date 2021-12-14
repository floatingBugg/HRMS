using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssetAssignCredential
    {
        public int assignId { get; set; }
        public string assetname { get; set; }

        public int assetId { get; set; }

        public int quantity { get; set; }

        public int assigntoId { get; set; }
        public string assigntoName { get; set; }

        public string createdby { get; set; }
        public string creatbyname { get; set; }

        public DateTime createdbydate = new DateTime();
        public string modifiedby { get; set; }

        public string modifiedbyname { get; set; }
    }
}
