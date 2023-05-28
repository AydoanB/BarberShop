using BarberShop.Identity.Data.Models;
using BarberShop.Identity.Models;
using BarberShop.Services;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Identity.Services;

public class IdentityService : IIdentityService
{
    private const string InvalidErrorMessage = "Invalid credentials.";

    private readonly UserManager<User> _userManager;
    private readonly ITokenGeneratorService _jwtTokenGenerator;

    public IdentityService(UserManager<User> userManager, ITokenGeneratorService jwtTokenGenerator)
    {
        this._userManager = userManager;
        this._jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<User>> Register(UserInputModel userInput)
    {
        var user = new User
        {
            Email = userInput.Email,
            UserName = userInput.Email
        };

        var identityResult = await this._userManager.CreateAsync(user, userInput.Password);

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result<User>.SuccessWith(user)
            : Result<User>.Failure(errors);
    }

    public async Task<Result<UserOutputModel>> Login(UserInputModel userInput)
    {
        var user = await this._userManager.FindByEmailAsync(userInput.Email);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var passwordValid = await this._userManager.CheckPasswordAsync(user, userInput.Password);
        if (!passwordValid)
        {
            return InvalidErrorMessage;
        }

        var token = this._jwtTokenGenerator.GenerateToken(user);

        return new UserOutputModel(token);
    }

    public async Task<Result> ChangePassword(
        string userId,
        ChangePasswordInputModel changePasswordInput)
    {
        var user = await this._userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var identityResult = await this._userManager.ChangePasswordAsync(
            user,
            changePasswordInput.CurrentPassword,
            changePasswordInput.NewPassword);

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(errors);
    }
}
