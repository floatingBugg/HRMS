
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Web.API.Helper;
using Web.Model;
using Web.Model.ViewModel;
using Web.Services.Interfaces;

namespace Web.API.Controllers
{
    public class EmpLeaveController : Controller
    {

        private readonly IEmpLeaveService _empLeaveservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public EmpLeaveController(IEmpLeaveService empLeaveService, IConfiguration config, IWebHostEnvironment environment)
        {
            _empLeaveservice = empLeaveService;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }
        [HttpPost("/Leave/AssignLeave")]
        public BaseResponse AssignLeave([FromBody] LmsEmployeeLeaveVM leave)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _empLeaveservice.createLeave(leave);



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
    }
}
