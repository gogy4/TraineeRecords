using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ResourceRepository(AppDbContext context) : IResourceRepository
{
    public async Task Add(CurrentProject project)
    {
        await context.CurrentProjects.AddAsync(project);
        await context.SaveChangesAsync();
    }

    public async Task Update(CurrentProject currentProject)
    {
        context.Update(currentProject);
        await context.SaveChangesAsync();
    }

    public async Task<CurrentProject?> GetById(Guid id)
    {
        return await context.CurrentProjects.FindAsync(id);
    }

    public async Task<List<CurrentProject>> GetAll()
    {
        return await context.CurrentProjects.ToListAsync();
    }

    public async Task<List<CurrentProject>> GetByName(string name)
    {
        return await context.CurrentProjects
            .Where(p => p.Name == name)
            .ToListAsync();
    }

    public async Task<List<CurrentProject>> GetByTraineeCount(int traineeCount)
    {
        return await context.CurrentProjects
            .Where(p => p.CountTrainees == traineeCount)
            .ToListAsync();
    }

    public async Task Delete(CurrentProject currentProject)
    {
        context.CurrentProjects.Remove(currentProject);
        await context.SaveChangesAsync();
    }
}