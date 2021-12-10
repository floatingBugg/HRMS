﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces
{
    public interface IAssetService
    {

       

        public BaseResponse CreateAssetAc(AssetAcCredential Ac);

        public BaseResponse CreateAssetLaptop(AssetLaptopCredential laptop);
        public BaseResponse CreateAssetFurniture(AssetFurnitureCredential furniture);
        public BaseResponse CreateAssestLaptop(AssetLaptopCredential laptop);

        public BaseResponse UpdateAssestLaptop(AssetLaptopCredential laptop);
    }
}
