namespace Domain.Entities;

public interface IInternshipDirectionRepository
{
    Task Add(string direction);

    Task Update(Trainee trainee);

    Task<InternshipDirection?> GetById(Guid id);
}