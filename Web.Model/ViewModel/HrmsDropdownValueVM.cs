using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class HrmsDropdownValueVM
    {
        public int HdvValueId { get; set; }
        public int? HdvHdDropdownId { get; set; }
        public string HdvValueName { get; set; }
        public string HdvCreatedBy { get; set; }
        public string HdvCreatedByName { get; set; }
        public DateTime? HdvCreatedByDate { get; set; }
        public bool? HdvIsDelete { get; set; }
    }
}
