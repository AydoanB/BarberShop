using BarberShop.Appointments.Enums;
using BarberShop.Appointments.Models.Users;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BarberShop.Appointments.Models.Appointments;

public class Appointment
{
    public Appointment()
    {
        BarberServices = new HashSet<BarberServices>();
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public Barber Barber { get; set; }
    public Client Client { get; set; }
    public DateTime ExactDate { get; set; }
    public IEnumerable<BarberServices> BarberServices { get; set; }
}