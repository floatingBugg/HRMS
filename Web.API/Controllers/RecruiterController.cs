using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Helper;
using Web.Model;
using Web.Model.Common;
using Web.Model.ViewModel;
using Web.Services.Interfaces;

namespace Web.API.Controllers
{
    public class RecruiterController : Controller
    {
        private readonly IRecruiterService _recruiterservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public RecruiterController(IRecruiterService recruiterService, IConfiguration config, IWebHostEnvironment environment)
        {
            _recruiterservice = recruiterService;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }

        [HttpPost("/Recruiter/AddRecruit")]
        public BaseResponse CreateRecruit([FromBody] RmsTblRecruiterVM recruit)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _recruiterservice.CreateRecruit(recruit);



                return responce;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                responce.Data = null;
                responce.Message = ex.Message;
                responce.Success = false;
                return responce;
            }
        }

        [HttpPost("/Recruiter/UpdateRecruit")]
        public BaseResponse UpdateRecruit([FromBody] RmsTblRecruiterVM recruit)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _recruiterservice.UpdateRecruit(recruit);



                return responce;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                responce.Data = null;
                responce.Message = ex.Message;
                responce.Success = false;
                return responce;
            }
        }

        [HttpGet("/Recruiter/DisplayRecruit")]
        public BaseResponse DisplayRecruit()
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _recruiterservice.DisplayRecruit();



                return responce;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                responce.Data = null;
                responce.Message = ex.Message;
                responce.Success = false;
                return responce;
            }
        }

        [HttpDelete("/Recruiter/DeleteRecruit")]
        public BaseResponse DeleteRecruit(int id)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _recruiterservice.DeleteRecruit(id);



                return responce;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                responce.Data = null;
                responce.Message = ex.Message;
                responce.Success = false;
                return responce;
            }
        }


    }
}
