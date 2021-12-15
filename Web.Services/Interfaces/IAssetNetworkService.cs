using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces
{
    public interface IAssetNetworkService
    {
        public BaseResponse CreateAssetnetwork(AssetCredential network);

        public BaseResponse UpdateAssetNetwork(AssetCredential network);

        public BaseResponse DeleteAssetNetwork(int id);

        public BaseResponse DisplayAssetNetwork();
    }
}
