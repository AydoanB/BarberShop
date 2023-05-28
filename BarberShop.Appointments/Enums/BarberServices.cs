using System.ComponentModel;

namespace BarberShop.Appointments.Enums;

public enum BarberServices
{
    Hair,
    Beard,
    Nose,
    Ears,
    [Description("Hot towel")]
    HotTowel
}