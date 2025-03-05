using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class InternshipDirectionRepository(AppDbContext context) : IInternshipDirectionRepository
{
    public async Task Add(string direction)
    {
        var internshipDirection = new InternshipDirection(direction);
        await context.InternshipDirections.AddAsync(internshipDirection);
        await context.SaveChangesAsync();
    }

    public async Task Update(Trainee trainee)
    {
        context.Update(trainee);
        await context.SaveChangesAsync();
    }

    public async Task<InternshipDirection?> GetById(Guid id)
    {
        return await context.InternshipDirections.FindAsync(id);
    }
}