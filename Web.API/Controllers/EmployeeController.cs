using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web.API.Helper;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
using Web.Services.Interfaces;


namespace Web.API.Controllers
{
    /*[Authorize]*/
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

        [HttpGet("/Employee/GetEmployeebyID")]
        public BaseResponse ViewEmployeeDataByid(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _employeeservice.ViewDataEmployeeByid(id);
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

        [HttpPost("/Employee/AddEmployee")]
        public BaseResponse Create([FromBody] EmsTblEmployeeDetailsVM employee)
        {
            string projectRootPath = _hostEnvironment.WebRootPath;
            BaseResponse response = new BaseResponse(); 
            try
            {
                var test = ModelState;
                response = _employeeservice.CreateEmployee(employee,projectRootPath);
               
                    
                
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

        [HttpPost("/Employee/UpdateEmployee")]
        public BaseResponse Update([FromBody] EmployeeCredential employee)
        {
            BaseResponse response = new BaseResponse();
            string projectRootPath = _hostEnvironment.WebRootPath;
            try
            {
               
                response = _employeeservice.UpdateEmployee(employee,projectRootPath);                  
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);           
                return response;
            }
        }

        [HttpDelete("/Employee/DeleteEmployee")]
        public BaseResponse Delete(int empid)
        {
            BaseResponse response = new BaseResponse();
            try
            {          
                response  = _employeeservice.DeleteEmployee(empid);
              
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
