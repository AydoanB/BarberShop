using BarberShop.Appointments.Data;
using BarberShop.Appointments.Models.Appointment;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BarberShop.Appointments.Services;

public class AppointmentService : IAppointmentService
{
    private readonly MongoDbContext<Appointment> _context;
    private readonly ILogger<AppointmentService> _logger;
    private readonly IClientService _clientService;
    private readonly IBarberService _barberService;

    public AppointmentService(
        MongoDbContext<Appointment> context, 
        ILogger<AppointmentService> logger, 
        IClientService clientService, 
        IBarberService barberService)
    {
        _context = context;
        _logger = logger;
        _clientService = clientService;
        _barberService = barberService;
    }

    public async Task<Appointment> GetAsync(string id)
    {
        _logger.LogInformation($"Fetch appointment with id: {id}");

        return await _context._collection.Find(appointment => appointment.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        var appointments =  await _context._collection.Find(_ => true).ToListAsync();

        _logger.LogInformation($"Fetch all appointments {appointments.Count}");

        return appointments;
    }

    public async Task CreateAsync(NewAppointmentDto appointment)
    {
        var client = await _clientService.GetAsync(appointment.ClientId);
        var barber = await _barberService.GetAsync(appointment.BarberId);

        var createdAppointment = new Appointment()
        {
            Client = client,
            Barber = barber,
            ExactDate = appointment.ExactDate,
            BarberServices = appointment.BarberServices
        };

        await _context._collection.InsertOneAsync(createdAppointment);

        _logger.LogInformation($"Appointment with id: {createdAppointment.Id} was created");
    }

    public async Task DeleteAsync(string id)
    {
        await _context._collection.DeleteOneAsync(appointment => appointment.Id == ObjectId.Parse(id));

        _logger.LogInformation($"Appointment with id: {id} was deleted successfully");
    }
}