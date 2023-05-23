using Appointments.Data;
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

    public  Client Get(string id)
    {
        _logger.LogInformation($"Fetch appointment with id: {id}");

        return _context._collection.Find(client => client.Id == id)
            .FirstOrDefault();
    }

    public async Task CreateAsync()
    {

        var client = new Client
        {
            Name = "Mee",
        };

       await _context._collection.InsertOneAsync(client);

        _logger.LogInformation($"Inserting appointment with id: {client.Id}");

    }
}