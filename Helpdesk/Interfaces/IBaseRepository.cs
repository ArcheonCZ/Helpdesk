namespace Helpdesk.Interfaces;


public interface IBaseRepository<TEntity> where TEntity : class
{
	IList<TEntity> GetAll();

	TEntity? FindById(uint? id);

	TEntity Insert(TEntity entity);

	TEntity Update(TEntity entity);

	void Delete(uint id);

	bool ExistsWithId(uint id);
}