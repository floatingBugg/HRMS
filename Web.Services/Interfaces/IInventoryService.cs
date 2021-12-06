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

        BaseResponse CreateAssests(ImsTblAssetsCategory assests,string userName,string userId);

        BaseResponse UpdateAssests(ImsTblAssetsCategory assests,string userName,string userId);

        BaseResponse GetAllAssets();

        BaseResponse DeleteAssests(int id,string userName,string userId);

        BaseResponse ViewAssetById(int id);
    }
}
