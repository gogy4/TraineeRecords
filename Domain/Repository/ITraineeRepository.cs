namespace Domain.Entities;

public interface ITraineeRepository : IRepository<Trainee>
{
    Task<Trainee?> GetByPhoneNumberAsync(string phoneNumber);
    Task<Trainee?> GetByEmailAsync(string email);
    Task<List<Trainee>> GetByResourceIds(Guid? internshipDirectionId, Guid? currentProjectId);
}