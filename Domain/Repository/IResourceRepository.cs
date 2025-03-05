namespace Domain.Entities;

public interface IResourceRepository : IRepository<CurrentProject>
{
    Task<List<CurrentProject>> GetByName(string name);
    Task<List<CurrentProject>> GetByTraineeCount(int traineeCount);
}