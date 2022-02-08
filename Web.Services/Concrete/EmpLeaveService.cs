﻿using Microsoft.Extensions.Configuration;
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
            var totalremaining = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrTotalTaken).FirstOrDefault();
            var sick = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrSickTaken).FirstOrDefault();
            var casual = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrCasualTaken).FirstOrDefault();
            var annual = _hrmsleaverecordrepository.Table.Where(x => x.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId).Select(y => y.LmslrAnnualTaken).FirstOrDefault();
            totalremaining = totalremaining + leave.LmselDays;
            if (leave.LmselLeaveType==2 && sick<=6) 
            {
                sick = sick + leave.LmselDays;
            }
            else if (leave.LmselLeaveType == 3 && casual<=6)
            {
                casual = casual + leave.LmselDays;
            }
            else if (leave.LmselLeaveType == 1 && annual<=12)
            {
                annual = annual + leave.LmselDays;
                
            }

            else
            {
                response.Message = UserMessages.strNoLeave;
                response.Data = null;
            }
            
            if (leave.LmselLeaveType != null && totalremaining<=24 && sick<=6 && casual<=6 && annual<=12)
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


                _hrmsleaverecordrepository.Table.Where(p => p.LmslrEtedEmployeeId == leave.LmselEtedEmployeeId)
                    .ToList()
                    .ForEach(x => {
                        x.LmslrTotalTaken = totalremaining;
                        x.LmslrSickTaken = sick;
                        x.LmslrCasualTaken = casual;
                        x.LmslrAnnualTaken = annual;
                    });

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

        public BaseResponse updateLeave(LmsEmployeeLeaveVM leave)
        {

            BaseResponse response = new BaseResponse();
            LmsEmployeeLeave lmsEmployeeLeave = new LmsEmployeeLeave();
            bool count = _hrmsemployeeleaverepository.Table.Where(p => p.LmselLeaveId == leave.LmselLeaveId).Count() > 0;

            if (count == true)
            {
                if ((leave.LmselDays!=null))
                {

                    lmsEmployeeLeave.LmselLeaveId = leave.LmselLeaveId;
                    lmsEmployeeLeave.LmselEtedEmployeeId = leave.LmselEtedEmployeeId;
                    lmsEmployeeLeave.LmselLeaveType = leave.LmselLeaveType;
                    lmsEmployeeLeave.LmselStartDate = leave.LmselStartDate;
                    lmsEmployeeLeave.LmselEndDate = leave.LmselEndDate;
                    lmsEmployeeLeave.LmselDays = leave.LmselDays;
                    lmsEmployeeLeave.LmselIsDelete =leave.LmselIsDelete;
                    
                }

                _hrmsemployeeleaverepository.Update(lmsEmployeeLeave);
                _uow.Commit();
                response.Success = true;
                response.Message = UserMessages.strUpdated;
                response.Data = null;
            }

            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotupdated;
            }

            return response;
        }
        public BaseResponse ViewLeaveRecordByempid(int empid)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsemployeeleaverepository.Table.Where(z => z.LmselEtedEmployeeId == empid && z.LmselIsDelete != true).Count() > 0;
            var leavedata = _hrmsemployeeleaverepository.Table.Where(x => x.LmselEtedEmployeeId == empid && x.LmselIsDelete != true).Select(x => new LmsEmployeeLeaveVM
            {
                LmselLeaveId = x.LmselLeaveId,
                LmselEtedEmployeeName = _hrmsemployeerepository.Table.Where(z => z.EtedEmployeeId == x.LmselEtedEmployeeId).Select(z => z.EtedFirstName + " " + z.EtedLastName).FirstOrDefault(),
                LmselLeaveType = x.LmselLeaveType,
                LmselStartDate = x.LmselStartDate,
                LmselDays = x.LmselDays,
                LmselCreatedBy = x.LmselCreatedBy,
                LmselCreatedByName = x.LmselCreatedByName,
                LmselCreatedByDate = x.LmselCreatedByDate,
                
            }).ToList().OrderByDescending(x => x.LmselLeaveId);

            if (count == true)
            {
                response.Data = leavedata;
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

        public BaseResponse EmployeeData(int roleid, int empid)
        {
            

            BaseResponse response = new BaseResponse();
            List<DisplayEmployeeGrid> empCred = new List<DisplayEmployeeGrid>();
            bool count = _hrmsemployeerepository.Table.Count() > 0;

            var employeeData = _hrmsemployeerepository.Table.Where(z => z.EtedIsDelete == false && z.EtedEmployeeId == empid).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                empStatus = x.EtedStatus,
            }).ToList().OrderByDescending(x => x.empID);



            var managerData = _hrmsemployeerepository.Table.Where(z => z.EtedIsDelete == false && z.EtedIsManager == true).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                empStatus = x.EtedStatus,
            }).ToList().OrderByDescending(x => x.empID);

            if (roleid == 1 || roleid == 2)
            {
                var employeesData = _hrmsemployeerepository.Table.Where(z => z.EtedIsDelete == false && z.EtedEmployeeId != empid && z.EtedStatus=="Active").Select(x => new DisplayEmployeeGrid()
                {
                    empID = x.EtedEmployeeId,
                    fullName = x.EtedFirstName,
                    empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                    empStatus = x.EtedStatus,
               
                }).ToList().OrderByDescending(x => x.empID);
                response.Data = employeesData;

            }

            else if (roleid == 3)
            {
                var employeesData = _hrmsemployeerepository.Table.Where(z => z.EtedIsDelete == false && z.EtedManagerId == empid).Select(x => new DisplayEmployeeGrid()
                {
                    empID = x.EtedEmployeeId,
                    fullName = x.EtedFirstName,
                    empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                    empStatus = x.EtedStatus,
                }).ToList().OrderByDescending(x => x.empID);
                response.Data = employeesData;
            }

            if (count == true)
            {
                response.Data3 = managerData;
                response.Data2 = employeeData;

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
