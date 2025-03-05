using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class TraineeRepository(AppDbContext context) : ITraineeRepository
{
    public async Task Add(Trainee trainee)
    {
        await context.Trainees.AddAsync(trainee);
        await context.SaveChangesAsync();
    }


    public async Task Update(Trainee trainee)
    {
        context.Trainees.Update(trainee);
        await context.SaveChangesAsync();
    }

    public async Task<Trainee?> GetById(Guid id)
    {
        return await context.Trainees.FindAsync(id);
    }

    public async Task<List<Trainee>> GetAll()
    {
        return await context.Trainees.ToListAsync();
    }

    public async Task<List<Trainee>> GetByProject(Guid projectId)
    {
        return await context.Trainees
            .Where(t => t.CurrentProjectId == projectId)
            .ToListAsync();
    }

    public async Task<List<Trainee>> GetByDirection(Guid directionId)
    {
        return await context.Trainees
            .Where(t => t.InternshipDirectionId == directionId)
            .ToListAsync();
    }

    public async Task Delete(Trainee trainee)
    {
        context.Remove(trainee);
        await context.SaveChangesAsync();
    }
}