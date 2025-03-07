namespace Domain.Entities;

public interface ITraineeRepository : IRepository<Trainee>
{
    Task<List<Trainee>> GetByProjectAsync(Guid projectId);
    Task<List<Trainee>> GetByDirectionAsync(Guid directionId);
    Task<Trainee?> GetByPhoneNumberAsync(string phoneNumber);
    Task<Trainee?> GetByEmailAsync(string email);
    Task<List<Trainee>> GetByResourceIds(Guid? internshipDirectionId, Guid? currentProjectId);
}