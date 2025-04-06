using Microsoft.EntityFrameworkCore;

namespace service_api.Models;

public class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
    }

    public DbSet<services> services { get; set; }
}