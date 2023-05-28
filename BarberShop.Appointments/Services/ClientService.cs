using BarberShop.Appointments.Data;
using BarberShop.Appointments.Models.Users;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BarberShop.Appointments.Services;

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
        _logger.LogInformation($"Fetching appointment with id: {id}");

        return _context._collection
            .Find(client => client.Id == ObjectId.Parse(id))
            .FirstOrDefault();
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        var clients = await _context._collection.Find(_ => true).ToListAsync();

        _logger.LogInformation($"Fetch all appointments {clients.Count}");

        return clients;
    }

    public async Task<string> CreateAsync(NewClientDto input)
    {
        var newClient = new Client
        {
            Name = input.Name,
            PhoneNumber = input.PhoneNumber,
            Preferences = input.Preferences,
        };

        await _context._collection
            .InsertOneAsync(newClient);

       _logger.LogInformation($"Inserting appointment: {newClient.ToJson()}");

       return newClient.Id.ToString();
    }

    public async Task DeleteAsync(string id)
    {
        var a= await _context._collection.DeleteOneAsync(client => client.Id == ObjectId.Parse(id));

        _logger.LogInformation($"Deleted client: {a.ToJson()}");
    }
}