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
using Web.Services.Helper;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        private readonly IHRMSAcademicRepository _hrmsacademicrepository;
        private readonly IHRMSPRofessionalRepository _hrmsprofessionalrepository;
        private readonly IHRMSEmployeeContactRepository _employeeContactRepository;
        private readonly IHRMSEmployeeWorkingHistoryRepository _workinghistoryRepository;
        private readonly IHRMSProfessionalDetailsRepository _hrmsprofessionaldetailsrepository;
        private readonly IHRMSUserAuthRepository _hrmsUserAuthRepository;
        private readonly IHRMSDropdownValueRepository _hrmsdropdownvaluerepository;
        private readonly IHRMSAssetRepository _hrmsassetRepository;
        private readonly IHRMSAssetAssignRepository _hrmsassetAssignRepository;
        private readonly IHRMSEmployementStatusRepository _hrmsstatusRepository;
        private readonly IHRMSLeaveRecordRepository _hrmsleaverecordrepository;
        private readonly IHRMSEmployeeLeaveRepository _hrmsemployeeLeaveRepository;



        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public EmployeeService(IConfiguration config, IHRMSEmployeeLeaveRepository hrmsemployeeLeaveRepository, IHRMSLeaveRecordRepository hrmsleaverecordrepository, IHRMSEmployementStatusRepository hrmsstatusRepository, IHRMSUserAuthRepository hrmsUserAuthRepository, IHRMSAssetRepository hrmsassetRepository, IHRMSEmployeeRepository hrmsemployeeRepository, IHRMSAcademicRepository hRMSAcademicRepository, IHRMSEmployeeContactRepository employeeContactRepository, IHRMSPRofessionalRepository hRMSProfessionalRepository, IHRMSEmployeeWorkingHistoryRepository workingHistoryRepository, IHRMSProfessionalDetailsRepository hRMSProfessionalDetailsRepository, IHRMSDropdownValueRepository hrmsdropdownvaluerepository, IUnitOfWork uow, IHRMSAssetAssignRepository hrmsassetAssignRepository)
        {
            _config = config;
            _hrmsassetRepository = hrmsassetRepository;
            _hrmsassetAssignRepository = hrmsassetAssignRepository; 
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _employeeContactRepository = employeeContactRepository;
            _hrmsacademicrepository = hRMSAcademicRepository;
            _hrmsprofessionalrepository = hRMSProfessionalRepository;
            _workinghistoryRepository = workingHistoryRepository;
            _hrmsprofessionaldetailsrepository = hRMSProfessionalDetailsRepository;
            _hrmsUserAuthRepository = hrmsUserAuthRepository;
            _hrmsdropdownvaluerepository = hrmsdropdownvaluerepository;
            _hrmsstatusRepository = hrmsstatusRepository;
            _hrmsleaverecordrepository = hrmsleaverecordrepository;
            _hrmsemployeeLeaveRepository = hrmsemployeeLeaveRepository;
            _uow = uow;
        }


        public BaseResponse GetAllEmployee()
        {
            BaseResponse response = new BaseResponse();
            List<DisplayEmployeeGrid> empCred = new List<DisplayEmployeeGrid>();
            bool count = _hrmsemployeeRepository.Table.Count() > 0;

            var employeesData = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false ).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                emailAddress = x.EtedEmailAddress,
                contactNumber = x.EtedContactNumber,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned"
            }).ToList().OrderByDescending(x => x.empID);

            var managerData = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false && z.EtedIsManager == true).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                emailAddress = x.EtedEmailAddress,
                contactNumber = x.EtedContactNumber,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned"
            }).ToList().OrderByDescending(x => x.empID);


            if (count == true)
            {
                response.Data2 = managerData;
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
        
        public BaseResponse GetAllEmployee(int roleid,int empid)
        {
            
            BaseResponse response = new BaseResponse();
            List<DisplayEmployeeGrid> empCred = new List<DisplayEmployeeGrid>();
            bool count = _hrmsemployeeRepository.Table.Count() > 0;

            var employeeData = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false && z.EtedEmployeeId == empid).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                emailAddress = x.EtedEmailAddress,
                contactNumber = x.EtedContactNumber,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                manager = _hrmsemployeeRepository.Table.Where(m => m.EtedEmployeeId == x.EtedManagerId && m.EtedIsDelete != true).Select(m => m.EtedFirstName + " " + m.EtedLastName).FirstOrDefault() ?? "Not Assigned",
                empStatus=x.EtedStatus,
            }).ToList().OrderByDescending(x => x.empID);

           

            var managerData = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false && z.EtedIsManager == true).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                emailAddress = x.EtedEmailAddress,
                contactNumber = x.EtedContactNumber,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                empStatus = x.EtedStatus,
            }).ToList().OrderByDescending(x => x.empID);

            if (roleid == 1 || roleid == 2 ) { 
            var employeesData = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false && z.EtedEmployeeId!=empid).Select(x => new DisplayEmployeeGrid()
            {
                empID = x.EtedEmployeeId,
                fullName = x.EtedFirstName,
                emailAddress = x.EtedEmailAddress,
                contactNumber = x.EtedContactNumber,
                empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                empStatus = x.EtedStatus,
                manager = _hrmsemployeeRepository.Table.Where(m => m.EtedEmployeeId == x.EtedManagerId && m.EtedIsDelete != true).Select(m => m.EtedFirstName + " " + m.EtedLastName).FirstOrDefault() ??  "Not Assigned"
            }).ToList().OrderByDescending(x => x.empID);
                response.Data = employeesData;
                
            }

            else if (roleid == 3)
            {
                var employeesData = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false && z.EtedManagerId==empid).Select(x => new DisplayEmployeeGrid()
                {
                    empID = x.EtedEmployeeId,
                    fullName = x.EtedFirstName,
                    emailAddress = x.EtedEmailAddress,
                    contactNumber = x.EtedContactNumber,
                    empDesignation = x.EmsTblEmployeeProfessionalDetails.Count > 0 ? x.EmsTblEmployeeProfessionalDetails.Where(y => y.EtepdEtedEmployeeId == x.EtedEmployeeId).Select(z => z.EtepdDesignation).FirstOrDefault() : "Not Assigned",
                    empStatus = x.EtedStatus,
                    manager = _hrmsemployeeRepository.Table.Where(m => m.EtedEmployeeId == x.EtedManagerId && m.EtedIsDelete != true).Select(m => m.EtedFirstName + " " + m.EtedLastName).FirstOrDefault() ?? "Not Assigned"
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

        public BaseResponse CreateEmployee(EmsTblEmployeeDetailsVM employee, string rootpath)
        {

            BaseResponse response = new BaseResponse();
            bool doesEmailExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedOfficialEmailAddress == employee.EtedOfficialEmailAddress) > 0;
            bool doesCNICExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedCnic == employee.EtedCnic) > 0;
            EmsTblEmployeeDetails emsTblEmployeeDetails = new EmsTblEmployeeDetails();
            EmsTblHrmsUser emsuser = new EmsTblHrmsUser();
            ImsAssign imsassign = new ImsAssign();

            //Employee Details

            if (!string.IsNullOrEmpty(employee.EtedCreatedBy) && !string.IsNullOrEmpty(employee.EtedCreatedByName)
               //&& !string.IsNullOrEmpty(employee.EtedOfficialEmailAddress) && !string.IsNullOrEmpty(employee.EtedAddress) && !string.IsNullOrEmpty(employee.EtedGender)
               //&& !string.IsNullOrEmpty(employee.EtedReligion)
               //&& (employee.EtedCnic != null) && doesEmailExistAlready == false && doesCNICExistAlready == false
               )
            {
                if (!string.IsNullOrEmpty(employee.EtedPhotograph) && employee.EtedPhotograph!="")
                {
                    var RootPath = rootpath;
                    string FilePathPhoto = "Images\\EmployeeID\\PhotoGraph\\";
                    var targetPathProfile = Path.Combine(RootPath, FilePathPhoto);

                    if (!Directory.Exists(targetPathProfile))
                    {
                        Directory.CreateDirectory(targetPathProfile);
                    }
                    var imageUrl = employee.EtedPhotograph.Split("base64,");
                    var photographByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                    targetPathProfile += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}.jpg";
                    using (FileStream fs = new FileStream(targetPathProfile, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(photographByte);
                    }
                    employee.EtedPhotograph = targetPathProfile.Replace(RootPath, "").Replace("\\", "/");
                }
                //HRMS
                    
                emsTblEmployeeDetails.EtedFirstName = employee.EtedFirstName;
                emsTblEmployeeDetails.EtedLastName = employee.EtedLastName;
                emsTblEmployeeDetails.EtedEmailAddress = employee.EtedEmailAddress;
                emsTblEmployeeDetails.EtedContactNumber = employee.EtedContactNumber;
                emsTblEmployeeDetails.EtedOfficialEmailAddress = employee.EtedOfficialEmailAddress;
                emsTblEmployeeDetails.EtedBloodGroup = employee.EtedBloodGroup;
                emsTblEmployeeDetails.EtedCnic = employee.EtedCnic;
                emsTblEmployeeDetails.EtedDob = employee.EtedDob;
                emsTblEmployeeDetails.EtedPhotograph = employee.EtedPhotograph;
                emsTblEmployeeDetails.EtedAddress = employee.EtedAddress;
                emsTblEmployeeDetails.EtedGender = employee.EtedGender;
                emsTblEmployeeDetails.EtedMaritalStatus = employee.EtedMaritalStatus;
                emsTblEmployeeDetails.EtedReligion = employee.EtedReligion;
                emsTblEmployeeDetails.EtedNationality = employee.EtedNationality;
                emsTblEmployeeDetails.EtedStatus = employee.EtedStatus;
                emsTblEmployeeDetails.EtedCreatedBy = employee.EtedCreatedBy;
                emsTblEmployeeDetails.EtedCreatedByName = employee.EtedCreatedByName;
                emsTblEmployeeDetails.EtedCreatedByDate = DateTime.Now;
                emsTblEmployeeDetails.EtedIsDelete = false;
                emsTblEmployeeDetails.EtedManagerId = employee.EtedManagerId;
                if (employee.EtrEthuRoleId == 3) {

                    emsTblEmployeeDetails.EtedIsManager = true;
                }
                else 
                {
                    emsTblEmployeeDetails.EtedIsManager = false;
                }
                _hrmsemployeeRepository.Insert(emsTblEmployeeDetails);
                
                //HRMS
                emsuser.EtrEthuRoleId = employee.EtrEthuRoleId;
                emsuser.EthuFullName = employee.EtedFirstName + " " + employee.EtedLastName;
                emsuser.EthuUserName = emsTblEmployeeDetails.EtedFirstName + emsTblEmployeeDetails.EtedEmployeeId;
                emsuser.EthuGender = employee.EtedGender;
                emsuser.EthuEmailAddress = employee.EtedOfficialEmailAddress;
                emsuser.EthuPhoneNumber = employee.EtedContactNumber;
                emsuser.EthuPassword = employee.EthuPassword;
                emsuser.EthuIsDelete = false;
                emsuser.EthuCreatedBy = employee.EtedCreatedBy;
                emsuser.EthuCreatedByName = employee.EtedCreatedByName;
                emsuser.EthuCreatedByDate = employee.EtedCreatedByDate;
                emsuser.EtedEthuEmpId = emsTblEmployeeDetails.EtedEmployeeId;

                _hrmsUserAuthRepository.Insert(emsuser);
                //Academic
                if (employee.EmsTblAcademicQualification.Count > 0)
                {
                    foreach (var empAcad in employee.EmsTblAcademicQualification)
                    {
                        if (!string.IsNullOrEmpty(empAcad.EtaqUploadDocuments) && empAcad.EtaqUploadDocuments!="")
                        {
                            var RootPath = rootpath;
                            string FilePathAcad = "Images\\EmployeeID\\AcademicQualification\\";
                            var targetPathAcademicQual = Path.Combine(RootPath, FilePathAcad);
                            if (!Directory.Exists(targetPathAcademicQual))
                            {
                                Directory.CreateDirectory(targetPathAcademicQual);
                            }
                            var imageUrl = empAcad.EtaqUploadDocuments.Split(";base64,");
                            var attachmentType = imageUrl[0].Replace("data:", "").ToString();
                            var FileExtension = attachmentType.getFileExtension();
                            var uploadDocumentsByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                            targetPathAcademicQual += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empAcad.EtaqInstituteName}{FileExtension}";
                            using (FileStream fs = new FileStream(targetPathAcademicQual, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(uploadDocumentsByte);
                            }
                            empAcad.EtaqUploadDocuments = targetPathAcademicQual.Replace(RootPath, "").Replace("\\", "/");
                        }
                    }
                    var _emsTblAcademicQualificationList = employee.EmsTblAcademicQualification.Select(x => new EmsTblAcademicQualification
                    {
                        EtaqEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                        EtaqInstituteName = x.EtaqInstituteName,
                        EtaqPassingYear = x.EtaqPassingYear,
                        EtaqCgpa = x.EtaqCgpa,
                        EtaqUploadDocuments = x.EtaqUploadDocuments,
                        EtaqQualification = x.EtaqQualification,
                        EtaqCreatedBy = x.EtaqCreatedBy,
                        EtaqCreatedByDate = DateTime.Now,
                        EtaqCreatedByName = x.EtaqCreatedByName,
                        EtaqIsDelete = false,
                    });
                     _hrmsacademicrepository.Insert(_emsTblAcademicQualificationList.ToList());

                }

                //Professional Qualification
                if (employee.EmsTblProfessionalQualification.Count > 0)
                {
                    foreach (var empProfQual in employee.EmsTblProfessionalQualification)
                    {
                        //Professional Qualification
                        if (!string.IsNullOrEmpty(empProfQual.EtpqDocuments)&& empProfQual.EtpqDocuments !="")
                        {
                            var RootPath = rootpath;
                            string FilePathProf = "Images\\EmployeeID\\ProfessionalQualification\\";
                            var targetPathProfQual = Path.Combine(RootPath, FilePathProf);
                            if (!Directory.Exists(targetPathProfQual))
                            {
                                Directory.CreateDirectory(targetPathProfQual);
                            }
                            var imageUrl = empProfQual.EtpqDocuments.Split(";base64,");
                            var attachmentType = imageUrl[0].Replace("data:", "").ToString();
                            var FileExtension = attachmentType.getFileExtension();
                            var documentsByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                            targetPathProfQual += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empProfQual.EtpqInstituteName}{FileExtension}";
                            using (FileStream fs = new FileStream(targetPathProfQual, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(documentsByte);
                            }
                            empProfQual.EtpqDocuments = targetPathProfQual.Replace(RootPath, "").Replace("\\", "/");
                        }
                    }
                    var _emsTblProfessionalQualificationList = employee.EmsTblProfessionalQualification.Select(x => new EmsTblProfessionalQualification
                    {
                        EtpqEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                        EtpqCertification = x.EtpqCertification,
                        EtpqStratDate = x.EtpqStratDate,
                        EtpqEndDate = x.EtpqEndDate,
                        EtpqDocuments = x.EtpqDocuments,
                        EtpqInstituteName = x.EtpqInstituteName,
                        EtpqCreatedBy = x.EtpqCreatedBy,
                        EtpqCreatedByDate = DateTime.Now,
                        EtpqCreatedByName = x.EtpqCreatedByName,
                        EtpqIsDelete = false,
                    });
                    _hrmsprofessionalrepository.Insert(_emsTblProfessionalQualificationList.ToList());
                }

                //Professional Detail
                if (employee.EmsTblEmployeeProfessionalDetails.Count > 0)
                {
                    var _emsTblEmployeeProfessionalDetailsList = employee.EmsTblEmployeeProfessionalDetails.Select(x => new EmsTblEmployeeProfessionalDetails
                    {
                        EtepdEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                        EtepdDesignation = x.EtepdDesignation,
                        EtepdSalary = x.EtepdSalary,
                        EtepdJoiningDate = x.EtepdJoiningDate,
                        EtepdProbation = x.EtepdProbation,
                        EtepdCreatedBy = x.EtepdCreatedBy,
                        EtepdCreatedByDate = DateTime.Now,
                        EtepdCreatedByName = x.EtepdCreatedByName,
                        EtepdIsDelete = false,
                    });
                    _hrmsprofessionaldetailsrepository.Insert(_emsTblEmployeeProfessionalDetailsList.ToList());
                }

                //Emergency Contact
                if (employee.EmsTblEmergencyContact.Count > 0)
                {
                    var _emsTblEmergencyContactList = employee.EmsTblEmergencyContact.Select(x => new EmsTblEmergencyContact
                    {
                        EtecEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                        EtecFirstName = x.EtecFirstName,
                        EtecLastName = x.EtecLastName,
                        EtecRelation = x.EtecRelation,
                        EtecContactNumber = x.EtecContactNumber,
                        EtecAddress = x.EtecAddress,
                        EtecCreatedBy = x.EtecCreatedBy,
                        EtecCreatedByDate = DateTime.Now,
                        EtecCreatedByName = x.EtecCreatedByName,
                        EtecIsDelete = false,
                    });
                    _employeeContactRepository.Insert(_emsTblEmergencyContactList.ToList());
                }

                //Working History
                if (employee.EmsTblWorkingHistory.Count > 0)
                {
                    foreach (var empWork in employee.EmsTblWorkingHistory)
                    {
                        if (!string.IsNullOrEmpty(empWork.EtwhExperienceLetter) && empWork.EtwhExperienceLetter !="")
                        {
                            var RootPath = rootpath;
                            string FilePathWork = "Images\\EmployeeID\\WorkingHistory\\";
                            var targetPathWork = Path.Combine(RootPath, FilePathWork);
                            if (!Directory.Exists(targetPathWork))
                            {
                                Directory.CreateDirectory(targetPathWork);
                            }
                            var imageUrl = empWork.EtwhExperienceLetter.Split(";base64,");
                            var attachmentType = imageUrl[0].Replace("data:", "").ToString();
                            var FileExtension = attachmentType.getFileExtension();
                            var experienceLetterByte = Convert.FromBase64String(imageUrl[1]);
                            targetPathWork += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empWork.EtwhDesignation}{FileExtension}";
                            using (FileStream fs = new FileStream(targetPathWork, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(experienceLetterByte);
                            }
                            empWork.EtwhExperienceLetter = targetPathWork.Replace(RootPath, "").Replace("\\", "/");
                        }
                    }
                    var _emsTblWorkingHistoryList = employee.EmsTblWorkingHistory.Select(x => new EmsTblWorkingHistory
                    {
                        EtwhEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                        EtwhCompanyName = x.EtwhCompanyName,
                        EtwhDesignation = x.EtwhDesignation,
                        EtwhStratDate = x.EtwhStratDate,
                        EtwhEndDate = x.EtwhEndDate,
                        EtwhExperienceLetter = x.EtwhExperienceLetter,
                        EtwhDuration = x.EtwhDuration,
                        EtwhCreatedBy = x.EtwhCreatedBy,
                        EtwhCreatedByDate = DateTime.Now,
                        EtwhCreatedByName = x.EtwhCreatedByName,
                        EtwhIsDelete = false,
                    });
                    _workinghistoryRepository.Insert(_emsTblWorkingHistoryList.ToList());

                }
               

                if (employee.ImsAssign.Count > 0)
                {
                    
                    foreach (var imsAssign in employee.ImsAssign)
                    {
                        var remaining = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == imsAssign.ItasItaAssetId).Select(y => y.ItaRemaining).FirstOrDefault();
                        var assigned = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == imsAssign.ItasItaAssetId).Select(y => y.ItaAssignQuantity).FirstOrDefault();
                        assigned = assigned + imsAssign.ItasQuantity;
                        remaining = remaining - imsAssign.ItasQuantity;
                        if (remaining > 0) { 
                       _hrmsassetRepository.Table.Where(p => p.ItaAssetId == imsAssign.ItasItaAssetId)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.ItaAssignQuantity = assigned;
                          x.ItaRemaining = remaining;

                      });
                            
                        }
                    }
                    var _imsAssignList = employee.ImsAssign.Select(x => new ImsAssign
                    {
                        ItasEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                        ItasItacCategoryId = x.ItasItacCategoryId,
                        ItasItaAssetId = x.ItasItaAssetId,
                        ItasQuantity = x.ItasQuantity,
                        ItasAssignedDate = x.ItasAssignedDate,
                        ItasCreatedBy = x.ItasCreatedBy.ToString(),
                        ItasCreatedByDate = x.ItasCreatedByDate,
                        ItasCreatedByName = x.ItasCreatedByName,
                        ItasIsDelete = false
                    });
                    _hrmsassetAssignRepository.Insert(_imsAssignList.ToList());

                }
                ///////////////////Add Leave Record//////////////////
                if (employee.Empleaveassign.Count > 0)
                {

                  
                    var _lmsLeaveRecordlist = employee.Empleaveassign.Select(x => new LmsLeaveRecord
                    {
                        LmslrEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                        //LmslrEtedEmployeeId = emsTblEmployeeProfessionalDetails.EtepdDesignation,
                        LmslrAnnualAssign = x.LmslrAnnualAssign,
                        LmslrSickAssign = x.LmslrSickAssign,
                        LmslrCasualAssign = x.LmslrCasualAssign,
                        LmslrTotalAssign = x.LmslrTotalAssign,
                        LmslrAnnualTaken = x.LmslrAnnualTaken,
                        LmslrSickTaken = x.LmslrSickTaken,
                        LmslrCasualTaken = x.LmslrCasualTaken,
                        LmslrTotalTaken = x.LmslrTotalTaken,
                        LmslrCreatedBy = x.LmslrCreatedBy,
                        LmslrCreatedByName = x.LmslrCreatedByName,
                        LmslrCreatedByDate = DateTime.Now,
                        LmslrIsDelete =false,
                    });
                    _hrmsleaverecordrepository.Insert(_lmsLeaveRecordlist.ToList());
                }

                response.Success = true;
                response.Message = UserMessages.strAdded;
                response.Data = null;
            }

            else if (doesEmailExistAlready == true)
            {
                response.Message = UserMessages.strEmailexist;
            }
            else if (doesCNICExistAlready == true)
            {
                response.Message = UserMessages.strCnicexist;
            }
            else
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotinsert;
            }

            return response;
        }
        public BaseResponse UpdateEmployee(EmsTblEmployeeDetailsVM employee, string rootpath)
        {

            BaseResponse response = new BaseResponse();
            EmsTblEmployeeDetails emsTblEmployeeDetails = new EmsTblEmployeeDetails();
            EmsTblEmployeeDetails emsTblEmployeeDetailsInsert = new EmsTblEmployeeDetails();
            EmsTblHrmsUser emsTblUser = new EmsTblHrmsUser();
            var userid = _hrmsUserAuthRepository.Table.Where(x => x.EtedEthuEmpId == employee.EtedEmployeeId).Select(y => y.EthuUserId).FirstOrDefault();

            bool count = _hrmsemployeeRepository.Table.Where(p => p.EtedEmployeeId == employee.EtedEmployeeId).Count() > 0;
            if (count == true)
            {
                if (!string.IsNullOrEmpty(employee.EtedPhotograph))
                {
                    var RootPath = rootpath;
                    string FilePathPhoto = "Images\\EmployeeID\\PhotoGraph\\";
                    var targetPathProfile = Path.Combine(RootPath, FilePathPhoto);

                    if (!Directory.Exists(targetPathProfile))
                    {
                        Directory.CreateDirectory(targetPathProfile);
                    }
                    var imageUrl = employee.EtedPhotograph.Split("base64,");
                    var photographByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                    targetPathProfile += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}.jpg";
                    using (FileStream fs = new FileStream(targetPathProfile, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(photographByte);
                    }
                    employee.EtedPhotograph = targetPathProfile.Replace(RootPath, "").Replace("\\", "/");
                }
                else
                {
                    employee.EtedPhotograph = _hrmsemployeeRepository.Table.Where(e => e.EtedEmployeeId == employee.EtedEmployeeId && e.EtedIsDelete != true).Select(x=>x.EtedPhotograph).FirstOrDefault();
                }


                emsTblEmployeeDetails.EtedEmployeeId = employee.EtedEmployeeId;
                emsTblEmployeeDetails.EtedFirstName = employee.EtedFirstName;
                emsTblEmployeeDetails.EtedPhotograph = employee.EtedPhotograph;
                emsTblEmployeeDetails.EtedLastName = employee.EtedLastName;
                emsTblEmployeeDetails.EtedEmailAddress = employee.EtedEmailAddress;
                emsTblEmployeeDetails.EtedDob = employee.EtedDob;
                emsTblEmployeeDetails.EtedContactNumber = employee.EtedContactNumber;
                emsTblEmployeeDetails.EtedAddress = employee.EtedAddress;
                emsTblEmployeeDetails.EtedGender = employee.EtedGender;
                emsTblEmployeeDetails.EtedMaritalStatus = employee.EtedMaritalStatus;
                emsTblEmployeeDetails.EtedBloodGroup = employee.EtedBloodGroup;
                emsTblEmployeeDetails.EtedCnic = employee.EtedCnic;
                emsTblEmployeeDetails.EtedOfficialEmailAddress = employee.EtedEmailAddress;
                emsTblEmployeeDetails.EtedReligion = employee.EtedReligion;
                emsTblEmployeeDetails.EtedNationality = employee.EtedNationality;
                emsTblEmployeeDetails.EtedStatus = employee.EtedStatus;
                emsTblEmployeeDetails.EtedIsDelete = false;
                emsTblEmployeeDetails.EtedModifiedByDate = DateTime.Now;
                emsTblEmployeeDetails.EtedModifiedBy = employee.EtedModifiedBy;
                emsTblEmployeeDetails.EtedModifiedByName = employee.EtedModifiedByName;
                emsTblEmployeeDetails.EtedManagerId = employee.EtedManagerId;
                if (employee.EtrEthuRoleId == 3)
                {

                    emsTblEmployeeDetails.EtedIsManager = true;
                }
                else
                {
                    emsTblEmployeeDetails.EtedIsManager = false;
                }

                _hrmsemployeeRepository.Update(emsTblEmployeeDetails);


                emsTblUser.EthuUserId = userid;
                emsTblUser.EthuPassword = employee.EthuPassword;
                emsTblUser.EthuGender = employee.EtedGender;
                emsTblUser.EthuEmailAddress = employee.EtedOfficialEmailAddress;
                emsTblUser.EthuFullName = employee.EtedFirstName + " " + employee.EtedLastName;
                emsTblUser.EtrEthuRoleId = employee.EtrEthuRoleId;
                emsTblUser.EthuUserName = employee.EtedFirstName + " " + employee.EtedEmployeeId;
                emsTblUser.EthuPhoneNumber = employee.EtedContactNumber;
                emsTblUser.EtedEthuEmpId = employee.EtedEmployeeId;
                emsTblUser.EtrEthuRoleId = employee.EtrEthuRoleId;
                emsTblUser.EthuIsDelete = false;
                emsTblUser.EthuModifiedBy = employee.EtedModifiedBy;
                emsTblUser.EthuModifiedByDate = DateTime.Now;
                emsTblUser.EthuModifiedByName = employee.EtedModifiedByName;

                _hrmsUserAuthRepository.Update(emsTblUser);

                if (employee.EmsTblAcademicQualification.Count > 0)
                {
                    foreach (var empAcad in employee.EmsTblAcademicQualification)
                    {
                        if (!string.IsNullOrEmpty(empAcad.EtaqUploadDocuments))
                        {
                            var RootPath = rootpath;
                            string FilePathAcad = "Images\\EmployeeID\\AcademicQualification\\";
                            var targetPathAcademicQual = Path.Combine(RootPath, FilePathAcad);
                            if (!Directory.Exists(targetPathAcademicQual))
                            {
                                Directory.CreateDirectory(targetPathAcademicQual);
                            }
                            var imageUrl = empAcad.EtaqUploadDocuments.Split(";base64,");
                            var attachmentType = imageUrl[0].Replace("data:", "").ToString();
                            var FileExtension = attachmentType.getFileExtension();
                            var uploadDocumentsByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                            targetPathAcademicQual += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empAcad.EtaqInstituteName}{FileExtension}";
                            using (FileStream fs = new FileStream(targetPathAcademicQual, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(uploadDocumentsByte);
                            }
                            empAcad.EtaqUploadDocuments = targetPathAcademicQual.Replace(RootPath, "").Replace("\\", "/");
                        }
                        else
                        {
                            empAcad.EtaqUploadDocuments = _hrmsacademicrepository.Table.Where(x => x.EtaqAqId == empAcad.EtaqAqId).Select(z=>z.EtaqUploadDocuments).FirstOrDefault();
                        }
                    }
                    var _emsTblAcademicQualificationList = employee.EmsTblAcademicQualification.Where(z => z.EtaqAqId > 0).Select(x => new EmsTblAcademicQualification
                    {
                        EtaqAqId = x.EtaqAqId,
                        EtaqUploadDocuments = x.EtaqUploadDocuments,
                        EtaqEtedEmployeeId = employee.EtedEmployeeId,
                        EtaqInstituteName = x.EtaqInstituteName,
                        EtaqPassingYear = x.EtaqPassingYear,
                        EtaqCgpa = x.EtaqCgpa,
                        EtaqQualification = x.EtaqQualification,
                        EtaqCreatedBy = x.EtaqCreatedBy,
                        EtaqCreatedByDate = DateTime.Now,
                        EtaqCreatedByName = x.EtaqCreatedByName,
                        EtaqIsDelete = false,
                    });
                    if (_emsTblAcademicQualificationList.Count() > 0)
                    {
                        _hrmsacademicrepository.Update(_emsTblAcademicQualificationList.ToList());
                    }
                    var _emsTblAcademicQualificationList1 = employee.EmsTblAcademicQualification.Where(z => z.EtaqAqId == 0).Select(x => new EmsTblAcademicQualification
                    {
                        EtaqUploadDocuments = x.EtaqUploadDocuments,
                        EtaqEtedEmployeeId = employee.EtedEmployeeId,
                        EtaqInstituteName = x.EtaqInstituteName,
                        EtaqPassingYear = x.EtaqPassingYear,
                        EtaqCgpa = x.EtaqCgpa,
                        EtaqQualification = x.EtaqQualification,
                        EtaqCreatedBy = x.EtaqCreatedBy,
                        EtaqCreatedByDate = DateTime.Now,
                        EtaqCreatedByName = x.EtaqCreatedByName,
                        EtaqIsDelete = false,
                    });
                    if (_emsTblAcademicQualificationList1.Count() > 0)
                    {
                        _hrmsacademicrepository.Insert(_emsTblAcademicQualificationList1.ToList());
                    }
                }

                if (employee.EmsTblProfessionalQualification.Count > 0)
                {
                    foreach (var empProfQual in employee.EmsTblProfessionalQualification)
                    {
                        //Professional Qualification
                        if (!string.IsNullOrEmpty(empProfQual.EtpqDocuments))
                        {
                            var RootPath = rootpath;
                            string FilePathProf = "Images\\EmployeeID\\ProfessionalQualification\\";
                            var targetPathProfQual = Path.Combine(RootPath, FilePathProf);
                            if (!Directory.Exists(targetPathProfQual))
                            {
                                Directory.CreateDirectory(targetPathProfQual);
                            }
                            var imageUrl = empProfQual.EtpqDocuments.Split(";base64,");
                            var attachmentType = imageUrl[0].Replace("data:", "").ToString();
                            var FileExtension = attachmentType.getFileExtension();
                            var documentsByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                            targetPathProfQual += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empProfQual.EtpqInstituteName}{FileExtension}";
                            using (FileStream fs = new FileStream(targetPathProfQual, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(documentsByte);
                            }
                            empProfQual.EtpqDocuments = targetPathProfQual.Replace(RootPath, "").Replace("\\", "/");
                        }
                        else
                        {
                            empProfQual.EtpqDocuments = _hrmsprofessionalrepository.Table.Where(x => x.EtpqPqId == empProfQual.EtpqPqId).Select(z => z.EtpqDocuments).FirstOrDefault();
                        }
                    }
                    var _emsTblProfessionalQualificationList = employee.EmsTblProfessionalQualification.Where(z => z.EtpqPqId > 0).Select(x => new EmsTblProfessionalQualification
                    {
                        EtpqPqId = x.EtpqPqId,
                        EtpqDocuments = x.EtpqDocuments,
                        EtpqEtedEmployeeId = employee.EtedEmployeeId,
                        EtpqCertification = x.EtpqCertification,
                        EtpqStratDate = x.EtpqStratDate,
                        EtpqEndDate = x.EtpqEndDate,
                        EtpqInstituteName = x.EtpqInstituteName,
                        EtpqCreatedBy = x.EtpqCreatedBy,
                        EtpqCreatedByDate = DateTime.Now,
                        EtpqCreatedByName = x.EtpqCreatedByName,
                        EtpqIsDelete = false,
                    });
                    if (_emsTblProfessionalQualificationList.Count() > 0)
                    {
                        _hrmsprofessionalrepository.Update(_emsTblProfessionalQualificationList.ToList());
                    }

                    var _emsTblProfessionalQualificationList1 = employee.EmsTblProfessionalQualification.Where(z => z.EtpqPqId == 0).Select(x => new EmsTblProfessionalQualification
                    {
                        EtpqDocuments = x.EtpqDocuments,
                        EtpqEtedEmployeeId = employee.EtedEmployeeId,
                        EtpqCertification = x.EtpqCertification,
                        EtpqStratDate = x.EtpqStratDate,
                        EtpqEndDate = x.EtpqEndDate,
                        EtpqInstituteName = x.EtpqInstituteName,
                        EtpqCreatedBy = x.EtpqCreatedBy,
                        EtpqCreatedByDate = DateTime.Now,
                        EtpqCreatedByName = x.EtpqCreatedByName,
                        EtpqIsDelete = false,
                    });
                    if (_emsTblProfessionalQualificationList1.Count() > 0)
                    {
                        _hrmsprofessionalrepository.Insert(_emsTblProfessionalQualificationList1.ToList());
                    }
                }

                if (employee.EmsTblPermanentEmployee.Count > 0 && employee.EmsTblPermanentEmployee != null || employee.EmsTblContractEmployee.Count > 0 && employee.EmsTblContractEmployee != null ||employee.Empreleaseddata.Count>0 && employee.Empreleaseddata
                    != null || employee.Empresigneddata.Count>0 && employee.Empresigneddata!= null || employee.EmsTblPartTimeEmployee.Count>0 && employee.EmsTblPartTimeEmployee!=null || employee.EmsTblInterneedata.Count>0 && employee.EmsTblInterneedata!=null)
                {
                    var value =Convert.ToInt32(employee.EmsTblEmployeeProfessionalDetails.Select(x => x.EtedempStatus).FirstOrDefault());
                    if (value == 1)
                    {
                        var _emsEmpStatusList = employee.EmsTblPermanentEmployee.Where(z => z.EesEmployementId == 0).Select(x => new EmsEmployementStatus
                        {
                            EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                            EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                            EesStartDate = x.EesStartDate,
                            EesEndDate = Convert.ToDateTime(x.EesEndDate),
                            EesDuration = x.EesDuration,
                            EesIncrement = x.EesIncrement,
                            EesDateOfIncrement = x.EesDateOfIncrement,
                            EesRemarks = x.EesRemarks,
                            EesCreatedBy = x.EesCreatedBy,
                            EesCreatedByDate = DateTime.Now,
                            EesCreatedByName = x.EesCreatedByName,
                            EesIsDelete = false,
                        });
                        if (_emsEmpStatusList.Count() > 0)
                        {
                            _hrmsstatusRepository.Insert(_emsEmpStatusList.ToList());
                        }
                        var _emsEmpStatusList1 = employee.EmsTblPermanentEmployee.Where(z => z.EesEmployementId > 0).Select(x => new EmsEmployementStatus
                        {
                            EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                            EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                            EesStartDate = x.EesStartDate,
                            EesEndDate = Convert.ToDateTime(x.EesEndDate),
                            EesDuration = x.EesDuration,
                            EesIncrement = x.EesIncrement,
                            EesDateOfIncrement = x.EesDateOfIncrement,
                            EesRemarks = x.EesRemarks,
                            EesCreatedBy = x.EesCreatedBy,
                            EesCreatedByDate = DateTime.Now,
                            EesCreatedByName = x.EesCreatedByName,
                            EesIsDelete = false,
                        });
                        if (_emsEmpStatusList.Count() > 0)
                        {
                            _hrmsstatusRepository.Update(_emsEmpStatusList.ToList());
                        }
                    }
                    else if (value == 2)
                    {
                        var _emsEmpStatusList = employee.EmsTblContractEmployee.Where(z => z.EesEmployementId == 0).Select(x => new EmsEmployementStatus
                        {
                            EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                            EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                            EesStartDate = x.EesStartDate,
                            EesEndDate = Convert.ToDateTime(x.EesEndDate),
                            EesDuration = x.EesDuration,
                            EesContractType = x.EesContractType,
                            EesSalary = x.EesSalary,
                            EesRemarks = x.EesRemarks,
                            EesCreatedBy = x.EesCreatedBy,
                            EesCreatedByDate = DateTime.Now,
                            EesCreatedByName = x.EesCreatedByName,
                            EesIsDelete = false,
                        });
                        if (_emsEmpStatusList.Count() > 0)
                        {
                            _hrmsstatusRepository.Insert(_emsEmpStatusList.ToList());
                        }
                        var _emsEmpStatusList1 = employee.EmsTblContractEmployee.Where(z => z.EesEmployementId > 0).Select(x => new EmsEmployementStatus
                        {
                            EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                            EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                            EesStartDate = x.EesStartDate,
                            EesEndDate = Convert.ToDateTime(x.EesEndDate),
                            EesDuration = x.EesDuration,
                            EesContractType = x.EesContractType,
                            EesSalary = x.EesSalary,
                            EesRemarks = x.EesRemarks,
                            EesCreatedBy = x.EesCreatedBy,
                            EesCreatedByDate = DateTime.Now,
                            EesCreatedByName = x.EesCreatedByName,
                            EesIsDelete = false,
                        });
                        if (_emsEmpStatusList.Count() > 0)
                        {
                            _hrmsstatusRepository.Update(_emsEmpStatusList.ToList());
                        }
                    }
                    else if (value == 3)
                    {
                        var _emsEmpstatusList = employee.Empreleaseddata.Where(z => z.EesEmployementId == 0).Select(x => new EmsEmployementStatus
                        {
                            EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                            EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                            EesStartDate = x.EesStartDate,
                            EesEndDate = DateTime.Parse(x.EesEndDate, System.Globalization.CultureInfo.InvariantCulture),
                            EesDuration = x.EesDuration,
                            EesClearenceDate = Convert.ToDateTime(x.EesClearenceDate),
                            EesRemarks = x.EesRemarks,
                            EesCreatedBy = x.EesCreatedBy,
                            EesCreatedByName = x.EesCreatedByName,
                            EesCreatedByDate = DateTime.Now,
                            EesIsDelete = false
                        });
                        if (_emsEmpstatusList.Count() > 0)
                        {
                            _hrmsstatusRepository.Insert(_emsEmpstatusList.ToList());
                        }

                        var _emsEmpstatusList1 = employee.Empreleaseddata.Where(z => z.EesEmployementId > 0).Select(x => new EmsEmployementStatus
                        {
                            EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                            EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                            EesStartDate = x.EesStartDate,
                            EesEndDate = DateTime.Parse(x.EesEndDate, System.Globalization.CultureInfo.InvariantCulture),
                            EesDuration = x.EesDuration,
                            EesClearenceDate = Convert.ToDateTime(x.EesClearenceDate),
                            EesRemarks = x.EesRemarks,
                            EesCreatedBy = x.EesCreatedBy,
                            EesCreatedByName = x.EesCreatedByName,
                            EesCreatedByDate = DateTime.Now,
                            EesIsDelete = false
                        });
                        if (_emsEmpstatusList.Count() > 0)
                        {
                            _hrmsstatusRepository.Update(_emsEmpstatusList.ToList());
                        }
                    }
                    else if (value == 4)
                    {
                        var _emsEmpstatusList = employee.Empresigneddata.Where(z => z.EesEmployementId == 0).Select(x => new EmsEmployementStatus
                        {
                            EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                            EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                            EesStartDate = x.EesStartDate,
                            EesEndDate = Convert.ToDateTime(x.EesEndDate),
                            EesClearenceDate =Convert.ToDateTime(x.EesClearenceDate),
                            EesDuration = x.EesDuration,
                            EesRemarks = x.EesRemarks,
                            EesCreatedBy = x.EesCreatedBy,
                            EesCreatedByName = x.EesCreatedByName,
                            EesCreatedByDate = DateTime.Now,
                            EesIsDelete = false
                        });
                        if (_emsEmpstatusList.Count() > 0)
                        {
                            _hrmsstatusRepository.Insert(_emsEmpstatusList.ToList());
                        }
                        var _emsEmpstatusList1 = employee.Empresigneddata.Where(z => z.EesEmployementId > 0).Select(x => new EmsEmployementStatus
                        {
                            EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                            EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                            EesStartDate = x.EesStartDate,
                            EesEndDate = Convert.ToDateTime(x.EesEndDate),
                            EesClearenceDate = x.EesClearenceDate,
                            EesDuration = x.EesDuration,
                            EesRemarks = x.EesRemarks,
                            EesCreatedBy = x.EesCreatedBy,
                            EesCreatedByName = x.EesCreatedByName,
                            EesCreatedByDate = DateTime.Now,
                            EesIsDelete = false
                        });
                        if (_emsEmpstatusList.Count() > 0)
                        {
                            _hrmsstatusRepository.Update(_emsEmpstatusList.ToList());
                        }
                    }

                else if (value== 5)
                {
                    var _emsEmpstatusList = employee.EmsTblInterneedata.Where(z => z.EesEmployementId == 0).Select(x => new EmsEmployementStatus
                    {
                        EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                        EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                        EesStartDate = x.EesStartDate,
                        EesEndDate = DateTime.Parse(x.EesEndDate, System.Globalization.CultureInfo.InvariantCulture),
                        EesClearenceDate = Convert.ToDateTime(x.EesClearenceDate),
                        EesDuration = x.EesDuration,
                        EesRemarks = x.EesRemarks,
                        EesCreatedBy = x.EesCreatedBy,
                        EesCreatedByName = x.EesCreatedByName,
                        EesCreatedByDate = DateTime.Now,
                        EesIsDelete = false
                    });
                    if (_emsEmpstatusList.Count() > 0)
                    {
                        _hrmsstatusRepository.Insert(_emsEmpstatusList.ToList());
                    }
                    var _emsEmpstatusList1 = employee.EmsTblInterneedata.Where(z => z.EesEmployementId > 0).Select(x => new EmsEmployementStatus
                    {
                        EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                        EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                        EesStartDate = x.EesStartDate,
                        EesEndDate = Convert.ToDateTime(x.EesEndDate),
                        EesClearenceDate = x.EesClearenceDate,
                        EesDuration = x.EesDuration,
                        EesRemarks = x.EesRemarks,
                        EesCreatedBy = x.EesCreatedBy,
                        EesCreatedByName = x.EesCreatedByName,
                        EesCreatedByDate = DateTime.Now,
                        EesIsDelete = false
                    });
                    if (_emsEmpstatusList.Count() > 0)
                    {
                        _hrmsstatusRepository.Update(_emsEmpstatusList.ToList());
                    }
                }
            else if (value == 6)
            {
                var _emsEmpstatusList = employee.Empresigneddata.Where(z => z.EesEmployementId == 0).Select(x => new EmsEmployementStatus
                {
                    EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                    EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                    EesStartDate = x.EesStartDate,
                    EesEndDate = Convert.ToDateTime(x.EesEndDate),
                    EesEtedParttimeType = x.EesetedpartTimetype.ToString(),
                    EesDays=x.EesDays,
                    EesClearenceDate = x.EesClearenceDate,
                    EesDuration = x.EesDuration,
                    EesRemarks = x.EesRemarks,
                    EesCreatedBy = x.EesCreatedBy,
                    EesCreatedByName = x.EesCreatedByName,
                    EesCreatedByDate = DateTime.Now,
                    EesIsDelete = false
                });
                if (_emsEmpstatusList.Count() > 0)
                {
                    _hrmsstatusRepository.Insert(_emsEmpstatusList.ToList());
                }
                var _emsEmpstatusList1 = employee.Empresigneddata.Where(z => z.EesEmployementId > 0).Select(x => new EmsEmployementStatus
                {
                    EesEcsEmpstatusId = x.EesEcsEmpstatusId,
                    EesEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                    EesStartDate = x.EesStartDate,
                    EesEndDate = Convert.ToDateTime(x.EesEndDate),
                    EesClearenceDate = x.EesClearenceDate,
                    EesEtedParttimeType = x.EesetedpartTimetype.ToString(),
                    EesDays = x.EesDays,
                    EesDuration = x.EesDuration,
                    EesRemarks = x.EesRemarks,
                    EesCreatedBy = x.EesCreatedBy,
                    EesCreatedByName = x.EesCreatedByName,
                    EesCreatedByDate = DateTime.Now,
                    EesIsDelete = false
                });
                if (_emsEmpstatusList.Count() > 0)
                {
                    _hrmsstatusRepository.Update(_emsEmpstatusList.ToList());
                }
            }
        }
            if (employee.EmsTblEmployeeProfessionalDetails.Count > 0)
                {
                    
                    var incremnent = employee.EmsTblPermanentEmployee.Select(x => x.EesIncrement).ToList() ?? null;
                    if (incremnent == null)
                    {
                        incremnent[0] = 0;   
                    }
                  
                    
                    var _emsTblEmployeeProfessionalDetailsList1 = employee.EmsTblEmployeeProfessionalDetails.Where(z => z.EtepdPdId > 0).Select(x => new EmsTblEmployeeProfessionalDetails
                    {
                        EtepdPdId = x.EtepdPdId,
                        EtepdEtedEmployeeId = employee.EtedEmployeeId,
                        EtepdDesignation = x.EtepdDesignation,
                        EtepdSalary = x.EtepdSalary+incremnent,
                        EtepdJoiningDate = x.EtepdJoiningDate,
                        EtepdProbation = x.EtepdProbation,
                        EtepdCreatedBy = x.EtepdCreatedBy,
                        EtepdCreatedByDate = DateTime.Now,
                        EtepdCreatedByName = x.EtepdCreatedByName,
                        EtepdIsDelete = false,
                    });
                    if (_emsTblEmployeeProfessionalDetailsList1.Count() > 0)
                    {
                        _hrmsprofessionaldetailsrepository.Update(_emsTblEmployeeProfessionalDetailsList1.ToList());
                    }
                }

                if (employee.EmsTblEmergencyContact.Count > 0)
                {
                    var _emsTblEmergencyContactList1 = employee.EmsTblEmergencyContact.Where(z => z.EtecEcId > 0).Select(x => new EmsTblEmergencyContact
                    {
                        EtecEcId = x.EtecEcId,
                        EtecEtedEmployeeId = employee.EtedEmployeeId,
                        EtecFirstName = x.EtecFirstName,
                        EtecLastName = x.EtecLastName,
                        EtecRelation = x.EtecRelation,
                        EtecContactNumber = x.EtecContactNumber,
                        EtecAddress = x.EtecAddress,
                        EtecCreatedBy = x.EtecCreatedBy,
                        EtecCreatedByDate = DateTime.Now,
                        EtecCreatedByName = x.EtecCreatedByName,
                        EtecIsDelete = false,
                    });
                    if (_emsTblEmergencyContactList1.Count() > 0)
                    {
                        _employeeContactRepository.Update(_emsTblEmergencyContactList1.ToList());
                    }

                    var _emsTblEmergencyContactList = employee.EmsTblEmergencyContact.Where(z => z.EtecEcId == 0).Select(x => new EmsTblEmergencyContact
                    {
                        
                        EtecEtedEmployeeId = employee.EtedEmployeeId,
                        EtecFirstName = x.EtecFirstName,
                        EtecLastName = x.EtecLastName,
                        EtecRelation = x.EtecRelation,
                        EtecContactNumber = x.EtecContactNumber,
                        EtecAddress = x.EtecAddress,
                        EtecCreatedBy = x.EtecCreatedBy,
                        EtecCreatedByDate = DateTime.Now,
                        EtecCreatedByName = x.EtecCreatedByName,
                        EtecIsDelete = false,
                    });
                    if (_emsTblEmergencyContactList1.Count() > 0)
                    {
                        _employeeContactRepository.Insert(_emsTblEmergencyContactList.ToList());
                    }
                }

                if (employee.EmsTblWorkingHistory.Count > 0)
                {
                    foreach (var empWork in employee.EmsTblWorkingHistory)
                    {
                        if (!string.IsNullOrEmpty(empWork.EtwhExperienceLetter))
                        {
                            var RootPath = rootpath;
                            string FilePathWork = "Images\\EmployeeID\\WorkingHistory\\";
                            var targetPathWork = Path.Combine(RootPath, FilePathWork);
                            if (!Directory.Exists(targetPathWork))
                            {
                                Directory.CreateDirectory(targetPathWork);
                            }
                            var imageUrl = empWork.EtwhExperienceLetter.Split(";base64,");
                            var attachmentType = imageUrl[0].Replace("data:", "").ToString();
                            var FileExtension = attachmentType.getFileExtension();
                            var experienceLetterByte = Convert.FromBase64String(imageUrl[1]);
                            targetPathWork += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empWork.EtwhDesignation}{FileExtension}";
                            using (FileStream fs = new FileStream(targetPathWork, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(experienceLetterByte);
                            }
                            empWork.EtwhExperienceLetter = targetPathWork.Replace(RootPath, "").Replace("\\", "/");
                        }
                        else
                        {
                            empWork.EtwhExperienceLetter = _workinghistoryRepository.Table.Where(x => x.EtwhWhId == empWork.EtwhWhId).Select(z => z.EtwhExperienceLetter).FirstOrDefault();
                        }
                    }

                    var _emsTblWorkingHistoryList1 = employee.EmsTblWorkingHistory.Where(z => z.EtwhWhId > 0).Select(x => new EmsTblWorkingHistory
                    {
                        EtwhEtedEmployeeId = employee.EtedEmployeeId,
                        EtwhWhId = x.EtwhWhId,
                        EtwhCompanyName = x.EtwhCompanyName,
                        EtwhDesignation = x.EtwhDesignation,
                        EtwhStratDate = x.EtwhStratDate,
                        EtwhEndDate = x.EtwhEndDate,
                        EtwhExperienceLetter = x.EtwhExperienceLetter,
                        EtwhDuration = x.EtwhDuration,
                        EtwhCreatedBy = x.EtwhCreatedBy,
                        EtwhCreatedByDate = DateTime.Now,
                        EtwhCreatedByName = x.EtwhCreatedByName,
                        EtwhIsDelete = false,
                    });
                    if (_emsTblWorkingHistoryList1.Count() > 0)
                    {
                        _workinghistoryRepository.Update(_emsTblWorkingHistoryList1.ToList());
                    }
                    var _emsTblWorkingHistoryList = employee.EmsTblWorkingHistory.Where(z => z.EtwhWhId == 0).Select(x => new EmsTblWorkingHistory
                    {
                        EtwhEtedEmployeeId = employee.EtedEmployeeId,
                        EtwhCompanyName = x.EtwhCompanyName,
                        EtwhDesignation = x.EtwhDesignation,
                        EtwhStratDate = x.EtwhStratDate,
                        EtwhEndDate = x.EtwhEndDate,
                        EtwhExperienceLetter = x.EtwhExperienceLetter,
                        EtwhDuration = x.EtwhDuration,
                        EtwhCreatedBy = x.EtwhCreatedBy,
                        EtwhCreatedByDate = DateTime.Now,
                        EtwhCreatedByName = x.EtwhCreatedByName,
                        EtwhIsDelete = false,
                    });
                    if (_emsTblWorkingHistoryList.Count() > 0)
                    {
                        _workinghistoryRepository.Insert(_emsTblWorkingHistoryList.ToList());
                    }

                }

                if (employee.ImsAssign.Count > 0)
                {

                    foreach (var imsAssign in employee.ImsAssign)
                    {

                        var remaining = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == imsAssign.ItasItaAssetId).Select(y => y.ItaRemaining).FirstOrDefault();
                        var assigned = _hrmsassetRepository.Table.Where(x => x.ItaAssetId == imsAssign.ItasItaAssetId).Select(y => y.ItaAssignQuantity).FirstOrDefault();
                        assigned = assigned + imsAssign.ItasQuantity;
                        remaining = remaining - imsAssign.ItasQuantity;
                        if (remaining > 0)
                        {
                            _hrmsassetRepository.Table.Where(p => p.ItaAssetId == imsAssign.ItasItaAssetId)
                           .ToList()
                           .ForEach(x =>
                           {
                               x.ItaAssignQuantity = assigned;
                               x.ItaRemaining = remaining;

                           });
                            if (imsAssign.ItasAssignId > 0) { 
                            var _imsAssignList = new ImsAssign
                            {
                                ItasAssignId=imsAssign.ItasAssignId,
                                ItasEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                                ItasItacCategoryId = imsAssign.ItasItacCategoryId,
                                ItasItaAssetId = imsAssign.ItasItaAssetId,
                                ItasQuantity = imsAssign.ItasQuantity,
                                ItasAssignedDate = imsAssign.ItasAssignedDate,
                                ItasCreatedBy = imsAssign.ItasCreatedBy.ToString(),
                                ItasCreatedByDate = imsAssign.ItasCreatedByDate,
                                ItasCreatedByName = imsAssign.ItasCreatedByName,
                                ItasIsDelete = false
                            };
                                _hrmsassetAssignRepository.Update(_imsAssignList);
                            }
                            else
                            {
                                var _imsAssignList = new ImsAssign
                                {
                                    ItasEtedEmployeeId = emsTblEmployeeDetails.EtedEmployeeId,
                                    ItasItacCategoryId = imsAssign.ItasItacCategoryId,
                                    ItasItaAssetId = imsAssign.ItasItaAssetId,
                                    ItasQuantity = imsAssign.ItasQuantity,
                                    ItasAssignedDate = imsAssign.ItasAssignedDate,
                                    ItasCreatedBy = imsAssign.ItasCreatedBy.ToString(),
                                    ItasCreatedByDate = imsAssign.ItasCreatedByDate,
                                    ItasCreatedByName = imsAssign.ItasCreatedByName,
                                    ItasIsDelete = false
                                };
                                _hrmsassetAssignRepository.Insert(_imsAssignList);
                            }
                        }

                    }

                }



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
        public BaseResponse DeleteEmployee(int empid)
        {
            BaseResponse response = new BaseResponse();
            bool doesExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedEmployeeId == empid) > 0;
            bool isDeletedAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedIsDelete == true && p.EtedEmployeeId == empid) > 0;
            _hrmsemployeeRepository.Table.Where(p => p.EtedEmployeeId == empid)
                      .ToList()
                      .ForEach(x =>
                      {
                          x.EtedIsDelete = true;
                      });
            _hrmsacademicrepository.Table.Where(p => p.EtaqEtedEmployeeId == empid)
                .ToList()
                .ForEach(x =>
                {
                    x.EtaqIsDelete = true;
                });
            _hrmsprofessionalrepository.Table.Where(p => p.EtpqEtedEmployeeId == empid)
                .ToList()
                .ForEach(x =>
                {
                    x.EtpqIsDelete = true;
                });

            _employeeContactRepository.Table.Where(p => p.EtecEtedEmployeeId == empid)
               .ToList()
               .ForEach(x =>
               {

                   x.EtecIsDelete = true;
               });
            _workinghistoryRepository.Table.Where(p => p.EtwhEtedEmployeeId == empid)
               .ToList()
               .ForEach(x =>
               {
                   x.EtwhIsDelete = true;
               });

            _hrmsprofessionaldetailsrepository.Table.Where(p => p.EtepdEtedEmployeeId == empid)
               .ToList()
               .ForEach(x =>
               {
                   x.EtepdIsDelete = true;
               });

            _uow.Commit();
            if (doesExistAlready == true && isDeletedAlready == false)
            {
                response.Message = UserMessages.strDeleted;
                response.Success = true;
                response.Data = empid;
            }
            else if (isDeletedAlready == true && doesExistAlready == true)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strAlrdeleted;
            }
            else if (doesExistAlready == false)
            {
                response.Data = null;
                response.Success = false;
                response.Message = UserMessages.strNotfound;
            }

            return response;
        }

        public BaseResponse ViewDataEmployeeByid(int id)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false && z.EtedEmployeeId == id).Count() > 0;
            var employeesData = (from emp in this._hrmsemployeeRepository.Table
                                 where emp.EtedEmployeeId == id
                                 select new EmsTblEmployeeDetails
                                 {
                                     EtedEmployeeId = emp.EtedEmployeeId,
                                     EtedFirstName = emp.EtedFirstName,
                                     EtedLastName = emp.EtedLastName,
                                     EtedEmailAddress = emp.EtedEmailAddress,
                                     EtedDob = emp.EtedDob,
                                     EtedContactNumber = emp.EtedContactNumber,
                                     EtedAddress = emp.EtedAddress,
                                     EtedGender = emp.EtedGender,
                                     EtedMaritalStatus = emp.EtedMaritalStatus,
                                     EtedBloodGroup = emp.EtedBloodGroup,
                                     EtedPhotograph = emp.EtedPhotograph,
                                     EtedCnic = emp.EtedCnic,
                                     EtedOfficialEmailAddress = emp.EtedOfficialEmailAddress,
                                     EtedReligion = emp.EtedReligion,
                                     EtedNationality = emp.EtedNationality,
                                     EtedStatus = emp.EtedStatus,
                                     EtedIsManager = emp.EtedIsManager,
                                     EtedManagerId = emp.EtedManagerId,
                                     EtedCreatedBy = emp.EtedCreatedBy,
                                     EtedCreatedByName = emp.EtedCreatedByName,
                                     EtedModifiedBy = emp.EtedModifiedBy,
                                     EtedModifiedByName = emp.EtedModifiedByName,
                                     EtedModifiedByDate = emp.EtedModifiedByDate,
                                     EtedIsDelete = emp.EtedIsDelete,

                                     EmsTblProfessionalQualification = (from pq in this._hrmsprofessionalrepository.Table where pq.EtpqEtedEmployeeId == id select pq).ToList(),
                                     EmsEmployementStatus = (from es in this._hrmsstatusRepository.Table where es.EesEtedEmployeeId == id select es).ToList(),
                                     EmsTblAcademicQualification = (from aq in this._hrmsacademicrepository.Table where aq.EtaqEtedEmployeeId == id select aq).ToList(),
                                     EmsTblEmergencyContact = (from ec in this._employeeContactRepository.Table where ec.EtecEtedEmployeeId == id select ec).ToList(),
                                     EmsTblEmployeeProfessionalDetails= (from pd in this._hrmsprofessionaldetailsrepository.Table where pd.EtepdEtedEmployeeId== id select pd).ToList(),
                                     EmsTblHrmsUser = (from hu in this._hrmsUserAuthRepository.Table where hu.EtedEthuEmpId== id select hu).ToList(),
                                     EmsTblWorkingHistory = (from wh in this._workinghistoryRepository.Table where wh.EtwhEtedEmployeeId== id select wh).ToList(),
                                     ImsAssign = (from ia in this._hrmsassetAssignRepository.Table where ia.ItasEtedEmployeeId == id select ia).ToList(),
                                     LmsLeaveRecord = (from lr in this._hrmsleaverecordrepository.Table where lr.LmslrEtedEmployeeId== id select lr).ToList(),
                                     LmsEmployeeLeave = (from el in this._hrmsemployeeLeaveRepository.Table where el.LmselEtedEmployeeId== id select el).ToList(),


                                 }).ToList();
            

            //var employeesData = _hrmsemployeeRepository.Table.Include(x => x.EmsTblAcademicQualification).Include(x => x.EmsTblEmergencyContact).Include(x => x.EmsTblEmployeeProfessionalDetails).Include(x => x.EmsTblProfessionalQualification).Include(x => x.EmsTblWorkingHistory).Include(x => x.EmsTblHrmsUser).Where(x => x.EtedEmployeeId == id).ToList();
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
        
        public BaseResponse CreateDropdownvalue(HrmsDropdownValueVM value)
        {
            BaseResponse response = new BaseResponse();

            HrmsDropdownValue hrmsdropdownvalue = new HrmsDropdownValue();
            if (!string.IsNullOrEmpty(value.HdvValueName))

            {
                hrmsdropdownvalue.HdvHdDropdownId=value.HdvHdDropdownId ;
                hrmsdropdownvalue.HdvValueName = value.HdvValueName;
                hrmsdropdownvalue.HdvCreatedBy = value.HdvCreatedBy;
                hrmsdropdownvalue.HdvCreatedByName = value.HdvCreatedByName;
                hrmsdropdownvalue.HdvCreatedByDate = value.HdvCreatedByDate;
                hrmsdropdownvalue.HdvIsDelete = false;
                _uow.Commit();
                _hrmsdropdownvaluerepository.Insert(hrmsdropdownvalue);
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
        public BaseResponse getDropdownValuesByid(int id)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsdropdownvaluerepository.Table.Where(z => z.HdvIsDelete != true && z.HdvHdDropdownId == id).Count() > 0;
            var dropDownValueData = _hrmsdropdownvaluerepository.Table.Where(y => y.HdvHdDropdownId == id).Select(x => x.HdvValueName).ToList();


            if (count == true)
            {
                response.Data = dropDownValueData;
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

        public BaseResponse getEmployeeUnderTeamLeadByid(int empid)
        {
            BaseResponse response = new BaseResponse();

            bool count = _hrmsemployeeRepository.Table.Where(z => z.EtedIsDelete == false && z.EtedManagerId == empid).Count() > 0;
            var employeeData = _hrmsemployeeRepository.Table.Where(y => y.EtedManagerId ==empid).ToList();


            if (count == true)
            {
                response.Data = employeeData;
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
