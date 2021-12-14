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
        public BaseResponse AssignAsset(AssetAssignCredential assign);
        public BaseResponse DeleteAssetAssign(int id);

        public BaseResponse UpdateAssignAsset(AssetAssignCredential assign);
        public BaseResponse DisplayAllAssign();
    }
}
