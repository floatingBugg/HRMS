using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Web.Data;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Model;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public EmployeeService(IConfiguration config, IHRMSEmployeeRepository hrmsemployeeRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _uow = uow;
        }


        public BaseResponse GetAllEmployee()
        {
            
            BaseResponse response = new BaseResponse();
            List<DisplayEmployeeGrid> empCred = new List<DisplayEmployeeGrid>();
            bool count = _hrmsemployeeRepository.Table.Count() > 0;
            var employeesData = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                emailAddress = x.EtedEmailAddress,
                contactNumber = x.EtedContactNumber,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not assigned"
            }).ToList().Take(10);

            if (count == true)
            {
                response.Data = employeesData;
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

        public BaseResponse CreateEmployee(EmsTblEmployeeDetails employee)
        {
            
            BaseResponse response = new BaseResponse();
            bool doesExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedEmailAddress == employee.EtedEmailAddress) > 0;
            if (!string.IsNullOrEmpty(employee.EtedFirstName) && !string.IsNullOrEmpty(employee.EtedLastName)
               && !string.IsNullOrEmpty(employee.EtedEmailAddress) && !string.IsNullOrEmpty(employee.EtedAddress) && !string.IsNullOrEmpty(employee.EtedGender) && !string.IsNullOrEmpty(employee.EtedReligion)
               && (employee.EtedCnic!=null) && doesExistAlready == false )
            {
                employee.EtedCreatedBy = "admin";
                employee.EtedCreatedByDate = DateTime.Now;
                employee.EtedCreatedByName = "admin";
                employee.EtedIsDelete = false;

                // Update AcademicQualification
                if (employee.EmsTblAcademicQualification.Count > 0)
                {
                    foreach (var item in employee.EmsTblAcademicQualification)
                    {
                        item.EtaqCreatedBy = "admin";
                        item.EtaqCreatedByDate = DateTime.Now;
                        item.EtaqCreatedByName = "admin";
                        item.EtaqIsDelete = false;

                    }
                }

                //Update ProfessionalQualification
                if (employee.EmsTblProfessionalQualification.Count > 0)
                {
                    foreach (var item in employee.EmsTblProfessionalQualification)
                    {
                        item.EtpqCreatedBy = "admin";
                        item.EtpqCreatedByDate = DateTime.Now;
                        item.EtpqCreatedByName = "admin";
                        item.EtpqIsDelete = false;
                    }
                }

                //Update ProfessionalDetails
                if (employee.EmsTblEmployeeProfessionalDetails.Count > 0)
                {
                    foreach (var item in employee.EmsTblEmployeeProfessionalDetails)
                    {
                        item.EtepdCreatedBy = "admin";
                        item.EtepdCreatedByDate = DateTime.Now;
                        item.EtepdCreatedByName = "admin";
                        item.EtepdIsDelete = false;
                    }
                }

                //Update Emergency Contact
                if (employee.EmsTblEmergencyContact.Count > 0)
                {
                    foreach (var item in employee.EmsTblEmergencyContact)
                    {
                        item.EtecCreatedBy = "admin";
                        item.EtecCreatedByDate = DateTime.Now;
                        item.EtecCreatedByName = "admin";
                        item.EtecIsDelete = false;
                    }
                }

                // Working History
                if (employee.EmsTblWorkingHistory.Count > 0)
                {
                    foreach (var item in employee.EmsTblWorkingHistory)
                    {
                        item.EtwhCreatedBy = "admin";
                        item.EtwhCreatedByDate = DateTime.Now;
                        item.EtwhCreatedByName = "admin";
                        item.EtwhIsDelete = false;
                        response.Message = UserMessages.strUpdated;
                        response.Success = true;


                    }

                }

                _hrmsemployeeRepository.Insert(employee);
                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }

         else if (doesExistAlready == true)
            {
                response.Message = UserMessages.strAlrexist;
            }

            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }
           
            return response;
        }

        public BaseResponse  UpdateEmployee(EmsTblEmployeeDetails employee)
        {

            //Update EmployeeDetails
            BaseResponse response = new BaseResponse();
            
            employee.EtedModifiedBy = "admin";
            employee.EtedModifiedByDate = DateTime.Now;
            employee.EtedModifiedByName = "admin";
            employee.EtedIsDelete = false;

            // Update AcademicQualification
            if (employee.EmsTblAcademicQualification.Count > 0)
            {
                foreach (var item in employee.EmsTblAcademicQualification)
                {
                    item.EtaqModifiedBy = "admin";
                    item.EtaqModifiedByDate = DateTime.Now;
                    item.EtaqModifiedByName = "admin";
                    item.EtaqIsDelete = false;
                 
                }
            }

            //Update ProfessionalQualification
            if (employee.EmsTblProfessionalQualification.Count > 0)
            {
                foreach (var item in employee.EmsTblProfessionalQualification)
                {
                    item.EtpqModifiedBy = "admin";
                    item.EtpqModifiedByDate = DateTime.Now;
                    item.EtpqModifiedByName = "admin";
                    item.EtpqIsDelete = false;
                }
            }

            //Update ProfessionalDetails
            if (employee.EmsTblEmployeeProfessionalDetails.Count > 0)
            {
                foreach (var item in employee.EmsTblEmployeeProfessionalDetails)
                {
                    item.EtepdModifiedBy = "admin";
                    item.EtepdModifiedByDate = DateTime.Now;
                    item.EtepdModifiedByName = "admin";
                    item.EtepdIsDelete = false;
                }
            }

            //Update Emergency Contact
            if (employee.EmsTblEmergencyContact.Count > 0)
            {
                foreach (var item in employee.EmsTblEmergencyContact)
                {
                    item.EtecModifiedBy = "admin";
                    item.EtecModifiedByDate = DateTime.Now;
                    item.EtecModifiedByName = "admin";
                    item.EtecIsDelete = false;
                }
            }

            // Working History
            if (employee.EmsTblWorkingHistory.Count > 0)
            {
                foreach (var item in employee.EmsTblWorkingHistory)
                {
                    item.EtwhModifiedBy = "admin";
                    item.EtwhModifiedByDate = DateTime.Now;
                    item.EtwhModifiedByName = "admin";
                    item.EtwhIsDelete = false;
                    response.Message = UserMessages.strUpdated;
                    response.Success = true;
                   
                    
                }
               
            }
          

            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotupdated;
            }
            _hrmsemployeeRepository.Update(employee);
            _uow.Commit();
            return response;
        }

        public BaseResponse DeleteEmployee(int empId)
        {
            BaseResponse response = new BaseResponse();
            bool doesExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedEmployeeId == empId) > 0;
            bool isDeletedAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedIsDelete == true && p.EtedEmployeeId== empId) > 0;
            _hrmsemployeeRepository.Table.Where(p => p.EtedEmployeeId == empId)
           .ToList()
           .ForEach(x =>
           {
               x.EtedIsDelete = true;
               x.EtedModifiedBy = "admin";
               x.EtedModifiedByDate = DateTime.Now;
               x.EtedModifiedByName = "admin";

           });
            if (doesExistAlready == true && isDeletedAlready == false)
            {
                response.Message = UserMessages.strDeleted;
                response.Success = true;
                response.Data = empId;
            }
            else if(isDeletedAlready == true && doesExistAlready == true)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strAlrdeleted;
            }
            else if(doesExistAlready == false)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotfound;
            }
            

            _uow.Commit();
            return response;


        }

        public BaseResponse EditEmployeeByid(int id)
        {
            BaseResponse response = new BaseResponse();
            
           bool count = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false && z.EtedEmployeeId == id).Count() > 0;
         


            var employeesData = _hrmsemployeeRepository.Table.Include(x => x.EmsTblAcademicQualification).Include(x => x.EmsTblEmergencyContact).Include(x => x.EmsTblEmployeeProfessionalDetails).Include(x => x.EmsTblProfessionalQualification).Include(x => x.EmsTblWorkingHistory).Where(x=>x.EtedEmployeeId == id).ToList();

            if (count == true)
            {
                response.Data = employeesData;
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
