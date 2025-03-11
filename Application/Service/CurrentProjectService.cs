using Application.Dto;
using Domain.Entities;

namespace Application.Services;

public class CurrentProjectService(ICurrentProjectRepository repository)
{
    public async Task<Guid> Create(string name)
    {
        var project = new CurrentProject(name);
        await repository.AddAsync(project);
        return project.Id;
    }

    public async Task<CurrentProject> GetByName(string name)
    {
        return (await repository.GetByNameAsync(name))
            .Where(p => p.Name == name)
            .FirstOrDefault();
    }

    public async Task<CurrentProject> GetById(Guid? id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task Delete(Guid id)
    {
        await repository.DeleteAsync(id);
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

    public async Task ChangeName(Guid id, string name)
    {
        if (await GetByName(name) is not null)
            throw new ArgumentException("Такой проект уже существует");
        var project = await GetById(id);
        if (project is null) throw new ArgumentException("Выберите проект");
        project.ChangeName(name);
        await Update(project);
    }
}