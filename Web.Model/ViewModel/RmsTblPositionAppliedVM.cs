using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class RmsTblPositionAppliedVM
    {
        public int RtpaPosId { get; set; }
        public string RtpaPosition { get; set; }
        public string RtpaCreatedBy { get; set; }
        public string RtpaCreatedByName { get; set; }
        public DateTime? RtpaCreatedByDate { get; set; }
        public string RtpaModifiedBy { get; set; }
        public string RtpaModifiedByName { get; set; }
        public DateTime? RtpaModifiedByDate { get; set; }
        public bool? RtpaIsDelete { get; set; }

    }
}
