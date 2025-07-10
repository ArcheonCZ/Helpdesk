namespace Helpdesk.Interfaces;


public interface IBaseRepository<TEntity> where TEntity : class
{
	Task<TEntity?> FindById(uint id);
	Task<bool> ExistsWithId(uint id);
	Task<IList<TEntity>> GetAll();
	Task<TEntity> Insert(TEntity entity);
	Task<TEntity> Update(TEntity entity);
	Task<bool> Delete(uint id);
}