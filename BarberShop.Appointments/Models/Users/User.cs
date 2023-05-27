using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Appointments.Models.Users;

public abstract class User
{
    protected User()
    {
        Appointments = new HashSet<Appointment>();
        Schedule = new HashSet<DateTime>();
    }

    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public IEnumerable<Appointment> Appointments { get; set; }
    public IEnumerable<DateTime> Schedule { get; set; }

}