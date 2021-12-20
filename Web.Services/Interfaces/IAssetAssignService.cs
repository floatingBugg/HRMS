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
        BaseResponse createAssign(AssetAssignCredential assign);

        BaseResponse updateAssign(AssetAssignCredential assign);

        BaseResponse deleteAssign(int assetid);

        BaseResponse displayAllAssetAssigned(int type);

    }
}
