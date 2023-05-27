using System.ComponentModel;

namespace Appointments.Enums;

public enum BarberServices
{
    Hair,
    Beard,
    Nose,
    Ears,
    [Description("Hot towel")]
    HotTowel
}