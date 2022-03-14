using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        [HttpPost("/Leave/AddLeaveRecord")]
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

        [HttpPost("/Leave/UpdateLeaveRecord")]
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

        [HttpGet("/Leave/ViewLeaveRecordByEmpid")]
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

        [HttpGet("/Leave/ViewLeaveRecordByRecordid")]
        public BaseResponse ViewRecordbyrecordid(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _leaveservice.ViewLeaveByrecordid(id);



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

        [HttpGet("/Leave/ViewAllLeaveRecord")]
        public BaseResponse ViewAllRecord()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _leaveservice.ViewLeaveEmployee();



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
