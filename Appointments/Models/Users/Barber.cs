using Appointments.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Appointments.Models.Users;

public class Barber
{
    public Barber()
    {
        AvailableServices = new HashSet<BarberServices>();
    }
    public IEnumerable<BarberServices> AvailableServices { get; set; }
}