using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.Common;
using Web.Model.ViewModel;

namespace Web.Services.Interfaces
{
    public interface IAssetCategoryService
    {
        public BaseResponse CreateAssetCategory(ImsAssetsCategoryVM asset);

        public BaseResponse UpdateAssetCategory(ImsAssetsCategoryVM asset);

        public BaseResponse DeleteAssetCategory(int id);

        public BaseResponse DisplayAssetCategory();
    }
}
