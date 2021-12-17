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
    public class AssetNetworkController : Controller
    {
        private readonly IAssetNetworkService _assetnetworkservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetNetworkController(IAssetNetworkService assetnetworkservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetnetworkservice = assetnetworkservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }

        [HttpPost("/Asset/AddAssetNetwork")]
        public BaseResponse CreateAssetNetwork([FromBody] AssetCredential network)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetnetworkservice.CreateAssetnetwork(network);



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

        [HttpPost("/Asset/UpdateAssetNetwork")]
        public BaseResponse UpdateAssetNetwork([FromBody] AssetCredential network)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetnetworkservice.UpdateAssetNetwork(network);



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

        [HttpDelete("/Asset/DeletAssetNetwork")]
        public BaseResponse DeleteAssetNetwork(int id)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetnetworkservice.DeleteAssetNetwork(id);



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

        [HttpGet("/Asset/DisplayAssetNetwork")]
        public BaseResponse DisplayAssetNetwork(int categoryid)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetnetworkservice.DisplayAssetNetwork(categoryid);



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

        [HttpGet("Asset/Network/GetAllSum")]
        public BaseResponse SumOfNetwork(int categoryid)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetnetworkservice.SumOfNetwork(categoryid);
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

        [HttpGet("Asset/Network/GetAllQuantity")]
        public BaseResponse TotalQuantityNetwork(int categoryid)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetnetworkservice.TotalQuantityNetwork(categoryid);
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
