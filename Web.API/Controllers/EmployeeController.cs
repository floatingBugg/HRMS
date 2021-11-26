﻿using Microsoft.Extensions.Logging;
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
using Web.DLL.Models;
using Web.Model;


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
       /* [HttpGet("/DisplayAllEmployees")]
        public IEnumerable<AllTableDetails> Get()
        {
             
            return _employeeservice.GetAllEmployee();
        }*/

        //[HttpGet("/DisplayAllEmployeesEmergencyContact")]
        //public IActionResult GetEmergencyContact()
        //{

        //    var emp = _employeeservice.GetAllEmployee;

        //    return Ok(emp);
        //}

        [HttpPost("/Employee/Add")]
        public BaseResponse Create([FromBody] EmployeeCredential employee)
        {
            BaseResponse response = new BaseResponse(); 
            try
            {
                
                response.Data = _employeeservice.CreateEmployee(employee);
                if (response.Data !=null) 
                {
                    response.Message = "Success";
                }
                
                return response;
            }
            catch(Exception ex)
            {
                
                return null;
            }
            return response;
        }

        [HttpPost("/Employee/Update")]
        public BaseResponse Update([FromBody] EmployeeCredential employee)
        {
            BaseResponse response = new BaseResponse();
            
            try
            {
               
                response.Data = _employeeservice.UpdateEmployee(employee);
                if(response.Data!= null)
                {
                    response.Message = "Success";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Failed";
                return null;
            }
            return response;
        }

        [HttpDelete("/Employee/Remove")]
        public BaseResponse Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            response.Data= _employeeservice.DeleteEmployee(id);
             response.Message = "False";
            return response;
        }
    }
}
