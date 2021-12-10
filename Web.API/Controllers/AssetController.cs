
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web.API.Helper;
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
            _logger = new Logger (_hostEnvironment);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
