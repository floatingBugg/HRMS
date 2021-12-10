using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces
{
    public interface IAssetAcService
    {
        public BaseResponse CreateAssetAc(AssetAcCredential Ac);

        public BaseResponse UpdateAssestAc(AssetAcCredential Ac);

        public BaseResponse DeleteAssestAc(int id);
    }
}

