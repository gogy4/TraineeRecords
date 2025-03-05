using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class InternshipDirectionRepository(AppDbContext context) : IInternshipDirectionRepository
{
    public async Task AddAsync(InternshipDirection direction)
    {
        await context.InternshipDirections.AddAsync(direction);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(InternshipDirection direction)
    {
        context.Update(direction);
        await context.SaveChangesAsync();
    }

    public async Task<InternshipDirection?> GetByIdAsync(Guid id)
    {
        return await context.InternshipDirections.FindAsync(id);
    }

    public async Task<List<InternshipDirection>> GetAllAsync()
    {
        return await context.InternshipDirections.ToListAsync();
    }

    public async Task<List<InternshipDirection>> GetByNameAsync(string name)
    {
        return await context.InternshipDirections
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<List<InternshipDirection>> GetByTraineeCountAsync(int traineeCount)
    {
        return await context.InternshipDirections
            .Where(p => p.CountTrainees == traineeCount)
            .ToListAsync();
    }

    public async Task DeleteAsync(InternshipDirection direction)
    {
        context.Remove(direction);
        await context.SaveChangesAsync();
    }
}