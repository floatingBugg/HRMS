using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces
{
    public interface IAssetFurnitureService
    {
        public BaseResponse CreateAssetFurniture(AssetFurnitureCredential furniture);

        public BaseResponse UpdateAssestFurniture(AssetFurnitureCredential furniture);

        public BaseResponse DeleteAssestFurniture(int id);
    }
}
