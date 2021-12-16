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
using Web.Services.Interfaces;

namespace Web.API.Controllers
{
    public class AssetLaptopController : Controller
    {
        private readonly IAssetLaptopService _assetLaptopservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetLaptopController(IAssetLaptopService assetLaptopservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetLaptopservice = assetLaptopservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }
        [HttpPost("/Asset/Laptop/AddLaptop")]
        public BaseResponse CreateLaptop([FromBody] AssetLaptopCredential asset)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetLaptopservice.creatLaptop(asset);



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

        [HttpPost("/Asset/Laptop/UpdateLaptop")]
        public BaseResponse UpdateLaptop([FromBody] AssetLaptopCredential asset)
        {
            BaseResponse response = new BaseResponse();

            try
            {

                response = _assetLaptopservice.updatelaptop(asset);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                return response;
            }
        }

        [HttpDelete("/Asset/Laptop/DeleteLaptop")]
        public BaseResponse DeleteLaptop(int assetid)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetLaptopservice.deleteLaptop(assetid);

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

        [HttpGet("Asset/Laptop/GetAllSum")]
        public BaseResponse getTotalCostLaptop(int categoryid)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetLaptopservice.sumOfLaptop(categoryid);
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

        [HttpGet("Asset/Laptop/GetAllQuantity")]
        public BaseResponse getTotalQuantityLaptop(int categoryid)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetLaptopservice.totalQuantityLaptop(categoryid);
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
