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
    public class PositionAppliedController : Controller
    {
        private readonly IPositionAppliedService _positionappliedservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public PositionAppliedController(IPositionAppliedService positionappliedservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _positionappliedservice = positionappliedservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }

        [HttpPost("/PositionApplied/AddPosition")]
        public BaseResponse CreatePosition([FromBody] RmsTblPositionAppliedVM position)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _positionappliedservice.CreatePosition(position);



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

        [HttpPost("/PositionApplied/UpdatePosition")]
        public BaseResponse UpdatePosition([FromBody] RmsTblPositionAppliedVM position)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _positionappliedservice.UpdatePosition(position);



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

        [HttpGet("/PositionApplied/DisplayPosition")]
        public BaseResponse DisplayPosition()
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _positionappliedservice.DisplayPosition();



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

        [HttpDelete("/PositionApplied/DeletePosition")]
        public BaseResponse DeletePosition(int id)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _positionappliedservice.DeletePosition(id);



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
