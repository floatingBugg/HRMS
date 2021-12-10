using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.Common
{
    public class AssestLaptopCredential
    {
        public int assestID { get; set; }
        public string assestName { get; set; }

        public int quantity { get; set; }

        public int cost { get; set; }

        public DateTime purchaseDate = new DateTime();

        public int assignedTo { get; set; }

        public string serialno { get; set; }

        public string generation { get; set; }

        public string ram { get; set; }

        public string processor { get; set; }

        public string hdd { get; set; }

        public string companyName { get; set; }


    }
}
