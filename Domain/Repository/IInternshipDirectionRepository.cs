namespace Domain.Entities;

public interface IInternshipDirectionRepository : IRepository<InternshipDirection>
{
    Task<List<InternshipDirection>> GetByName(string name);
    Task<List<InternshipDirection>> GetByTraineeCount(int traineeCount);

}