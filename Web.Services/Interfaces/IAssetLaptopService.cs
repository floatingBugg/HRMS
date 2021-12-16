using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces
{
    public interface IAssetLaptopService
    {
        BaseResponse creatLaptop(AssetLaptopCredential asset);

        BaseResponse updatelaptop(AssetLaptopCredential asset);

        BaseResponse displayAllLaptop(int type);

        BaseResponse getLaptopByID(int id);

        BaseResponse deleteLaptop(int assetid);

        BaseResponse sumOfLaptop(int categoryid);

        BaseResponse totalQuantityLaptop(int categoryid);


    }
}
