using BarberShop.Appointments.Models.Users;
using BarberShop.Appointments.Services;
using BarberShop.Messages;
using BarberShop.Services;
using MassTransit;

namespace BarberShop.Appointments.Messages;

public class UserBarberCreatedConsumer : IConsumer<BarberCreatedMessage>
{
    private readonly IBarberService _barberService;
    private readonly ICurrentUserService _currentUserService;

    public UserBarberCreatedConsumer(
        IBarberService barberService, 
        ICurrentUserService currentUserService)
    {
        _barberService = barberService;
        _currentUserService = currentUserService;
    }

    public async Task Consume(ConsumeContext<BarberCreatedMessage> context)
    {
        var currentUserId = _currentUserService.UserId;

        var barber = new NewBarberDto
        {
            Name = context.Message.Name,
            UserId = currentUserId
        };

        await _barberService.CreateUser(barber);
    }
}