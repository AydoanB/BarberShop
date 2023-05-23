﻿using Appointments.Data.Micro.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Appointments.Data;

public class MongoDbContext<T> where T : class
{
    public readonly IMongoCollection<T> _collection;

    public MongoDbContext(IOptions<MongoDBSettings> mongoDbSettings, string collection)
    {
        Type type = typeof(T);

        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
         _collection = database.GetCollection<T>(nameof(T));
    }
}