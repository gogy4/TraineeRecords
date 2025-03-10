using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class CurrentProjectRepository(AppDbContext context) : ICurrentProjectRepository
{
    public async Task AddAsync(CurrentProject project)
    {
        await context.CurrentProjects.AddAsync(project);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CurrentProject currentProject)
    {
        context.Update(currentProject);
        await context.SaveChangesAsync();
    }

    public async Task<CurrentProject?> GetByIdAsync(Guid? id)
    {
        return await context.CurrentProjects.FindAsync(id);
    }

    public async Task<List<CurrentProject>> GetAllAsync()
    {
        return await context.CurrentProjects.ToListAsync();
    }

    public async Task<List<CurrentProject>> GetByNameAsync(string name)
    {
        return await context.CurrentProjects
            .Where(p => p.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<List<CurrentProject>> GetByTraineeCountAsync(int traineeCount)
    {
        return await context.CurrentProjects
            .Where(p => p.CountTrainees == traineeCount)
            .ToListAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var project = await GetByIdAsync(id);
        context.CurrentProjects.Remove(project);
        await context.SaveChangesAsync();
    }
}