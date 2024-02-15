using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using MyMechanic.Entities.Models;
using MyMechanic.Repositories.Interface;
using MyMechanic.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Repositories.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly ICommonRepo<User> _userRepo;
        private readonly IConfiguration _configuration;

        public AuthRepo(ICommonRepo<User> userRepo,IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public RegistrationModel RegisterNewUser(RegistrationModel model)
        {
            if(_userRepo.ExistUser(u => u.Email== model.Email))
            {
                model.Email = "Email is already Used";
                return model;
            }
            if(_userRepo.ExistUser(u => u.MobileNumber== model.MobileNumber))
            {
                model.Email = "Mobile Number is already Used";
                return model;
            }

            User newUser = new User()
            {
                FirstName= model.FirstName,
                LastName= model.LastName,
                Email= model.Email,
                Role= model.Role,
                MobileNumber= model.MobileNumber,
                Password= model.Password,
                //CreatedAt = DateTime.Now
            };
            _userRepo.AddNew(newUser);
            _userRepo.Save();
            return model;
        }
        public UserInfoModel VarifyUser(LoginModel loginModel)
        {
            if(_userRepo.ExistUser(u => u.Email == loginModel.Email && u.Password == loginModel.Password && u.DeletedAt == null))
            {
                User user = _userRepo.GetFirstOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password && u.DeletedAt == null);

                UserInfoModel userInfoModel = new UserInfoModel()
                {
                    Id = user.Id,
                    Email = loginModel.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role= user.Role,
                };
                userInfoModel = GenerateToken(userInfoModel);
                return userInfoModel;
            }
            return new UserInfoModel();
        }
        public UserInfoModel GenerateToken(UserInfoModel data)
        {
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var secretKey = _configuration["JwtConfig:SecretKey"];

            // Create a list of claims for the user
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, data.Email),
            new Claim(ClaimTypes.Role, data.Role),
            // Add additional claims as needed
        };

            // Generate a symmetric security key from the secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

            // Create signing credentials using the key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.Date, // Set the token expiration time
                signingCredentials: creds
            );

            // Serialize the token to a string
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
    
            data.Token = tokenString;
            return data;
        }
    }
}
