using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Data;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
using Web.Model.ViewModel;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class PositionAppliedService : IPositionAppliedService
    {
        private readonly IHRMSPositionAppliedRepository _hrmspositionappliedRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;

        public PositionAppliedService(IConfiguration config, IHRMSPositionAppliedRepository hrmspositionappliedRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmspositionappliedRepository = hrmspositionappliedRepository;
            _uow = uow;
        }

        public BaseResponse CreatePosition(RmsTblPositionAppliedVM position)
        {
            BaseResponse response = new BaseResponse();
            RmsTblPositionApplied rmsPosition = new RmsTblPositionApplied();
            if (!string.IsNullOrEmpty(position.RtpaPosition))

            {
                rmsPosition.RtpaPosId = position.RtpaPosId;
                rmsPosition.RtpaPosition = position.RtpaPosition;
                rmsPosition.RtpaCreatedBy = position.RtpaCreatedBy;
                rmsPosition.RtpaCreatedByName = position.RtpaCreatedByName;
                rmsPosition.RtpaCreatedByDate = position.RtpaCreatedByDate;
                rmsPosition.RtpaModifiedBy = position.RtpaModifiedBy;
                rmsPosition.RtpaModifiedByName = position.RtpaModifiedByName;
                rmsPosition.RtpaModifiedByDate = position.RtpaModifiedByDate;
                rmsPosition.RtpaIsDelete = position.RtpaIsDelete;

                _uow.Commit();
                _hrmspositionappliedRepository.Insert(rmsPosition);
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

        public BaseResponse UpdatePosition(RmsTblPositionAppliedVM position)
        {
            throw new NotImplementedException();
        }

        public BaseResponse DisplayPosition()
        {
            throw new NotImplementedException();
        }

        public BaseResponse DeletePosition(int id)
        {
            throw new NotImplementedException();
        }
    }
}
