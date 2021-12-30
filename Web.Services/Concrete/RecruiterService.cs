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
    public class RecruiterService : IRecruiterService
    {
        private readonly IHRMSRecruiterRepository _hrmsrecruiterRepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;

        public RecruiterService(IConfiguration config, IHRMSRecruiterRepository hrmsrecruiterRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsrecruiterRepository = hrmsrecruiterRepository;
            _uow = uow;
        }

        public BaseResponse CreateRecruit(RmsTblRecruiterVM recruit)
        {
            BaseResponse response = new BaseResponse();
            RmsTblRecruiter rmsRecruiter = new RmsTblRecruiter();
            if (!string.IsNullOrEmpty(recruit.RtrEmail))

            {
                rmsRecruiter.RtrRecId = recruit.RtrRecId;
                rmsRecruiter.RtrEmail = recruit.RtrEmail;
                rmsRecruiter.RtrPhoneNo = recruit.RtrPhoneNo;
                rmsRecruiter.RtrLastDegree = recruit.RtrLastDegree;
                rmsRecruiter.RtrLastCompany = recruit.RtrLastCompany;
                rmsRecruiter.RtrExperience = recruit.RtrExperience;
                rmsRecruiter.RtrCurrentSalary = recruit.RtrCurrentSalary;
                rmsRecruiter.RtrExpectedSalary = recruit.RtrExpectedSalary;
                rmsRecruiter.RtrResume = recruit.RtrResume;
                rmsRecruiter.RtrRecruitStatus = recruit.RtrRecruitStatus;
                rmsRecruiter.RtrInterviewDateTime = recruit.RtrInterviewDateTime;
                rmsRecruiter.RtrInterviewStatus = recruit.RtrInterviewStatus;
                rmsRecruiter.RtrRecruitStatusRemarks = recruit.RtrRecruitStatusRemarks;
                rmsRecruiter.RtrShortList = recruit.RtrShortList;
                rmsRecruiter.RtrExpectedDate = recruit.RtrExpectedDate;
                rmsRecruiter.RtrSalaryNegotiation = recruit.RtrSalaryNegotiation;
                rmsRecruiter.RtrRecommendedBy = recruit.RtrRecommendedBy;
                rmsRecruiter.RtrRecommendedPersonRemarks = recruit.RtrRecommendedPersonRemarks;
                rmsRecruiter.RtrHiringStatus = recruit.RtrHiringStatus;
                rmsRecruiter.RtrCreatedBy = recruit.RtrCreatedBy;
                rmsRecruiter.RtrCreatedByName = recruit.RtrCreatedByName;
                rmsRecruiter.RtrCreatedByDate = recruit.RtrCreatedByDate;
                rmsRecruiter.RtrModifiedBy = recruit.RtrModifiedBy;
                rmsRecruiter.RtrModifiedByName = recruit.RtrModifiedByName;
                rmsRecruiter.RtrModifiedByDate = recruit.RtrModifiedByDate;
                rmsRecruiter.RtrIsDelete = false;
                _uow.Commit();
               _hrmsrecruiterRepository.Insert(rmsRecruiter);
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

        public BaseResponse UpdateRecruit(RmsTblRecruiterVM recruit)
        {
            throw new NotImplementedException();
        }

        public BaseResponse GetAllRecruit()
        {
            throw new NotImplementedException();
        }

        public BaseResponse DeleteRecruit(int id)
        {
            throw new NotImplementedException();
        }

        public BaseResponse ViewDataRecruitByid(int id)
        {
            throw new NotImplementedException();
        }
    }
}
