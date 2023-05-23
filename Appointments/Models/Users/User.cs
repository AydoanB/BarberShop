using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Appointments.Models.Users;

public abstract class User
{
    public User()
    {
        Appointments = new HashSet<Appointment>();
        Schedule = new HashSet<DateTime>();
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string? Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; }
    public IEnumerable<DateTime> Schedule { get; set; }

}