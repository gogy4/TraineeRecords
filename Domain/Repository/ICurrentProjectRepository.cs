namespace Domain.Entities;

public interface ICurrentProjectRepository : IRepository<CurrentProject>
{
    Task<List<CurrentProject>> GetByNameAsync(string name);
    Task<List<CurrentProject>> GetByTraineeCountAsync(int traineeCount);
}