using Domain.Entities;

namespace Application.Services;

public class InternshipDirectionsServices(IInternshipDirectionRepository repository)
{
    public async Task<InternshipDirection> Create(string name)
    {
        var internshipDirection = new InternshipDirection(name);
        await repository.AddAsync(internshipDirection);
        return internshipDirection;
    }

    public async Task<InternshipDirection> GetByName(string name)
    {
        return (await repository.GetByNameAsync(name)).FirstOrDefault();
    }
    
    public async Task<List<InternshipDirection>> GetByFilter(string filter)
    {
        if (filter is null || filter.Split().Length == 0) return await GetAll();
        return await repository.GetByNameAsync(filter);
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