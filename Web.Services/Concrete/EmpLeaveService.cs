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
using Web.Model.ViewModel;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class EmpLeaveService : IEmpLeaveService
    {
        private readonly IHRMSEmployeeLeaveRepository _hrmsemployeeleaverepository;
        private readonly IHRMSLeaveRecordRepository _hrmsleaverecordrepository;
        private readonly IHRMSLeaveTypeRepository _hrmsleavetyperepository;
        private readonly IHRMSEmployeeRepository _hrmsemployeerepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public EmpLeaveService(IConfiguration config, IHRMSEmployeeLeaveRepository hrmsEmployeeLeaveRepository, IHRMSLeaveRecordRepository hrmsLeaverecordRepository, IHRMSLeaveTypeRepository hrmsLeaveTypeRepository, IHRMSEmployeeRepository hrmsEmployeeRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsemployeeleaverepository = hrmsEmployeeLeaveRepository;
            _hrmsleaverecordrepository = hrmsLeaverecordRepository;
            _hrmsleavetyperepository = hrmsLeaveTypeRepository;
            _hrmsemployeerepository = hrmsEmployeeRepository;

            _uow = uow;
        }

        public BaseResponse createLeave(LmsEmployeeLeaveVM leave)
        {
            BaseResponse response = new BaseResponse();
            if (leave.LmselLeaveType != null)
            {
                LmsEmployeeLeave lmsEmployeeLeave = new LmsEmployeeLeave();

                lmsEmployeeLeave.LmselEtedEmployeeId = leave.LmselEtedEmployeeId;
                lmsEmployeeLeave.LmselLeaveType = leave.LmselLeaveType;
                lmsEmployeeLeave.LmselStartDate = leave.LmselStartDate;
                lmsEmployeeLeave.LmselEndDate = leave.LmselEndDate;
                lmsEmployeeLeave.LmselDays = leave.LmselDays;
                lmsEmployeeLeave.LmselCreatedBy = leave.LmselCreatedBy;
                lmsEmployeeLeave.LmselCreatedByName = leave.LmselCreatedByName;
                lmsEmployeeLeave.LmselCreatedByDate = leave.LmselCreatedByDate;
                lmsEmployeeLeave.LmselIsDelete = leave.LmselIsDelete;
                

                _hrmsemployeeleaverepository.Insert(lmsEmployeeLeave);
                _uow.Commit();

                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }

            return response;
        }


    }
}
