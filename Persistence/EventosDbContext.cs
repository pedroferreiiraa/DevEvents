using Microsoft.AspNetCore.Http.HttpResults;
using DevEvents.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevEvents.API.Persistence;
public class EventosDbContext : DbContext
{
    public EventosDbContext(DbContextOptions<EventosDbContext> options) 
        : base(options)
    {
        
    }


    public DbSet<Evento> Eventos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Evento>()
           .HasKey(e => e.Id);
    }
}