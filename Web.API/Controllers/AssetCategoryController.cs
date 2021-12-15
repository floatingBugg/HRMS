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
        public IActionResult Index()
        {
            return View();
        }
    }
}
