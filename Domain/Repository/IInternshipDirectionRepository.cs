namespace Domain.Entities;

public interface IInternshipDirectionRepository : IRepository<InternshipDirection>
{
    Task<List<InternshipDirection>> GetByNameAsync(string name);
    Task<List<InternshipDirection>> GetByTraineeCountAsync(int traineeCount);

}