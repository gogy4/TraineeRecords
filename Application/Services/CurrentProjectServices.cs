using Domain.Entities;

namespace Application.Services;

public class CurrentProjectServices(ICurrentProjectRepository repository)
{
    public async Task<CurrentProject> Create(string name)
    {
        var project = new CurrentProject(name);
        await repository.AddAsync(project);
        return project;
    }

    public async Task<CurrentProject> GetByName(string name)
    {
        return (await repository.GetByNameAsync(name)).FirstOrDefault();
    }

    public async Task<CurrentProject> GetById(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<List<CurrentProject>> GetByFilter(string filter)
    {
        if (filter is null || filter.Split().Length == 0) return await GetAll();
        return await repository.GetByNameAsync(filter);
    }

    public async Task Update(CurrentProject project)
    {
        await repository.UpdateAsync(project);
    }
    
    public async Task<List<CurrentProject>> GetAll()
    {
        return await repository.GetAllAsync();
    }
}