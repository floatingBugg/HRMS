using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model.ViewModel
{
    public class ImsAssetsCategoryVM
    {

        public int ItacCategoryId { get; set; }
        public string ItacCategoryName { get; set; }
        public string ItacCreatedBy { get; set; }
        public string ItacCreatedByName { get; set; }
        public DateTime? ItacCreatedByDate { get; set; }
        public string ItacModifiedBy { get; set; }
        public string ItacModifiedByName { get; set; }
        public DateTime? ItacModifiedByDate { get; set; }
        public bool? ItacIsDelete { get; set; }

    }
}
