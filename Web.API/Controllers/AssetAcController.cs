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
    public class AssetAcController : Controller
    {
        private readonly IAssetAcService _assetacservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetAcController(IAssetAcService assetacservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetacservice = assetacservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }

        //Assest Laptop CRUD 
        [HttpPost("/Assest/Ac/AddAc")]
        public BaseResponse CreateAc([FromBody] AssetAcCredential Ac)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetacservice.CreateAssetAc(Ac);



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

        [HttpPost("/Assest/Ac/UpdateAc")]
        public BaseResponse UpdateAc([FromBody] AssetAcCredential Ac)
        {
            BaseResponse response = new BaseResponse();

            try
            {

                response = _assetacservice.UpdateAssestAc(Ac);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                return response;
            }
        }

        [HttpDelete("/Asset/Ac/Delete")]
        public BaseResponse Delete(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetacservice.DeleteAssestAc(id);

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
