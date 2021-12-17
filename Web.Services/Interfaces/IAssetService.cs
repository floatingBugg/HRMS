using System;
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
        BaseResponse createAsset(AssetCredential asset);

        BaseResponse updateAsset(AssetCredential asset);

        BaseResponse displayAllAssetUnAssigned(int type);

        BaseResponse getAssetByID(int id);

        BaseResponse deleteAsset(int assetid);

        BaseResponse sumOfAsset(int categoryid);

        BaseResponse totalQuantityAsset(int categoryid);


    }
}
