using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssetAcCredential
    {
        public int assestID { get; set; }
        public string assestName { get; set; }

        public int quantity { get; set; }

        public int cost { get; set; }

        public DateTime purchaseDate = new DateTime();

        public int assignedTo { get; set; }

        public string companyname { get; set; }

        public string size { get; set; }

    }
}
