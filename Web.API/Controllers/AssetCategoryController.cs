using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class AssetCategoryController : Controller
    {
        private readonly IAssetCategoryService _assetcategoryservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetCategoryController(IAssetCategoryService assetcategoryservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetcategoryservice = assetcategoryservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }

        [HttpPost("/Asset/AddAssetCategory")]
        public BaseResponse CreateAssetCategory([FromBody] ImsAssetsCategoryVM asset)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetcategoryservice.CreateAssetCategory(asset);



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

        [HttpPost("/Asset/UpdateAssetCategory")]
        public BaseResponse UpdateAssetCategory([FromBody] ImsAssetsCategoryVM asset)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetcategoryservice.UpdateAssetCategory(asset);



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


        [HttpDelete("/Asset/DeleteAssetCategory")]
        public BaseResponse DeleteAssetCategory(int id)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetcategoryservice.DeleteAssetCategory(id);



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

        [HttpGet("/Asset/DisplayAssetCategory")]
        public BaseResponse DisplayAssetCategory()
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetcategoryservice.DisplayAssetCategory();



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
