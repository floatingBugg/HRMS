using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces
{
    public interface IAssetCategoryService
    {
        public BaseResponse CreateAssetCategory(AssetCategoryCredential asset);

        public BaseResponse UpdateAssetCategory(AssetCategoryCredential asset);

        public BaseResponse DeleteAssetCategory(int id);

        public BaseResponse DisplayAssetCategory();
    }
}
