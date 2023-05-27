using Appointments.Enums;
using Appointments.Models.Users;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Appointments.Models;

public class Appointment
{
    public Appointment()
    {
        Id = Guid.NewGuid().ToString();
        BarberServices = new HashSet<BarberServices>();
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public Barber Barber { get; set; }
    public Client Client { get; set; }
    public DateTime ExactDate { get; set; }
    public IEnumerable<BarberServices> BarberServices { get; set; }
}