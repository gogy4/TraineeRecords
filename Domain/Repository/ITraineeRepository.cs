namespace Domain.Entities;

public interface ITraineeRepository : IRepository<Trainee>
{
    Task<List<Trainee>> GetByProjectAsync(Guid projectId);
    Task<List<Trainee>> GetByDirectionAsync(Guid directionId);
}