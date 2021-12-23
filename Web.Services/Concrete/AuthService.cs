using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Web.Data.Interfaces;
using Web.Data.Models;
using Web.Data;
using Web.Model;
using Web.Model.Common;
using Web.Services.Interfaces;
using System.Security.Cryptography;

namespace Web.Services.Concrete
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


        public BaseResponse Authentication(UserCredential login)
        {
            BaseResponse response = new BaseResponse();
            if (!string.IsNullOrEmpty(login.email) && !string.IsNullOrEmpty(login.password))
            {
                var result = _hrmsUserAuthRepository.Table.Where(x => x.EthuEmailAddress == login.email && x.EthuPassword == login.password).FirstOrDefault();
                if (result != null)
                {
                    response.Data = GenerateJSONWebToken(result.EthuFullName,result.EthuUserId);
                    response.Success = true;
                    response.Message = UserMessages.strUserfound;
                }
                else
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = UserMessages.strUsernotfound;
                }
            }
            return response;
        }

        public object GenerateJSONWebToken(string Username,int Userid)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
           /* var refreshtoken = GenerateRefreshToken();
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, userInfo.email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };*/
            var tokenExpiryTime = DateTime.Now.AddMinutes(120);
            /*var refreshtokenExpiryTime = DateTime.Now.AddDays(7);*/
            var userid = Userid;
            var username = Username;
            var token = new JwtSecurityToken(_config["Jwt:ValidIssuer"],
              _config["Jwt:ValidIssuer"],
              /*claims,*/
              expires: tokenExpiryTime,
         
              signingCredentials: credentials);

            return new
            {
                /*refreshtoken,*/
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = tokenExpiryTime,
                userId = userid,
                userName = username
            };
        }
      
        public string Register(RegisterCredential register)
        {
            // first validate the username and password from the db and then generate token
            if (!string.IsNullOrEmpty(register.username) && !string.IsNullOrEmpty(register.password)
                && !string.IsNullOrEmpty(register.email) && !string.IsNullOrEmpty(register.phone) && !string.IsNullOrEmpty(register.gender))
            {
                List<EmsTblHrmsUser> user = new List<EmsTblHrmsUser>();

                user.Add(new EmsTblHrmsUser
                {
                    EthuFullName = "test",
                    EthuUserName = register.username,
                    EthuPassword = register.password,
                    EthuEmailAddress = register.email,
                    EthuPhoneNumber = register.phone,
                    EthuGender = register.gender,
                    EthuCreatedBy = "test",
                    EthuCreatedByDate = DateTime.Now,
                    EthuCreatedByName = "test",
                    EthuModifiedBy = "test",
                    EthuModifiedByDate = DateTime.Now,
                    EthuModifiedByName = "test",
                    EthuIsDelete = false

                });

                _hrmsUserAuthRepository.Insert(user);
                //unitorWork.Commit();

            }
            return null;
        }


        public BaseResponse RefreshAuthentication()
        {
            BaseResponse refreshresponse = new BaseResponse();
            {


                refreshresponse.Data = GenerateRefreshToken();
                refreshresponse.Success = true;
                refreshresponse.Message = UserMessages.strRefreshtoken;

            }
            return refreshresponse;
        }

        public object GenerateRefreshToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var refreshtokenExpiryTime = DateTime.Now.AddHours(5);

            var token = new JwtSecurityToken(_config["Jwt:ValidIssuer"],
             _config["Jwt:ValidIssuer"],

             expires: refreshtokenExpiryTime,

              signingCredentials: credentials);

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = refreshtokenExpiryTime
            };
        }










    }
}
