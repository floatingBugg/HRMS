using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Web.API.Helper;
using Web.Model;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.API.Controllers
{
    public class AssetController : Controller
    {

        private readonly IAssetService _assetservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetController(IAssetService assetservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetservice = assetservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }

        [HttpPost("/Asset/CreateAssetCategory")]
        public BaseResponse CreateCategory([FromBody] AssetCategoryCredential category)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetservice.CreateAssetCategory(category);



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


        [HttpPost("/Assest/AddAsset")]
        public BaseResponse CreateAsset([FromBody] AssetCredential assets)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetservice.CreateAsset(assets);

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
       
