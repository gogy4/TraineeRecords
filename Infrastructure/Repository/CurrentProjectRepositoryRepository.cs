using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class CurrentProjectRepository(AppDbContext context) : ICurrentProjectRepository
{
    public async Task Add(string name)
    {
        var project = new CurrentProject(name);
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
}