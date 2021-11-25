using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services.Concrete;
using Web.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using log4net.Repository.Hierarchy;
using Web.Model.Common;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.API.Controllers
{
    [Route("Employee")]
    [ApiController]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService _employeeservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public EmployeeController(IEmployeeService employeeservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _employeeservice = employeeservice;
            _config = config;
            _hostEnvironment = environment;
        }
        // GET: api/<EmployeeController>
        [HttpGet("/DisplayAllEmployees")]
        public IActionResult Get()
        {
             
            var emp =_employeeservice.GetAllEmployee();

            return Ok(emp);
        }

        //[HttpGet("/DisplayAllEmployeesEmergencyContact")]
        //public IActionResult GetEmergencyContact()
        //{

        //    var emp = _employeeservice.GetAllEmployee;

        //    return Ok(emp);
        //}

        [HttpPost("/Employee/Add")]
        public String Create([FromBody] EmployeeCredential employee)
        {
            String message = "Success";
            try
            {
                
                string result = _employeeservice.CreateEmployee(employee);
                
                return message;
            }
            catch(Exception ex)
            {
                
                return null;
            }
            return message;
        }

        [HttpPost("/Update")]
        public String Update([FromBody] EmployeeCredential employee)
        {
            String message = "Success";
            try
            {
               
                string result = _employeeservice.UpdateEmployee(employee);

                return message;
            }
            catch (Exception ex)
            {

                return null;
            }
            return message;
        }

        [HttpDelete("/Remove")]
        public string Delete(int id)
        {
            return _employeeservice.DeleteEmployee(id);
        }
    }
}
