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
    public class AssetAssignController : Controller
    {
        private readonly IAssetAssignService _assetassignservice;
        private IConfiguration _config;
        Logger _logger;
        private IWebHostEnvironment _hostEnvironment;

        public AssetAssignController(IAssetAssignService assetassignservice, IConfiguration config, IWebHostEnvironment environment)
        {
            _assetassignservice = assetassignservice;
            _config = config;
            _hostEnvironment = environment;
            _logger = new Logger(_hostEnvironment);
        }

        [HttpPost("/Asset/AssignAsset")]
        public BaseResponse CreateAssign([FromBody] AssetAssignCredential assign)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetassignservice.createAssign(assign);



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

        [HttpDelete("/Asset/DeleteAssignedAsset")]
        public BaseResponse DeleteAssign(int assignid)
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetassignservice.deleteAssign(assignid);



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

        [HttpPost("/Asset/UpdateAssignAsset")]
        public BaseResponse Update([FromBody] AssetAssignCredential assign)
        {
            BaseResponse response = new BaseResponse();

            try
            {

                response = _assetassignservice.updateAssign(assign);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                return response;
            }
        }

        [HttpGet("/Asset/GetEmployee")]
        public BaseResponse getEmployee()
        {
            BaseResponse responce = new BaseResponse();

            try
            {
                var test = ModelState;
                responce = _assetassignservice.getEmployee();



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

        [HttpGet("/Asset/DisplayAssignAsset")]
        public BaseResponse DisplayAssignAsset(int type,int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var test = ModelState;
                response = _assetassignservice.displayAllAssetAssigned(type,id);



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

        [HttpGet("/Asset/GetAssetbyID")]
        public BaseResponse ViewAssignDataByid(int id)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                response = _assetassignservice.ViewDataAssignByid(id);
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


