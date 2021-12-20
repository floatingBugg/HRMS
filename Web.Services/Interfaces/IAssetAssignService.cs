using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces
{
    public interface IAssetAssignService
    {
        BaseResponse createAssign(AssetCredential assign);

        BaseResponse updateAssign(AssetCredential asset);

        BaseResponse deleteAssign(int assetid);

        BaseResponse displayAllAssetAssigned(int type);

    }
}
