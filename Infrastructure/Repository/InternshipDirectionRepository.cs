using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class InternshipDirectionRepository(AppDbContext context) : IInternshipDirectionRepository
{
    public async Task Add(InternshipDirection direction)
    {
        await context.InternshipDirections.AddAsync(direction);
        await context.SaveChangesAsync();
    }

    public async Task Update(InternshipDirection direction)
    {
        context.Update(direction);
        await context.SaveChangesAsync();
    }

    public async Task<InternshipDirection?> GetById(Guid id)
    {
        return await context.InternshipDirections.FindAsync(id);
    }

    public async Task<List<InternshipDirection>> GetAll()
    {
        return await context.InternshipDirections.ToListAsync();
    }

    public async Task<List<InternshipDirection>> GetByName(string name)
    {
        return await context.InternshipDirections
            .Where(p => p.Name == name)
            .ToListAsync();
    }

    public async Task<List<InternshipDirection>> GetByTraineeCount(int traineeCount)
    {
        return await context.InternshipDirections
            .Where(p => p.CountTrainees == traineeCount)
            .ToListAsync();
    }

    public async Task Delete(InternshipDirection direction)
    {
        context.Remove(direction);
        await context.SaveChangesAsync();
    }
}