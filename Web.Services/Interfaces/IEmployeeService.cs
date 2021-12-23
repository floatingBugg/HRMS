using System.Collections.Generic;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
using Web.Model.ViewModel;

namespace Web.Services.Interfaces

{
    public interface IEmployeeService
    {
        BaseResponse CreateEmployee(EmsTblEmployeeDetailsVM employee,string rootpath);

        BaseResponse UpdateEmployee(EmsTblEmployeeDetailsVM employee,string rootpath );

        BaseResponse GetAllEmployee();

        BaseResponse DeleteEmployee(int id);

        BaseResponse ViewDataEmployeeByid(int id);
    }
}
