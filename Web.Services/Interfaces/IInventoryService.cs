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

        BaseResponse CreateAssets(ImsTblAssests assests);

        BaseResponse UpdateEmployee(ImsTblAssests assests);

        BaseResponse GetAllEmployee();

        BaseResponse DeleteEmployee(int id);
    }
}
