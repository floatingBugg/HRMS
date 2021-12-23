using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class ImsAssignVM
    {
        public int ItasAssignId { get; set; }
        public int? ItasItaAssetId { get; set; }
        public int? ItasEtedEmployeeId { get; set; }
        public int? ItasItacCategoryId { get; set; }
        public int? ItasQuantity { get; set; }
        public DateTime? ItasAssignedDate { get; set; }
        public string ItasCreatedBy { get; set; }
        public string ItasCreatedByName { get; set; }
        public DateTime? ItasCreatedByDate { get; set; }
        public string ItasModifiedBy { get; set; }
        public string ItasModifiedByName { get; set; }
        public DateTime? ItasModifiedByDate { get; set; }
        public bool? ItasIsDelete { get; set; }
    }
}
