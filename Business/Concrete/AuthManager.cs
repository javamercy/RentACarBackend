﻿using Business.Abstract;
using Business.Constants.Messages;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash,
                passwordSalt;
            HashingHelper.CreatePasswordHash(
                userForRegisterDto.Password,
                out passwordHash,
                out passwordSalt
            );
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, BusinessMessages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByEmail(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(BusinessMessages.EmailNotFound);
            }

            if (
                !HashingHelper.VerifyPasswordHash(
                    userForLoginDto.Password,
                    userToCheck.PasswordHash,
                    userToCheck.PasswordSalt
                )
            )
            {
                return new ErrorDataResult<User>(BusinessMessages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck);
        }

        public IResult UserExists(string email)
        {
            var result = _userService.GetByEmail(email).Data;

            if (result != null)
            {
                return new ErrorResult(BusinessMessages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);

            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = _userService.GetByEmail(changePasswordModel.Email).Data;

            if (user == null)
            {
                return new ErrorResult(BusinessMessages.EmailNotFound);
            }

            var result = HashingHelper.VerifyPasswordHash(changePasswordModel.OldPassword, user.PasswordHash, user.PasswordSalt);

            if (!result)
            {
                return new ErrorResult(BusinessMessages.OldPasswordError);
            }

            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreatePasswordHash(changePasswordModel.NewPassword, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userService.Update(user);
            return new SuccessResult(BusinessMessages.PasswordUpdated);

        }
    }
}
