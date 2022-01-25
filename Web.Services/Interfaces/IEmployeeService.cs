using System.Collections.Generic;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
using Web.Model.ViewModel;

namespace Web.Services.Interfaces

{
    public interface IEmployeeService
    {
        public BaseResponse CreateEmployee(EmsTblEmployeeDetailsVM employee,string rootpath);

        public BaseResponse UpdateEmployee(EmsTblEmployeeDetailsVM employee,string rootpath );

        public BaseResponse GetAllEmployee();
        public BaseResponse GetAllEmployee(int roleid,int empid);

        public BaseResponse DeleteEmployee(int id);

        public BaseResponse ViewDataEmployeeByid(int id);

        public BaseResponse CreateDropdownvalue(HrmsDropdownValueVM value);

        public BaseResponse getDropdownValuesByid(int id);

        public BaseResponse getEmployeeUnderTeamLeadByid(int empid);
    }
}
