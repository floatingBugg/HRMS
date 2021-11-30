using System.Collections.Generic;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces

{
    public interface IEmployeeService
    {
        string CreateEmployee(EmsTblEmployeeDetails employee);

        string UpdateEmployee(EmsTblEmployeeDetails employee);

        BaseResponse GetAllEmployee();

        string DeleteEmployee(int id);
    }
}
