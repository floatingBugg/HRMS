using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Data.Interfaces;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class AssetCategoryService : IAssetCategoryService
    {
        private readonly IHRMSAssetCategoryRepository _hrmsassetcategoryRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public AssetCategoryService(IConfiguration config, IHRMSAssetCategoryRepository hrmsassetcategoryRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsassetcategoryRepository = hrmsassetcategoryRepository;
            _uow = uow;
        }
    }
}
