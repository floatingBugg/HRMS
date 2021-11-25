
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DLL.Models;
using Web.Model.Common;

namespace Web.Services.Interfaces

{
    public interface IEmployeeService
    {
        string CreateEmployee(EmployeeCredential employee);

        string UpdateEmployee(EmployeeCredential employee);

        IEnumerable<EmsTblEmployeeDetails> GetAllEmployee();


        IEnumerable<EmsTblEmployeeDetails> GetAllEmployeeContact();


        string DeleteEmployee(int id);
    }
}
