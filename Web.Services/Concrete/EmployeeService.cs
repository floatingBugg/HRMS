using System;
using System.Collections.Generic;
using System.IO;
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

        public BaseResponse CreateEmployee(EmployeeCredential employee,string rootpath)
        {
            
            BaseResponse response = new BaseResponse();
            bool doesEmailExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedEmailAddress == employee.personalemail) > 0;
            bool doesCNICExistAlready = _hrmsemployeeRepository.Table.Count(p => p.EtedCnic == employee.cnic) > 0;
                //Employee Details
                if (!string.IsNullOrEmpty(employee.imageUrlPhoto))
            {
                var RootPath = rootpath;
                string FilePathPhoto = "Images\\EmployeeID\\PhotoGraph\\";
                var targetPathProfile = Path.Combine(RootPath, FilePathPhoto);

                if (!Directory.Exists(targetPathProfile))
                {
                    Directory.CreateDirectory(targetPathProfile);
                }
                employee.photograph = Convert.FromBase64String(employee.imageUrlPhoto.Replace("data:image/jpeg;base64,", ""));
                targetPathProfile += $"{employee.firstname}-{employee.Lastname}_{employee.empID}.png";
                using (FileStream fs = new FileStream(targetPathProfile, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(employee.photograph);
                }
                    employee.imageUrlPhoto = targetPathProfile.Replace(RootPath, "").Replace("\\", "/"); 
                }
                //Work Exp Letter
                if (!string.IsNullOrEmpty(employee.expletter))
                {
                var RootPath = rootpath;
                string FilePathWork = "Images\\EmployeeID\\WorkingHistory\\";
                var targetPathWork = Path.Combine(RootPath, FilePathWork);
                if (!Directory.Exists(targetPathWork))
                {
                    Directory.CreateDirectory(targetPathWork);
                }
                employee.workexpletter = Convert.FromBase64String(employee.expletter.Replace("data:image/jpeg;base64,", ""));
                targetPathWork += $"{employee.firstname}-{employee.Lastname}_{employee.empID}_{employee.workdesignation}.png";
                using (FileStream fs = new FileStream(targetPathWork, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(employee.workexpletter);
                }
                employee.expletter = targetPathWork.Replace(RootPath, "").Replace("\\", "/"); 
                }
                //Professional Qualification
                if (!string.IsNullOrEmpty(employee.Documents))
                {
                var RootPath = rootpath;
                string FilePathProf = "Images\\EmployeeID\\ProfessionalQualification\\";
                var targetPathProfQual = Path.Combine(RootPath, FilePathProf);
                if (!Directory.Exists(targetPathProfQual))
                {
                    Directory.CreateDirectory(targetPathProfQual);
                }
                employee.DocumentsProfQual = Convert.FromBase64String(employee.Documents.Replace("data:image/jpeg;base64,", ""));
                targetPathProfQual += $"{employee.firstname}-{employee.Lastname}_{employee.empID}_{employee.certification}.png";
                using (FileStream fs = new FileStream(targetPathProfQual, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(employee.DocumentsProfQual);
                }
                employee.expletter = targetPathProfQual.Replace(RootPath, "").Replace("\\", "/");
                }
                //Academic
                if (!string.IsNullOrEmpty(employee.UploadDocuments))
                {
                var RootPath = rootpath;
                string FilePathAcad = "Images\\EmployeeID\\AcademicQualification\\";
                var targetPathAcademicQual = Path.Combine(RootPath, FilePathAcad);
                if (!Directory.Exists(targetPathAcademicQual))
                {
                    Directory.CreateDirectory(targetPathAcademicQual);
                }
                employee.UploadDocumentAcad = Convert.FromBase64String(employee.UploadDocuments.Replace("data:image/jpeg;base64,", ""));
                targetPathAcademicQual += $"{employee.firstname}-{employee.Lastname}_{employee.empID}_{employee.AcademicInstituteName}.png";
                using (FileStream fs = new FileStream(targetPathAcademicQual, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(employee.UploadDocumentAcad);
                }
                employee.UploadDocuments = targetPathAcademicQual.Replace(RootPath, "").Replace("\\", "/");
                }

            if (!string.IsNullOrEmpty(employee.created) && !string.IsNullOrEmpty(employee.createdName)
               && !string.IsNullOrEmpty(employee.officialemail) && !string.IsNullOrEmpty(employee.address) && !string.IsNullOrEmpty(employee.gender) 
               && !string.IsNullOrEmpty(employee.religion)
               && (employee.cnic!=null) && doesEmailExistAlready == false && doesCNICExistAlready== false)
            {
                List<EmsTblEmployeeDetails> EmpDetails = new List<EmsTblEmployeeDetails>();
                List<EmsTblAcademicQualification> Empacademic = new List<EmsTblAcademicQualification>();
                List<EmsTblProfessionalQualification> EmpPro = new List<EmsTblProfessionalQualification>();
                List<EmsTblEmergencyContact> EmpEmerg = new List<EmsTblEmergencyContact>();
                List<EmsTblWorkingHistory> EmpWorking = new List<EmsTblWorkingHistory>();
                List<EmsTblEmployeeProfessionalDetails> EmpProDetails = new List<EmsTblEmployeeProfessionalDetails>();



                EmpDetails.Add(new EmsTblEmployeeDetails
                {

                    EtedFirstName = employee.firstname,
                    EtedLastName = employee.Lastname,
                    EtedEmailAddress = employee.personalemail,
                    EtedCnic = employee.cnic,
                    EtedDob = employee.dob,
                    EtedGender = employee.gender,
                    EtedAddress = employee.address,
                    EtedMaritalStatus = employee.martialstatus,
                    EtedBloodGroup = employee.bloodgroup,
                    EtedContactNumber = employee.contact,
                    EtedNationality = employee.nationality,
                    EtedReligion = employee.religion,
                    EtedStatus = employee.empstatus,
                    EtedPhotograph=employee.imageUrlPhoto,
                    EtedOfficialEmailAddress = employee.officialemail,
                    EtedCreatedBy = employee.created,
                    EtedCreatedByDate = DateTime.Now,
                    EtedCreatedByName = employee.createdName,
                    EtedIsDelete = false,
                });
                _hrmsemployeeRepository.Insert(EmpDetails);


                //Create Method academic Qualification

                Empacademic.Add(new EmsTblAcademicQualification
                {
                    EtaqEtedEmployeeId = EmpDetails.FirstOrDefault().EtedEmployeeId,
                    EtaqQualification = employee.Qualification,
                    EtaqPassingYear = employee.PassingYear,
                    EtaqCgpa = employee.Cgpa,
                    EtaqInstituteName = employee.AcademicInstituteName,
                    EtaqUploadDocuments = "Hello",
                    EtaqCreatedBy = employee.created,
                    EtaqCreatedByDate = DateTime.Now,
                    EtaqCreatedByName = employee.createdName,
                    EtaqIsDelete = false,
                });
                _hrmsacademicrepository.Insert(Empacademic);

                //Create Method Professional Qualification

                EmpPro.Add(new EmsTblProfessionalQualification
                {
                    EtpqEtedEmployeeId = EmpDetails.FirstOrDefault().EtedEmployeeId,
                    EtpqCertification = employee.certification,
                    EtpqStratDate = DateTime.Now,
                    EtpqEndDate = DateTime.Now,
                    EtpqDocuments = "Testing Here",
                    EtpqInstituteName = employee.ProfessionalInstituteName,
                    EtpqCreatedBy = employee.created,
                    EtpqCreatedByName = employee.createdName,
                    EtpqCreatedByDate = DateTime.Now,
                    EtpqIsDelete = false,
                });
                _hrmsprofessionalrepository.Insert(EmpPro);


                EmpEmerg.Add(new EmsTblEmergencyContact
                {
                    EtecFirstName = employee.emergencyfirstname,
                    EtecLastName = employee.emergencylastname,
                    EtecRelation = employee.emergencyrelation,
                    EtecContactNumber = employee.emergencycontact,
                    EtecAddress = employee.emergencyaddress,
                    EtecEtedEmployeeId = EmpDetails.FirstOrDefault().EtedEmployeeId,
                    EtecCreatedBy = employee.created,
                    EtecCreatedByName = employee.createdName,
                    EtecCreatedByDate = DateTime.Now,
                    EtecIsDelete = false,

                });
                _employeeContactRepository.Insert(EmpEmerg);
                //working History
                EmpWorking.Add(new EmsTblWorkingHistory
                {
                    EtwhEtedEmployeeId = EmpDetails.FirstOrDefault().EtedEmployeeId,
                    EtwhCompanyName = employee.companyname,
                    EtwhStratDate = employee.workstartdate,
                    EtwhEndDate = employee.workenddate,
                    EtwhDuration = employee.duration,
                    EtwhDesignation = employee.workdesignation,
                    EtwhExperienceLetter = "None",
                    EtwhCreatedBy = employee.created,
                    EtwhCreatedByName = employee.createdName,
                    EtwhCreatedByDate = DateTime.Now,
                    EtwhIsDelete = false,
                });
                _workinghistoryRepository.Insert(EmpWorking);

                //Create Method Professional details

                EmpProDetails.Add(new EmsTblEmployeeProfessionalDetails
                {
                    EtepdEtedEmployeeId = EmpDetails.FirstOrDefault().EtedEmployeeId,
                    EtepdDesignation = employee.profdesignation,
                    EtepdSalary = employee.Salary,
                    EtepdJoiningDate = employee.JoiningDate,
                    EtepdProbation = employee.Probation,
                    EtepdCreatedBy = employee.created,
                    EtepdCreatedByName = employee.createdName,
                    EtepdCreatedByDate = DateTime.Now,
                    EtepdIsDelete = false,

                });
                _hrmsprofessionaldetailsrepository.Insert(EmpProDetails);

                _uow.Commit();
           
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

        public BaseResponse UpdateEmployee(EmployeeCredential employee)
        {

            BaseResponse response = new BaseResponse();

            bool count = _hrmsemployeeRepository.Table.Where(p => p.EtedEmployeeId == employee.empID).Count() > 0;
            if (count == true)
            {
                _hrmsemployeeRepository.Table.Where(p => p.EtedEmployeeId == employee.empID)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.EtedFirstName = employee.firstname;
                        x.EtedLastName = employee.Lastname;
                        x.EtedEmailAddress = employee.personalemail;
                        x.EtedDob = employee.dob;
                        x.EtedContactNumber = employee.contact;
                        x.EtedAddress = employee.address;
                        x.EtedGender = employee.gender;
                        x.EtedMaritalStatus = employee.martialstatus;
                        x.EtedBloodGroup = employee.bloodgroup;
                        x.EtedPhotograph = "test";
                        x.EtedCnic = employee.cnic;
                        x.EtedOfficialEmailAddress = employee.officialemail;
                        x.EtedReligion = employee.religion;
                        x.EtedNationality = employee.nationality;
                        x.EtedStatus = employee.empstatus;
                        x.EtedIsDelete = false;
                        x.EtedModifiedBy = employee.modified;
                        x.EtedModifiedByName = employee.modifiedName;
                        x.EtedModifiedByDate = DateTime.Now;


                    });
                _hrmsacademicrepository.Table.Where(p => p.EtaqEtedEmployeeId == employee.empID)
                    .ToList()
                    .ForEach(x =>
                    {

                        x.EtaqQualification = employee.Qualification;
                        x.EtaqPassingYear = employee.PassingYear;
                        x.EtaqCgpa = employee.Cgpa;
                        x.EtaqInstituteName = employee.AcademicInstituteName;
                        x.EtaqUploadDocuments = "Hello";
                        x.EtaqModifiedBy = employee.modified;
                        x.EtaqModifiedByDate = DateTime.Now;
                        x.EtaqModifiedByName = employee.modifiedName;
                        x.EtaqIsDelete = false;


                    });
                _hrmsprofessionalrepository.Table.Where(p => p.EtpqEtedEmployeeId == employee.empID)
                    .ToList()
                    .ForEach(x =>
                    {


                        x.EtpqCertification = employee.certification;
                        x.EtpqStratDate = employee.profstartDate;
                        x.EtpqEndDate = employee.profendDate;
                        x.EtpqDocuments = "Testing Here";
                        x.EtpqInstituteName = employee.ProfessionalInstituteName;
                        x.EtpqModifiedBy = employee.modified;
                        x.EtpqModifiedByName = employee.modifiedName;
                        x.EtpqModifiedByDate = DateTime.Now;
                        x.EtpqIsDelete = false;


                    });

                _employeeContactRepository.Table.Where(p => p.EtecEtedEmployeeId == employee.empID)
                   .ToList()
                   .ForEach(x =>
                   {


                       x.EtecFirstName = employee.emergencyfirstname;
                       x.EtecLastName = employee.emergencylastname;
                       x.EtecRelation = employee.emergencyrelation;
                       x.EtecContactNumber = employee.emergencycontact;
                       x.EtecAddress = employee.emergencyaddress;
                       x.EtecModifiedBy = employee.modified;
                       x.EtecModifiedByName = employee.modifiedName;
                       x.EtecModifiedByDate = DateTime.Now;
                       x.EtecIsDelete = false;


                   });
                _workinghistoryRepository.Table.Where(p => p.EtwhEtedEmployeeId == employee.empID)
                   .ToList()
                   .ForEach(x =>
                   {



                       x.EtwhCompanyName = employee.companyname;
                       x.EtwhStratDate = employee.workstartdate;
                       x.EtwhEndDate = employee.workenddate;
                       x.EtwhDuration = employee.duration;
                       x.EtwhDesignation = employee.workdesignation;
                       x.EtwhExperienceLetter = "None";
                       x.EtwhModifiedBy = employee.modified;
                       x.EtwhModifiedByName = employee.modifiedName;
                       x.EtwhModifiedByDate = DateTime.Now;
                       x.EtwhIsDelete = false;


                   });

                _hrmsprofessionaldetailsrepository.Table.Where(p => p.EtepdEtedEmployeeId == employee.empID)
                   .ToList()
                   .ForEach(x =>
                   { 
                       x.EtepdDesignation = employee.profdesignation;
                       x.EtepdSalary = employee.Salary;
                       x.EtepdJoiningDate = employee.JoiningDate;
                       x.EtepdProbation = employee.Probation;
                       x.EtepdModifiedBy = employee.modified;
                       x.EtepdModifiedByName = employee.modifiedName;
                       x.EtepdModifiedByDate = DateTime.Now;
                       x.EtepdIsDelete = false;


                   });

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
