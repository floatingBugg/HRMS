using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Helper;
using Web.Model;
using Web.Model.ViewModel;
using Web.Services.Interfaces;

namespace Web.API.Controllers
{
    public class LeaveController : Controller
    {
       private readonly ILeaveService _leaveservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;
        public LeaveController(ILeaveService leaveservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _leaveservice = leaveservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }
        [HttpPost("/Leave/AssignLeave")]
        public BaseResponse CreateLeave([FromBody] LmsLeaveRecordVM leave)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _leaveservice.CreateLeave(leave);



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

        [HttpPost("/Leave/UpdateAssignedLeave")]
        public BaseResponse UpdateLeave([FromBody] LmsLeaveRecordVM leave)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _leaveservice.UpdateLeave(leave);



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

        [HttpGet("/Leave/ViewLeaveByEmpid")]
        public BaseResponse ViewRecordbyempid(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _leaveservice.ViewLeaveByempid(id);



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

        [HttpGet("/Leave/ViewLeaveByEmpid")]
        public BaseResponse ViewRecordbyrecordid(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _leaveservice.ViewLeaveByempid(id);



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
