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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EmployeeController>/5
 /*       [HttpGet("{id}")]
        public string Get(int id)
        {

            return "value";
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }*/

        // PUT api/<EmployeeController>/5
        
        // DELETE api/<EmployeeController>/5
        [HttpPost("Employee/add")]
        public String Create([FromBody] EmployeeCredential employee)
        {
            String message = "Success";
            try
            {
                IActionResult response = Unauthorized();
                string result = _employeeservice.CreateEmployee(employee);
                
                return message;
            }
            catch(Exception ex)
            {
                
                return null;
            }
            return message;
        }
    }
}
