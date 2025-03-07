namespace Domain.Entities;

public interface IRepository<IEntity>
{
    Task AddAsync(IEntity entity);
    Task UpdateAsync(IEntity entity);
    Task<IEntity?> GetByIdAsync(Guid? id);
    Task<List<IEntity>> GetAllAsync();
    Task DeleteAsync(IEntity entity);
}