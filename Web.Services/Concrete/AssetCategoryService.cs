using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
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

        public BaseResponse CreateAssetCategory(AssetCategoryCredential asset)
        {
            BaseResponse response = new BaseResponse();

            if (!string.IsNullOrEmpty(asset.categoryname))

            {
                List<ImsAssetsCategoryVM> assetcategory = new List<ImsAssetsCategoryVM>();

                assetcategory.Add(new ImsAssetsCategory
                {
                    ItacCategoryName = asset.categoryname,
                    ItacCreatedBy = asset.createdby,
                    ItacCreatedByName = asset.createdbyname,
                    ItacCreatedByDate = DateTime.Now,
                    ItacIsDelete = false


                });
                _uow.Commit();
                _hrmsassetcategoryRepository.Insert(assetcategory);
                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }
            else
            {
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }
                return response;
            

        }


        public BaseResponse UpdateAssetCategory(AssetCategoryCredential asset)
        {
            BaseResponse responce = new BaseResponse();
            bool count = _hrmsassetcategoryRepository.Table.Where(p => p.ItacCategoryId == asset.categoryId).Count() > 0;
            if (count==true)
            {
                _hrmsassetcategoryRepository.Table.Where(p => p.ItacCategoryId == asset.categoryId).ToList().ForEach(x =>
                {
                    x.ItacCategoryName = asset.categoryname;
                    x.ItacModifiedBy = asset.modifiedby;
                    x.ItacModifiedByName = asset.modifiedbyname;
                    x.ItacModifiedByDate = DateTime.Now;
                    x.ItacIsDelete = false;


                });
                _uow.Commit();
                responce.Success = true;
                responce.Message = UserMessages.strUpdated;
                responce.Data = null;

            }
            else
            {
                responce.Success = false;
                responce.Message = UserMessages.strNotupdated;
                responce.Data = null;
            }
            return responce;
        
        }

        public BaseResponse DeleteAssetCategory(int id)
        {
            BaseResponse responce = new BaseResponse();
            bool doesExistAlready = _hrmsassetcategoryRepository.Table.Count(p => p.ItacCategoryId == id) > 0;
            bool isDeletedAlready = _hrmsassetcategoryRepository.Table.Count(p => p.ItacCategoryId == id && p.ItacIsDelete == true) > 0;

            _hrmsassetcategoryRepository.Table.Where(p => p.ItacCategoryId == id).ToList().ForEach(x =>
            {

                x.ItacIsDelete = true;

            });
            _uow.Commit();
            if (doesExistAlready==true && isDeletedAlready==false)
            {
                responce.Success = true;
                responce.Data = id;
                responce.Message = UserMessages.strDeleted;
            }
            else if(doesExistAlready==true && isDeletedAlready==true)
            {
                responce.Success = false;
                responce.Message = UserMessages.strAlrdeleted;
                responce.Data = null;
            }

            else if (doesExistAlready==false)
            {
                responce.Data = null;
                responce.Success = false;
                responce.Message = UserMessages.strNotfound;
            }
            return responce;


        }

        public BaseResponse DisplayAssetCategory()
        {
            BaseResponse response = new BaseResponse();
            bool count = _hrmsassetcategoryRepository.Table.Count() > 0;
            var categorydata = _hrmsassetcategoryRepository.Table.Where(z => z.ItacIsDelete == false).Select(x => new AssetCategoryCredential()
            {
                categoryId = x.ItacCategoryId,
                categoryname = x.ItacCategoryName


            }).ToList().OrderByDescending(x => x.categoryId);

            if (count == true)
            {
                response.Data = categorydata;
                response.Success = true;
                response.Message = UserMessages.strSuccess;


            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotfound;
            }
            return response;
        }
    }
}
