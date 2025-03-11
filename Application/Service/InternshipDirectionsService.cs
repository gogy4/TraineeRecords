using Domain.Entities;

namespace Application.Services;

public class InternshipDirectionsService(IInternshipDirectionRepository repository)
{
    public async Task<Guid> Create(string name)
    {
        var internshipDirection = new InternshipDirection(name);
        await repository.AddAsync(internshipDirection);
        return internshipDirection.Id;
    }

    public async Task<InternshipDirection> GetByName(string name)
    {
        return (await repository.GetByNameAsync(name))
            .Where(d => d.Name == name)
            .FirstOrDefault();
    }

    public async Task Delete(Guid directionId)
    {
        await repository.DeleteAsync(directionId);
    }

    public async Task<List<InternshipDirection>> GetByFilter(string filter)
    {
        if (filter is null || filter.Split().Length == 0) return await GetAll();
        return await repository.GetByNameAsync(filter);
    }

    public async Task<InternshipDirection> GetById(Guid? id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task Update(InternshipDirection direction)
    {
        await repository.UpdateAsync(direction);
    }

    public async Task<List<InternshipDirection>> GetAll()
    {
        return await repository.GetAllAsync();
    }

    public async Task ChangeName(Guid id, string name)
    {
        if (await GetByName(name) is not null)
            throw new ArgumentException("Такое направление уже существует");

        var direction = await GetById(id);
        if (direction is null) throw new ArgumentException("Выберите направление");
        direction.ChangeName(name);
        await Update(direction);
    }
}