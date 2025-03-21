﻿using Domain.Entities;
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

    public async Task<Trainee?> GetByIdAsync(Guid? id)
    {
        return await context.Trainees.FindAsync(id);
    }

    public async Task<Trainee?> GetByEmailAsync(string email)
    {
        return await context.Trainees.FirstOrDefaultAsync(t => t.Email == email);
    }

    public async Task<List<Trainee>> GetByResourceIds(Guid? internshipDirectionId, Guid? currentProjectId)
    {
        return await context.Trainees
            .Where(t => (currentProjectId == Guid.Empty || currentProjectId == null ||
                         t.CurrentProjectId == currentProjectId)
                        && (internshipDirectionId == Guid.Empty || currentProjectId == null ||
                            t.InternshipDirectionId == internshipDirectionId))
            .ToListAsync();
    }

    public async Task<List<Trainee>> GetAllAsync()
    {
        return await context.Trainees.ToListAsync();
    }
    

    public async Task DeleteAsync(Guid id)
    {
        var trainee = await GetByIdAsync(id);
        context.Trainees.Remove(trainee);
        await context.SaveChangesAsync();
    }
}