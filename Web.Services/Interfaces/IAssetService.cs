using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models;
using Web.Model;

namespace Web.Services.Interfaces
{
    public interface IAssetService
    {
        public BaseResponse CreateAssestLaptop(ImsAssets laptop);
    }
}
