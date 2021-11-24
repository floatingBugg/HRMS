﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Web.Data.Interfaces;
using Web.DLL.Models;
using Web.Model.Common;
using Web.Services.Interfaces;

namespace Web.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHRMSEmployeeRepository _hrmsemployeeRepository;
        IConfiguration _config;

        public EmployeeService(IConfiguration config, IHRMSEmployeeRepository hrmsemployeeRepository)
        {
            _config = config;
            _hrmsemployeeRepository = hrmsemployeeRepository;
        }

        public string CreateEmployee(EmployeeCredential employee)
        {
            if (!string.IsNullOrEmpty(employee.firstname) && !string.IsNullOrEmpty(employee.Lastname)
               && !string.IsNullOrEmpty(employee.personalemail) && !string.IsNullOrEmpty(employee.address) && !string.IsNullOrEmpty(employee.gender))
            {
                List<EmsTblEmployeeDetails> obj = new List<EmsTblEmployeeDetails>();
                obj.Add(new EmsTblEmployeeDetails
                {
                    EtedFirstName = employee.firstname,
                    EtedLastName = employee.Lastname,
                    EtedEmailAddress = employee.personalemail,
                    EtedCnic= employee.cnic,
                    EtedDob = DateTime.Now,
                    EtedGender = employee.gender,
                    EtedAddress = employee.address,
                    EtedMaritalStatus=employee.martialstatus,
                    EtedBloodGroup=employee.bloodgroup,
                    EtedContactNumber=employee.contact,
                    EtedNationality=employee.nationality,
                    EtedReligion=employee.religion,
                    EtedStatus=employee.empstatus,
                    EtedPhotograph = new byte[2],
                    EtedOfficialEmailAddress=employee.officialemail,
                    EtedCreatedBy = "test",
                    EtedCreatedByDate = DateTime.Now,
                    EtedCreatedByName = "test",
                    EtedModifiedBy = "test",
                    EtedModifiedByDate = DateTime.Now,
                    EtedModifiedByName = "test",
                    EtedIsDelete = "no",
                    

                });
                _hrmsemployeeRepository.Insert(obj);
            }
            return null;
        }

        public string GetAllEmployee()
        {
            throw new NotImplementedException();
        }

      
        public string UpdateEmployee(EmployeeCredential employee)
        {
            try
            {
                var DataList = _person.GetAll().Where(x => x.IsDeleted != true).ToList();
                foreach (var item in DataList)
                {
                    _person.Update(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }


    }
}
