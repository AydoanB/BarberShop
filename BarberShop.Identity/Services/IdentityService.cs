using BarberShop.Identity.Data.Models;
using BarberShop.Identity.Models;
using BarberShop.Messages;
using BarberShop.Services;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Identity.Services;

public class IdentityService : IIdentityService
{
    private const string InvalidErrorMessage = "Invalid credentials.";

    private readonly UserManager<User> _userManager;
    private readonly ITokenGeneratorService _jwtTokenGenerator;
    private readonly IBus _publisher;

    public IdentityService(
        UserManager<User> userManager,
        ITokenGeneratorService jwtTokenGenerator,
        IBus publisher)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _publisher = publisher;
    }

    public async Task<Result<User>> Register(UserInputModel userInput)
    {
        var user = new User
        {
            Email = userInput.Email,
            UserName = userInput.Email
        };

        var identityResult = await _userManager.CreateAsync(user, userInput.Password);

        var errors = identityResult.Errors.Select(e => e.Description);

        if (!errors.Any() && userInput.IsBarber)
        {
            await _publisher.Publish(new BarberCreatedMessage()
            {
                Name = userInput.FullName
            });
        }

        return identityResult.Succeeded
            ? Result<User>.SuccessWith(user)
            : Result<User>.Failure(errors);
    }

    public async Task<Result<UserOutputModel>> Login(UserInputModel userInput)
    {
        var user = await _userManager.FindByEmailAsync(userInput.Email);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, userInput.Password);
        if (!passwordValid)
        {
            return InvalidErrorMessage;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new UserOutputModel(token);
    }

    public async Task<Result> ChangePassword(
        string userId,
        ChangePasswordInputModel changePasswordInput)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return InvalidErrorMessage;
        }

        var identityResult = await _userManager.ChangePasswordAsync(
            user,
            changePasswordInput.CurrentPassword,
            changePasswordInput.NewPassword);

        var errors = identityResult.Errors.Select(e => e.Description);

        return identityResult.Succeeded
            ? Result.Success
            : Result.Failure(errors);
    }
}