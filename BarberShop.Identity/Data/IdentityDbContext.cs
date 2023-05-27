using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BarberShop.Identity.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BarberShop.Identity.Data;

public class IdentityDbContext : IdentityDbContext<User> 
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}