﻿using BarberShop.Appointments.Enums;

namespace BarberShop.Appointments.Models.Users;

public class Barber : User
{
    public Barber()
    {
        AvailableServices = new HashSet<BarberServices>();
    }
    public IEnumerable<BarberServices> AvailableServices { get; set; }
}