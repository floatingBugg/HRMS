﻿using System;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        private readonly IHRMSAcademicRepository _hrmsacademicrepository;
        private readonly IHRMSPRofessionalRepository _hrmsprofessionalrepository;
        private readonly IHRMSEmployeeContactRepository _employeeContactRepository;
        private readonly IHRMSEmployeeWorkingHistoryRepository _workinghistoryRepository;
        private readonly IHRMSProfessionalDetailsRepository _hrmsprofessionaldetailsrepository;

        IConfiguration _config;
        private readonly IUnitOfWork _uow;
        public EmployeeService(IConfiguration config, IHRMSEmployeeRepository hrmsemployeeRepository, IHRMSAcademicRepository hRMSAcademicRepository, IHRMSEmployeeContactRepository employeeContactRepository, IHRMSPRofessionalRepository hRMSProfessionalRepository, IHRMSEmployeeWorkingHistoryRepository workingHistoryRepository, IHRMSProfessionalDetailsRepository hRMSProfessionalDetailsRepository, IUnitOfWork uow)
        {
            _config = config;
            _hrmsemployeeRepository = hrmsemployeeRepository;
            _employeeContactRepository = employeeContactRepository;
            _hrmsacademicrepository = hRMSAcademicRepository;
            _hrmsprofessionalrepository = hRMSProfessionalRepository;
            _workinghistoryRepository = workingHistoryRepository;
            _hrmsprofessionaldetailsrepository = hRMSProfessionalDetailsRepository;
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
            }).ToList().OrderByDescending(x=>x.empID);

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

        public BaseResponse CreateEmployee(EmsTblEmployeeDetailsVM employee,string rootpath)
        {
           
            BaseResponse response = new BaseResponse();
            bool doesEmailExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedEmailAddress == employee.EtedEmailAddress) > 0;
            bool doesCNICExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedCnic == employee.EtedCnic) > 0;
            EmsTblEmployeeDetails emsTblEmployeeDetails = new EmsTblEmployeeDetails();
            //Employee Details
            
            if (!string.IsNullOrEmpty(employee.EtedCreatedBy) && !string.IsNullOrEmpty(employee.EtedCreatedByName)
               && !string.IsNullOrEmpty(employee.EtedOfficialEmailAddress) && !string.IsNullOrEmpty(employee.EtedAddress) && !string.IsNullOrEmpty(employee.EtedGender)
               && !string.IsNullOrEmpty(employee.EtedReligion)
               && (employee.EtedCnic != null) && doesEmailExistAlready == false && doesCNICExistAlready == false)
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
                    var photographByte = Convert.FromBase64String(imageUrl.Count()>1 ?imageUrl[1]:imageUrl[0]);
                    targetPathProfile += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}.png";
                    using (FileStream fs = new FileStream(targetPathProfile, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(photographByte);
                    }
                    employee.EtedPhotograph = targetPathProfile.Replace(RootPath, "").Replace("\\", "/");
                }

                emsTblEmployeeDetails.EtedEmployeeId = employee.EtedEmployeeId;
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

                //Academic
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
                            var imageUrl = empAcad.EtaqUploadDocuments.Split("base64,");
                            var uploadDocumentsByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                            targetPathAcademicQual += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empAcad.EtaqInstituteName}.png";
                            using (FileStream fs = new FileStream(targetPathAcademicQual, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(uploadDocumentsByte);
                            }
                            empAcad.EtaqUploadDocuments = targetPathAcademicQual.Replace(RootPath, "").Replace("\\", "/");
                        }
                    }
                    var _emsTblAcademicQualificationList = employee.EmsTblAcademicQualification.Select(x => new EmsTblAcademicQualification
                    {
                        EtaqEtedEmployeeId = x.EtaqEtedEmployeeId,
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
                    emsTblEmployeeDetails.EmsTblAcademicQualification = _emsTblAcademicQualificationList.ToArray();
                }

                //Professional Qualification
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
                            var imageUrl = empProfQual.EtpqDocuments.Split("base64,");
                            var documentsByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                            targetPathProfQual += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empProfQual.EtpqInstituteName}.png";
                            using (FileStream fs = new FileStream(targetPathProfQual, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(documentsByte);
                            }
                            empProfQual.EtpqDocuments = targetPathProfQual.Replace(RootPath, "").Replace("\\", "/");
                        }
                    }
                    var _emsTblProfessionalQualificationList = employee.EmsTblProfessionalQualification.Select(x => new EmsTblProfessionalQualification
                    {
                        EtpqEtedEmployeeId = x.EtpqEtedEmployeeId,
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
                    emsTblEmployeeDetails.EmsTblProfessionalQualification = _emsTblProfessionalQualificationList.ToArray();
                }

                //Professional Detail
                if (employee.EmsTblEmployeeProfessionalDetails.Count > 0)
                {
                    var _emsTblEmployeeProfessionalDetailsList = employee.EmsTblEmployeeProfessionalDetails.Select(x => new EmsTblEmployeeProfessionalDetails
                    {
                        EtepdEtedEmployeeId = x.EtepdEtedEmployeeId,
                        EtepdDesignation = x.EtepdDesignation,
                        EtepdSalary = x.EtepdSalary,
                        EtepdJoiningDate = x.EtepdJoiningDate,
                        EtepdProbation = x.EtepdProbation,
                        EtepdCreatedBy = x.EtepdCreatedBy,
                        EtepdCreatedByDate = DateTime.Now,
                        EtepdCreatedByName = x.EtepdCreatedByName,
                        EtepdIsDelete = false,
                    });
                    emsTblEmployeeDetails.EmsTblEmployeeProfessionalDetails = _emsTblEmployeeProfessionalDetailsList.ToArray();
                }

                //Emergency Contact
                if (employee.EmsTblEmergencyContact.Count > 0)
                {
                    var _emsTblEmergencyContactList = employee.EmsTblEmergencyContact.Select(x => new EmsTblEmergencyContact
                    {
                        EtecEtedEmployeeId = x.EtecEtedEmployeeId,
                        EtecFirstName = x.EtecFirstName,
                        EtecLastName = x.EtecLastName,
                        EtecRelation = x.EtecRelation,
                        EtecContactNumber = x.EtecContactNumber,
                        EtecAddress=x.EtecAddress,
                        EtecCreatedBy = x.EtecCreatedBy,
                        EtecCreatedByDate = DateTime.Now,
                        EtecCreatedByName = x.EtecCreatedByName,
                        EtecIsDelete = false,
                    });
                    emsTblEmployeeDetails.EmsTblEmergencyContact = _emsTblEmergencyContactList.ToArray();
                }
                //Working History
                if (employee.EmsTblWorkingHistory.Count > 0)
                {
                    foreach(var empWork in employee.EmsTblWorkingHistory)
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

                            var imageUrl = empWork.EtwhExperienceLetter.Split("base64,");
                            var experienceLetterByte = Convert.FromBase64String(imageUrl[1]);
                            targetPathWork += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empWork.EtwhDesignation}.png";
                            using (FileStream fs = new FileStream(targetPathWork, FileMode.Create, FileAccess.Write))
                            {
                                 fs.Write(experienceLetterByte);
                            }
                            empWork.EtwhExperienceLetter = targetPathWork.Replace(RootPath, "").Replace("\\", "/");
                        }
                    }
                    var _emsTblWorkingHistoryList = employee.EmsTblWorkingHistory.Select(x => new EmsTblWorkingHistory
                    {
                        EtwhEtedEmployeeId = x.EtwhEtedEmployeeId,
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
                    emsTblEmployeeDetails.EmsTblWorkingHistory = _emsTblWorkingHistoryList.ToArray();
                }
                    

                _hrmsemployeeRepository.Insert(emsTblEmployeeDetails);
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

        public BaseResponse UpdateEmployee(EmsTblEmployeeDetailsVM employee,string rootpath)
        {

            BaseResponse response = new BaseResponse();
            EmsTblEmployeeDetails emsTblEmployeeDetails = new EmsTblEmployeeDetails();
            EmsTblEmployeeDetails emsTblEmployeeDetailsInsert = new EmsTblEmployeeDetails();
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
                        targetPathProfile += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}.png";
                        using (FileStream fs = new FileStream(targetPathProfile, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(photographByte);
                        }
                        employee.EtedPhotograph = targetPathProfile.Replace(RootPath, "").Replace("\\", "/");
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
                            var imageUrl = empAcad.EtaqUploadDocuments.Split("base64,");
                            var uploadDocumentsByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                            targetPathAcademicQual += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empAcad.EtaqInstituteName}.png";
                            using (FileStream fs = new FileStream(targetPathAcademicQual, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(uploadDocumentsByte);
                            }
                            empAcad.EtaqUploadDocuments = targetPathAcademicQual.Replace(RootPath, "").Replace("\\", "/");
                        }
                    }

                    /*FOR INSERTION*/

                    var _emsTblAcademicQualificationList1 = employee.EmsTblAcademicQualification.Where(z => z.EtaqAqId==0).Select(x => new EmsTblAcademicQualification
                    {
                        EtaqAqId = x.EtaqAqId,
                        EtaqUploadDocuments=x.EtaqUploadDocuments,
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

                        _hrmsacademicrepository.Insert(emsTblEmployeeDetails.EmsTblAcademicQualification);
                    }

                    /*FOR UPDATE*/
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
                    emsTblEmployeeDetails.EmsTblAcademicQualification = _emsTblAcademicQualificationList.ToArray();
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
                            var imageUrl = empProfQual.EtpqDocuments.Split("base64,");
                            var documentsByte = Convert.FromBase64String(imageUrl.Count() > 1 ? imageUrl[1] : imageUrl[0]);
                            targetPathProfQual += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empProfQual.EtpqInstituteName}.png";
                            using (FileStream fs = new FileStream(targetPathProfQual, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(documentsByte);
                            }
                            empProfQual.EtpqDocuments = targetPathProfQual.Replace(RootPath, "").Replace("\\", "/");
                        }                      
                    }
                    var _emsTblProfessionalQualificationList = employee.EmsTblProfessionalQualification.Where(z => z.EtpqEtedEmployeeId == employee.EtedEmployeeId).Select(x => new EmsTblProfessionalQualification
                    {
                        EtpqPqId = x.EtpqPqId,
                        EtpqDocuments=x.EtpqDocuments,
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
                    emsTblEmployeeDetails.EmsTblProfessionalQualification = _emsTblProfessionalQualificationList.ToArray();
                }


                if (employee.EmsTblEmployeeProfessionalDetails.Count > 0)
                {
                    var _emsTblEmployeeProfessionalDetailsList = employee.EmsTblEmployeeProfessionalDetails.Where(z => z.EtepdEtedEmployeeId == employee.EtedEmployeeId).Select(x => new EmsTblEmployeeProfessionalDetails
                    {
                        EtepdPdId=x.EtepdPdId,
                        EtepdEtedEmployeeId=employee.EtedEmployeeId,
                        EtepdDesignation = x.EtepdDesignation,
                        EtepdSalary = x.EtepdSalary,
                        EtepdJoiningDate = x.EtepdJoiningDate,
                        EtepdProbation = x.EtepdProbation,
                        EtepdCreatedBy = x.EtepdCreatedBy,
                        EtepdCreatedByDate = DateTime.Now,
                        EtepdCreatedByName = x.EtepdCreatedByName,
                        EtepdIsDelete = false,
                    });
                    emsTblEmployeeDetails.EmsTblEmployeeProfessionalDetails = _emsTblEmployeeProfessionalDetailsList.ToArray();
                }

                if (employee.EmsTblEmergencyContact.Count > 0)
                {
                    var _emsTblEmergencyContactList1 = employee.EmsTblEmergencyContact.Where(z => z.EtecEcId == 0).Select(x => new EmsTblEmergencyContact
                    {
                        EtecEcId=x.EtecEcId,
                        EtecEtedEmployeeId=employee.EtedEmployeeId,
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

                        _employeeContactRepository.Insert(emsTblEmployeeDetails.EmsTblEmergencyContact);
                    }
                    var _emsTblEmergencyContactList = employee.EmsTblEmergencyContact.Where(z => z.EtecEcId > 0).Select(x => new EmsTblEmergencyContact
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
                    emsTblEmployeeDetails.EmsTblEmergencyContact = _emsTblEmergencyContactList.ToArray();
                }

                if (employee.EmsTblWorkingHistory.Count > 0)
                {
                    foreach (var empWork in employee.EmsTblWorkingHistory)
                    {
                        empWork.EtwhEtedEmployeeId = employee.EtedEmployeeId;
                        if (!string.IsNullOrEmpty(empWork.EtwhExperienceLetter))
                        {
                            var RootPath = rootpath;
                            string FilePathWork = "Images\\EmployeeID\\WorkingHistory\\";
                            var targetPathWork = Path.Combine(RootPath, FilePathWork);
                            if (!Directory.Exists(targetPathWork))
                            {
                                Directory.CreateDirectory(targetPathWork);
                            }
                            var imageUrl = empWork.EtwhExperienceLetter.Split("base64,");
                            var experienceLetterByte = Convert.FromBase64String(imageUrl[1]);
                            targetPathWork += $"{employee.EtedFirstName}-{employee.EtedLastName}_{employee.EtedEmployeeId}_{empWork.EtwhDesignation}.png";
                            using (FileStream fs = new FileStream(targetPathWork, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(experienceLetterByte);
                            }
                            empWork.EtwhExperienceLetter = targetPathWork.Replace(RootPath, "").Replace("\\", "/");
                        }
                    }
                    var _emsTblWorkingHistoryList1 = employee.EmsTblWorkingHistory.Where(z => z.EtwhWhId > 0).Select(x => new EmsTblWorkingHistory
                    {
                        EtwhEtedEmployeeId = employee.EtedEmployeeId,
                        EtwhWhId = x.EtwhWhId,
                        EtwhCompanyName = x.EtwhCompanyName,
                        EtwhDesignation = x.EtwhDesignation,
                        EtwhExperienceLetter = x.EtwhExperienceLetter,
                        EtwhStratDate = x.EtwhStratDate,
                        EtwhEndDate = x.EtwhEndDate,
                        EtwhDuration = x.EtwhDuration,
                        EtwhCreatedBy = x.EtwhCreatedBy,
                        EtwhCreatedByDate = DateTime.Now,
                        EtwhCreatedByName = x.EtwhCreatedByName,
                        EtwhIsDelete = false,
                    });

                    emsTblEmployeeDetails.EmsTblWorkingHistory = _emsTblWorkingHistoryList1.ToArray();

                    var _emsTblWorkingHistoryList = employee.EmsTblWorkingHistory.Where(z => z.EtwhWhId == 0).Select(x => new EmsTblWorkingHistory
                    {
                        EtwhEtedEmployeeId = employee.EtedEmployeeId,
                        EtwhCompanyName = x.EtwhCompanyName,
                        EtwhDesignation = x.EtwhDesignation,
                        EtwhExperienceLetter = x.EtwhExperienceLetter,
                        EtwhStratDate = x.EtwhStratDate,
                        EtwhEndDate = x.EtwhEndDate,
                        EtwhDuration = x.EtwhDuration,
                        EtwhCreatedBy = x.EtwhCreatedBy,
                        EtwhCreatedByDate = DateTime.Now,
                        EtwhCreatedByName = x.EtwhCreatedByName,
                        EtwhIsDelete = false,
                    });
                    if (_emsTblWorkingHistoryList.Count() > 0) {
                        emsTblEmployeeDetailsInsert.EmsTblWorkingHistory = _emsTblWorkingHistoryList.ToArray();
                        _workinghistoryRepository.Insert(emsTblEmployeeDetailsInsert.EmsTblWorkingHistory);
                    }
                   
                }

                _hrmsemployeeRepository.Update(emsTblEmployeeDetails);
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

        public BaseResponse DeleteEmployee(int empid)
        {
            BaseResponse response = new BaseResponse();
            bool doesExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedEmployeeId == empid) > 0;
            bool isDeletedAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedIsDelete == true && p.EtedEmployeeId== empid) > 0;
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
            
            return response;
        }

        public BaseResponse ViewDataEmployeeByid(int id)
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
