﻿using FluentValidation;
using System.Security.Claims;
using UserContacts.Bll.Dtos;
using UserContacts.Bll.Security;
using UserContacts.Bll.SecurityFolder.Safety;
using UserContacts.Core.Errors;
using UserContacts.Dal.Entities;
//using UserContacts.Repository.Services;
using UserContacts.Repository.Settings;

namespace UserContacts.Bll.Services;

public class AuthService(Repository.Services.IUserRoleRepository UserRoleRepository, IValidator<UserCreateDto> Validator, Repository.Services.IUserRepository UserRepository, ITokenService TokenService, JwtAppSettings JwtToken, IValidator<UserCreateDto> ValidatorForLogin, Repository.Services.IRefreshTokenRepository RefreshTokenRepository) : IAuthService
{
    
    private readonly int Expires = int.Parse(JwtToken.Lifetime);

    public async Task<long> SignUpUserAsync(UserCreateDto userCreateDto)
    {
        var validatorResult = await Validator.ValidateAsync(userCreateDto);
        if (!validatorResult.IsValid)
        {
            string errorMessages = string.Join("; ", validatorResult.Errors.Select(e => e.ErrorMessage));
            throw new AuthException(errorMessages);
        }

        var tupleFromHasher = PasswordHasher.Hasher(userCreateDto.Password);
        var user = new Users()
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            UserName = userCreateDto.UserName,
            Email = userCreateDto.Email,
            PhoneNumber = userCreateDto.PhoneNumber,
            Password = tupleFromHasher.Hash,
            Salt = tupleFromHasher.Salt,
        };
        user.RoleId = await UserRoleRepository.GetRoleIdAsync("User");

        return await UserRepository.InsertUserAsync(user);
    }

    public async Task<LoginResponseDto> LoginUserAsync(UserLoginDto userLoginDto)
    {
        var user = await UserRepository.SelectUserByUserNameAsync(userLoginDto.UserName);

        var checkUserPassword = PasswordHasher.Verify(userLoginDto.Password, user.Password, user.Salt);

        if (checkUserPassword == false)
        {
            throw new UnAuthorizedException("UserName or password incorrect");
        }

        var userGetDto = new UserGetDto()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.Name,
        };

        var token = TokenService.GenerateToken(userGetDto);
        var refreshToken = TokenService.GenerateRefreshToken();

        var refreshTokenToDB = new RefreshTokens()
        {
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId
        };

        await RefreshTokenRepository.AddRefreshToken(refreshTokenToDB);

        var loginResponseDto = new LoginResponseDto()
        {
            AccessToken = token,
            RefreshToken = refreshToken,
            TokenType = "Bearer",
            Expires = 24
        };


        return loginResponseDto;
    }


    public async Task<LoginResponseDto> RefreshTokenAsync(RefreshRequestDto request)
    {
        ClaimsPrincipal? principal = TokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null) throw new ForbiddenException("Invalid access token.");


        var userClaim = principal.FindFirst(c => c.Type == "UserId");
        var userId = long.Parse(userClaim.Value);


        var refreshToken = await RefreshTokenRepository.SelectRefreshToken(request.RefreshToken, userId);
        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow || refreshToken.IsRevoked)
            throw new UnAuthorizedException("Invalid or expired refresh token.");

        refreshToken.IsRevoked = true;

        var user = await UserRepository.SelectUserByIdAsync(userId);

        var userGetDto = new UserGetDto()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.Name,
        };

        var newAccessToken = TokenService.GenerateToken(userGetDto);
        var newRefreshToken = TokenService.GenerateRefreshToken();

        var refreshTokenToDB = new RefreshTokens()
        {
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId
        };

        await RefreshTokenRepository.AddRefreshToken(refreshTokenToDB);

        return new LoginResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            TokenType = "Bearer",
            Expires = 24
        };
    }
}
