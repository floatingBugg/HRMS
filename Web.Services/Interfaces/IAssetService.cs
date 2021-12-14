﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces
{
    public interface IAssetService 
    {
        public BaseResponse CreateAssetCategory(AssetCategoryCredential category);

        public BaseResponse GetAssetcategorybyID(int id);

        public BaseResponse GetAllAssetCategory();

        public BaseResponse CreateAsset(AssetCredential assets);

        public BaseResponse UpdateAssestCategory(AssetCategoryCredential category);

        public BaseResponse DeleteAssestCategory(int id);

        public BaseResponse UpdateAsset(AssetCredential assets);

        public BaseResponse DeleteAsset(int id);

        public BaseResponse GetAllAsset(int id);

        public BaseResponse GetAssetbyID(int id);
    }
}
