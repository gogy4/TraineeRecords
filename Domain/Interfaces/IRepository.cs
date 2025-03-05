namespace Domain.Entities;

public interface IRepository<IEntity>
{
    Task Add(IEntity entity);
    Task Update(IEntity entity);
    Task<IEntity?> GetById(Guid id);
    Task<List<IEntity>> GetAll();
    Task Delete(IEntity entity);
}