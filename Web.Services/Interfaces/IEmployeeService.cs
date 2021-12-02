using System.Collections.Generic;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces

{
    public interface IEmployeeService
    {
        BaseResponse CreateEmployee(EmsTblEmployeeDetails employee);

        BaseResponse UpdateEmployee(EmsTblEmployeeDetails employee);

        BaseResponse GetAllEmployee();

        BaseResponse DeleteEmployee(int id);

        BaseResponse EditEmployeeByid(int id);
    }
}
