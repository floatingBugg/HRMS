using System.Collections.Generic;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces

{
    public interface IEmployeeService
    {
        BaseResponse CreateEmployee(EmsTblEmployeeDetails employee, string userName, string userId);

        BaseResponse UpdateEmployee(EmsTblEmployeeDetails employee ,string userName, string userId);

        BaseResponse GetAllEmployee();

        BaseResponse DeleteEmployee(int id ,string userName, string userId);

        BaseResponse EditEmployeeByid(int id );
    }
}
