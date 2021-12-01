﻿using System;
using System.Text.Json;
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

        [HttpPost("/Employee/Add")]
        public BaseResponse Create([FromBody]EmsTblEmployeeDetails employee)
        {
            BaseResponse response = new BaseResponse(); 
            try
            {
                var test = ModelState;
                response = _employeeservice.CreateEmployee(employee);
               
                    
                
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
               
                response.Data = _employeeservice.UpdateEmployee(employee);
               
       
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
                response.Data = _employeeservice.DeleteEmployee(id);
              
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
