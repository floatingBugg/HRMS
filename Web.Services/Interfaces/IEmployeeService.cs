using System.Collections.Generic;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;

namespace Web.Services.Interfaces

{
    public interface IEmployeeService
    {
        BaseResponse CreateEmployee(EmployeeCredential employee,string rootpath);

        BaseResponse UpdateEmployee(EmployeeCredential employee );

        BaseResponse GetAllEmployee();

        BaseResponse DeleteEmployee(int id);

        BaseResponse ViewDataEmployeeByid(int id);
    }
}
