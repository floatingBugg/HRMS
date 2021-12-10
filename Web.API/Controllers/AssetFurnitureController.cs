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
    public class AssetFurnitureController : Controller
    {
        private readonly IAssetFurnitureService _assetfurnitureservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetFurnitureController(IAssetFurnitureService assetfurnitureservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetfurnitureservice = assetfurnitureservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }

        [HttpPost("/Assest/Furniture/AddFurniture")]
        public BaseResponse CreateAssetFurniture([FromBody] AssetFurnitureCredential furniture)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetfurnitureservice.CreateAssetFurniture(furniture);



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
