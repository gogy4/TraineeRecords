using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Trainee?> Trainees { get; set; }
    public DbSet<InternshipDirection?> InternshipDirections { get; set; }
    public DbSet<CurrentProject?> CurrentProjects { get; set; }
}