using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web.API.Helper;
using Web.Data.Models;
using Web.Model;
using Web.Services.Interfaces;


namespace Web.API.Controllers
{
   // [Authorize]
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
            _logger = new Logger(_hostEnvironment);
        }

        [HttpGet("/Employee/DisplayAllEmployees")]
        public BaseResponse GetAllEmployee()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _employeeservice.GetAllEmployee();
                 return response;
            }
            catch(Exception ex)
            {
                _logger.LogExceptions(ex);
                response.Data = null;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        [HttpGet("/Employee/Edit")]
        public BaseResponse EditEmployee(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _employeeservice.EditEmployeeByid(id);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                response.Data = null;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        [HttpPost("/Employee/Add")]
        public BaseResponse Create([FromBody]EmsTblEmployeeDetails employee, string userName=null, string userId=null)
        {
            BaseResponse response = new BaseResponse(); 
            try
            {
                var test = ModelState;
                response = _employeeservice.CreateEmployee(employee, userName, userId);
               
                    
                
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogExceptions(ex);
                response.Data = null;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }

        [HttpPost("/Employee/Update")]
        public BaseResponse Update([FromBody] EmsTblEmployeeDetails employee)
        {
            BaseResponse response = new BaseResponse();
            
            try
            {
               
                response = _employeeservice.UpdateEmployee(employee);                  
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);           
                return response;
            }
        }

        [HttpDelete("/Employee/Remove")]
        public BaseResponse Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {          
                response  = _employeeservice.DeleteEmployee(id);
              
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogExceptions(ex);
                response.Data = null;
                response.Message = ex.Message;
                response.Success = false;
                return response;
            }
        }
    }
}
