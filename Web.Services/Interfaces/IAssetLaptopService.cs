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

        public BaseResponse CreateAssetLaptop(AssetLaptopCredential laptop);

        public BaseResponse UpdateAssestLaptop(AssetLaptopCredential laptop);

        public BaseResponse DeleteAssestLaptop(int id);

        public BaseResponse GetAllAssestLaptop(int id);

        public BaseResponse GetLaptopbyID(int id);


    }
}
