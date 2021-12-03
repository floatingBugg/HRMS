using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models;
using Web.Model;

namespace Web.Services.Interfaces
{
    public interface IInventoryService
    {

        BaseResponse CreateAssests(ImsTblAssetsCategory assests);

        BaseResponse UpdateAssests(ImsTblAssetsCategory assests);

        BaseResponse GetAllAssets();

        BaseResponse DeleteAssests(int id);

        BaseResponse EditAssetById(int id);
    }
}
