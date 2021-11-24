
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Model.Common;

namespace Web.Services.Interfaces

{
    public interface IEmployeeService
    {
        string CreateEmployee(EmployeeCredential employee);

        string UpdateEmployee(EmployeeCredential employee);

        string GetAllEmployee();
    }
}