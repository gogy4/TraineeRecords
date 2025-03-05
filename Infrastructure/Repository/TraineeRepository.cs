using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class TraineeRepository(AppDbContext context) : ITraineeRepository
{
    public async Task Add(string name, string surname, string email, string phoneNumber, string dateOfBirth,
        Guid internshipDirectionId, Guid currentProjectId)
    {
        var trainee = new Trainee(name, surname, email, phoneNumber, dateOfBirth, internshipDirectionId, currentProjectId);
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
    
}