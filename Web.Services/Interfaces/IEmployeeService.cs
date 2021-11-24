using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Services.Interfaces
{
    public interface IEmployeeService
    {
        string CreateEmployee();

        string UpdateEmployee();

        string GetAllEmployee();
    }
}
