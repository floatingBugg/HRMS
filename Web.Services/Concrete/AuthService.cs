using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Web.Data.Interfaces;
using Web.DLL;
using Web.DLL.Models;
using Web.Model.Common;

namespace Web.Services
{
    public class AuthService : IJwtAuthService
    {
        private readonly IHRMSUserAuthRepository _hrmsUserAuthRepository;
        IConfiguration _config;
        private readonly UnitOfWork unitorWork;
        public AuthService(IConfiguration config, IHRMSUserAuthRepository hrmsUserAuthRepository)
        {
           _config = config;
            _hrmsUserAuthRepository = hrmsUserAuthRepository;
        }


        public string Authentication(UserCredential login)
        {
          // first validate the username and password from the db and then generate token
          if(!string.IsNullOrEmpty(login.username) && !string.IsNullOrEmpty(login.password))
            {
                var result = _hrmsUserAuthRepository.Table.Where(x => x.EthuUserName == login.username && x.EthuPassword == login.password).FirstOrDefault();
                if(result != null)
                {
                    return GenerateJSONWebToken(login);
                }
            }
            return null;  
        }

        private string GenerateJSONWebToken(UserCredential userInfo)
        {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userInfo.username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var token = new JwtSecurityToken(_config["Jwt:ValidIssuer"],
                  _config["Jwt:ValidIssuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string Register(RegisterCredential register)
        {
            // first validate the username and password from the db and then generate token
            if (!string.IsNullOrEmpty(register.username) && !string.IsNullOrEmpty(register.password)
                && !string.IsNullOrEmpty(register.email) && !string.IsNullOrEmpty(register.phone) && !string.IsNullOrEmpty(register.gender))
            {

                List<EmsTblHrmsUser> obj = new List<EmsTblHrmsUser>();

                obj.Add(new EmsTblHrmsUser
                {
                    EthuFullName = "test",
                    EthuUserName = register.username,
                    EthuPassword = register.password,
                    EthuEmailAddress = register.email,
                    EthuPhoneNumber = register.phone,
                    EthuGender = register.gender,
                    EthuCreatedBy = "test",
                    EthuCreatedByDate = DateTime.Now,
                    EthuCreatedByName= "test",
                    EthuModifiedBy = "test",
                    EthuModifiedByDate = DateTime.Now,
                    EthuModifiedByName = "test",
                    EthuIsDelete = "no"

                });

                _hrmsUserAuthRepository.Insert(obj);
                //unitorWork.Commit();

            }
            return null;
        }
    }
}
