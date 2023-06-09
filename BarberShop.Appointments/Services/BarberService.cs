﻿using BarberShop.Appointments.Data;
using BarberShop.Appointments.Models.Users;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BarberShop.Appointments.Services;

public class BarberService : IBarberService
{
    private readonly MongoDbContext<Barber> _context;
    private readonly ILogger<AppointmentService> _logger;

    public BarberService(
        MongoDbContext<Barber> context, 
        ILogger<AppointmentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Barber> GetAsync(string userId)
    {
       var barber = await _context._collection.Find(barber => barber.UserId == userId).FirstOrDefaultAsync();

       _logger.LogInformation($"Found barber: {barber.ToJson()}");

       return barber;
    }

    public async Task<IEnumerable<Barber>> GetAllAsync()
    {
        var barbers = await _context._collection.Find(_ => true).ToListAsync();

        _logger.LogInformation($"Fetched {barbers.Count} barbers");

        return barbers;
    }

    public async Task CreateAsync(NewBarberDto input, string currentUserId)
    {
        if (UserExists(currentUserId))
        {
            throw new InvalidOperationException("Barber already exists");
        }

        var newBarber = new Barber
        {
            Name = input.Name,
            PhoneNumber = input.PhoneNumber,
            UserId = currentUserId,
            AvailableServices = input.AvailableServices
        };

        await _context._collection.InsertOneAsync(newBarber);

        _logger.LogInformation($"Inserting client: {newBarber.ToJson()}");
    }

    public async Task<NewBarberDto> CreateUser(NewBarberDto consumer)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(string userId)
    {
        var barber = await _context._collection.DeleteOneAsync(barber => barber.UserId == userId);

        _logger.LogInformation($"Deleted barber: {barber.ToJson()}");
    }
    private bool UserExists(string userId) => _context._collection.Find(user => user.UserId == userId).Any();

}