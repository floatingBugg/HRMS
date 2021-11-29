using System.Collections.Generic;
using Web.Model.Common;

namespace Web.Services.Interfaces

{
    public interface IEmployeeService
    {
        string CreateEmployee(EmployeeCredential employee);

        string UpdateEmployee(EmployeeCredential employee);

        List<DisplayEmployeeGrid> GetAllEmployee();

        string DeleteEmployee(int id);
    }
}
