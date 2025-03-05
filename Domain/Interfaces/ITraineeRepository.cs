namespace Domain.Entities;

public interface ITraineeRepository
{
    Task Add(string name, string surname, string email, string phoneNumber, string dateOfBirth,
        Guid internshipDirectionId, Guid currentProjectId);

    Task Update(Trainee trainee);

    Task<Trainee?> GetById(Guid id);
}