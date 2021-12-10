
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Web.API.Helper;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
using Web.Services.Interfaces;
namespace Web.API.Controllers
{
    public class AssetLaptopController : Controller
    {
        private readonly IAssetLaptopService _assetlaptopservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetLaptopController(IAssetLaptopService assetlaptopservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetlaptopservice = assetlaptopservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }

        //Assest Laptop CRUD 
        [HttpPost("/Assest/Laptop/AddLaptop")]
        public BaseResponse CreateLaptop([FromBody] AssetLaptopCredential laptop)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetlaptopservice.CreateAssetLaptop(laptop);



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

        [HttpPost("/Assest/Laptop/UpdateLaptop")]
        public BaseResponse UpdateLaptop([FromBody] AssetLaptopCredential laptop)
        {
            BaseResponse response = new BaseResponse();

            try
            {

                response = _assetlaptopservice.UpdateAssestLaptop(laptop);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                return response;
            }
        }

        [HttpDelete("/Assest/Laptop/RemoveLaptop")]
        public BaseResponse Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetlaptopservice.DeleteAssestLaptop(id);

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
