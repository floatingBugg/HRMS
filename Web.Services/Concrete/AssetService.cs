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
    public class AssetService : IAssetService
    {
        private readonly IHRMSAssetCategoryRepository _hrmsassetcategoryRepository;
        private readonly IHRMSAssetRepository _hrmsassetRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        private readonly UnitOfWork unitofWork;
        public AssetService(IConfiguration config,  IUnitOfWork uow, IHRMSAssetCategoryRepository hrmsassetcategoryRepository, IHRMSAssetRepository hrmsassetrepository)
        {
            _config = config;
            _uow = uow;
            _hrmsassetcategoryRepository = hrmsassetcategoryRepository;
            _hrmsassetRepository = hrmsassetrepository;
        }

























    }
}
