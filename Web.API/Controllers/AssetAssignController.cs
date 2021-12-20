using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Helper;
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
    }
}
