using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class TraineeRepository(AppDbContext context) : ITraineeRepository
{
    public async Task AddAsync(Trainee trainee)
    {
        await context.Trainees.AddAsync(trainee);
        await context.SaveChangesAsync();
    }

    public async Task<Trainee?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await context.Trainees.FirstOrDefaultAsync(t => t.PhoneNumber == phoneNumber);
    }

    public async Task UpdateAsync(Trainee trainee)
    {
        context.Trainees.Update(trainee);
        await context.SaveChangesAsync();
    }

    public async Task<Trainee?> GetByIdAsync(Guid id)
    {
        return await context.Trainees.FindAsync(id);
    }

    public async Task<Trainee?> GetByEmailAsync(string email)
    {
        return await context.Trainees.FirstOrDefaultAsync(t => t.Email == email);
    }

    public async Task<List<Trainee>> GetByResourcesName(Guid internshipDirectionId, Guid currentProjectId)
    {
        return await context.Trainees
            .Where(t => (currentProjectId == Guid.Empty || t.CurrentProjectId == currentProjectId)
                        && (internshipDirectionId == Guid.Empty || t.InternshipDirectionId == internshipDirectionId))
            .ToListAsync();
    }

    public async Task<List<Trainee>> GetAllAsync()
    {
        return await context.Trainees.ToListAsync();
    }

    public async Task<List<Trainee>> GetByProjectAsync(Guid projectId)
    {
        return await context.Trainees
            .Where(t => t.CurrentProjectId == projectId)
            .ToListAsync();
    }

    public async Task<List<Trainee>> GetByDirectionAsync(Guid directionId)
    {
        return await context.Trainees
            .Where(t => t.InternshipDirectionId == directionId)
            .ToListAsync();
    }

   

    public async Task DeleteAsync(Trainee trainee)
    {
        context.Remove(trainee);
        await context.SaveChangesAsync();
    }
}