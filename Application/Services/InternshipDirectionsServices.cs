using Domain.Entities;

namespace Application.Services;

public class InternshipDirectionsServices(IInternshipDirectionRepository repository)
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
}