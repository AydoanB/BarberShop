using BarberShop.Appointments.Data;
using BarberShop.Appointments.Models.Users;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BarberShop.Appointments.Services;

public class ClientService : IClientService
{
    private readonly MongoDbContext<Client> _context;
    private readonly ILogger<ClientService> _logger;

    public ClientService(
        MongoDbContext<Client> context, 
        ILogger<ClientService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Client> GetAsync(string userId)
    {
        var client = await _context._collection
            .Find(client => client.UserId == userId)
            .FirstOrDefaultAsync();

        _logger.LogInformation($"Found client: {client.ToJson()}");

        return client;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        var clients = await _context._collection.Find(_ => true).ToListAsync();

        _logger.LogInformation($"Fetch all client {clients.Count}");

        return clients;
    }

    public async Task CreateAsync(NewClientDto input, string currentUserId)
    {
        if (UserExists(currentUserId))
        {
            throw new InvalidOperationException("Client already exists");
        }

        var newClient = new Client
        {
            Name = input.Name,
            PhoneNumber = input.PhoneNumber,
            Preferences = input.Preferences,
            UserId = currentUserId
        };

        await _context._collection
            .InsertOneAsync(newClient);

        _logger.LogInformation($"Inserting client: {newClient.ToJson()}");
    }

    public async Task DeleteAsync(string userId)
    {
        var client = await _context._collection.DeleteOneAsync(client => client.UserId == userId);

        _logger.LogInformation($"Deleted client: {client.ToJson()}");
    }

    private bool UserExists(string userId) => _context._collection.Find(user => user.UserId == userId).Any();
}