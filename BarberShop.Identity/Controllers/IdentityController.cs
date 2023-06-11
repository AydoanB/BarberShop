using BarberShop.Identity.Models;
using BarberShop.Identity.Services;
using BarberShop.Messages;
using BarberShop.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Identity.Controllers
{
    [ApiController]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identity;
        private readonly ICurrentUserService _currentUser;
        private readonly IBus _publisher;

        public IdentityController(
            IIdentityService identity,
            ICurrentUserService currentUser, 
            IBus publisher)
        {
            _identity = identity;
            _currentUser = currentUser;
            _publisher = publisher;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult<UserOutputModel>> Register(UserInputModel input)
        {
            var result = await _identity.Register(input);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return await Login(input);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<UserOutputModel>> Login(UserInputModel input)
        {
            var result = await _identity.Login(input);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            //await _publisher.Publish(new BarberCreatedMessage
            //{
            //    Name = input.FullName,
            //    Token = result.Data.Token,
            //});

            return new UserOutputModel(result.Data.Token);
        }

        [HttpPut]
        [Route(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword(ChangePasswordInputModel input)
            => await _identity.ChangePassword(_currentUser.UserId, new ChangePasswordInputModel
            {
                CurrentPassword = input.CurrentPassword,
                NewPassword = input.NewPassword
            });
    }
}
