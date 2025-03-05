namespace Domain.Entities;

public interface ICurrentProjectRepository
{
    Task Add(string name);
    Task Update(CurrentProject currentProject);
    Task<CurrentProject?> GetById(Guid id);
}