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
    public interface IAssetService
    {
        BaseResponse createAsset(ImsAssetsVM asset);

        BaseResponse updateAsset(ImsAssetsVM asset );

        BaseResponse displayAllAssetUnAssigned(int type);

        BaseResponse getAssetByID(int id);

        BaseResponse deleteAsset(int assetid);

        BaseResponse sumOfAsset(int categoryid);

        BaseResponse totalQuantityAsset(int categoryid);


    }
}
