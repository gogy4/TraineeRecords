namespace Domain.Entities;

public interface ITraineeRepository : IRepository<Trainee>
{
    Task<List<Trainee>> GetByProject(Guid projectId);
    Task<List<Trainee>> GetByDirection(Guid directionId);
}