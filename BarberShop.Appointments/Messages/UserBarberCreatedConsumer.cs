using System.IdentityModel.Tokens.Jwt;
using BarberShop.Appointments.Models.Users;
using BarberShop.Appointments.Services;
using BarberShop.Messages;
using MassTransit;

namespace BarberShop.Appointments.Messages;

public class UserBarberCreatedConsumer : IConsumer<BarberCreatedMessage>
{
    private readonly IBarberService _barberService;

    public UserBarberCreatedConsumer(IBarberService barberService)
    {
        _barberService = barberService;
    }

    public async Task Consume(ConsumeContext<BarberCreatedMessage> context)
    {
        var jwt = context.Message.Token;
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        var userId = token.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;

        var barber = new NewBarberDto
        {
            Name = context.Message.Name,
            UserId = userId
        };

        await _barberService.CreateUser(barber);
    }
}