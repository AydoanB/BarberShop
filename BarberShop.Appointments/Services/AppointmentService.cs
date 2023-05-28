using BarberShop.Appointments.Data;
using BarberShop.Appointments.Models;
using MongoDB.Driver;

namespace BarberShop.Appointments.Services;

public class AppointmentService : IAppointmentService
{
    private readonly MongoDbContext<Appointment> _context;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(MongoDbContext<Appointment> context, ILogger<AppointmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Appointment Get(string id)
    {
        var filter = Builders<Appointment>.Filter.Eq("Id", id);

        _logger.LogInformation($"Fetch appointment with id: {id}");

        return _context._collection.Find(filter).FirstOrDefault();
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        var appointments =  await _context._collection.Find(_ => true).ToListAsync();

        _logger.LogInformation($"Fetch all appointments {appointments.Count}");

        return appointments;
    }

    public async Task CreateAsync(Appointment appointment)
    {
        var createdAppointment = new Appointment
        {
            Barber = appointment.Barber,
            Client = appointment.Client,
            ExactDate = appointment.ExactDate,
            BarberServices = appointment.BarberServices
        };

        await _context._collection.InsertOneAsync(createdAppointment);

        _logger.LogInformation($"Appointment with id: {createdAppointment.Id} was created");
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<Appointment>.Filter.Eq("Id", id);

        _logger.LogInformation($"Appointment with id: {id} was deleted");

        await _context._collection.DeleteOneAsync(filter);
    }
}