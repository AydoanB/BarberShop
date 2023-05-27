using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Appointments.Models.Users;

public abstract class User
{
    public User()
    {
        Id = Guid.NewGuid().ToString();
        Appointments = new HashSet<Appointment>();
        Schedule = new HashSet<DateTime>();
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; }
    public IEnumerable<DateTime> Schedule { get; set; }

}