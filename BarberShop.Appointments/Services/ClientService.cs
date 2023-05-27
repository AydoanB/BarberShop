using Appointments.Data;
using Appointments.Models;
using Appointments.Models.Users;
using MongoDB.Driver;

namespace Appointments.Services;

public class ClientService : IClientService
{
    private readonly MongoDbContext<Client> _context;
    private readonly ILogger<ClientService> _logger;

    public ClientService(MongoDbContext<Client> context, ILogger<ClientService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Client Get(string id)
    {
        var filter = Builders<Client>.Filter.Eq("Id", id);

        _logger.LogInformation($"Fetch appointment with id: {id}");

        return _context._collection.Find(filter).FirstOrDefault();
    }
}