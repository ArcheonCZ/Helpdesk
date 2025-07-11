using Helpdesk.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;


namespace Helpdesk.Repositories

{



	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		protected readonly HelpdeskDbContext _helpdeskDbContext;
		protected readonly DbSet<TEntity> _dbSet;


		public BaseRepository(HelpdeskDbContext helpdeskDbContext)
		{
			this._helpdeskDbContext = helpdeskDbContext;
			_dbSet = helpdeskDbContext.Set<TEntity>();
		}

		public async Task<IList<TEntity>> GetAll()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<TEntity?> FindById(uint id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<bool> ExistsWithId(uint id)
		{
			TEntity? entity = await FindById(id);
			if (entity is not null)
				_helpdeskDbContext.Entry(entity).State = EntityState.Detached;
			return entity is not null;
		}

		public async Task<TEntity> Insert(TEntity entity)
		{
			EntityEntry<TEntity> entityEntry = await _dbSet.AddAsync(entity);
			await _helpdeskDbContext.SaveChangesAsync();
			return entityEntry.Entity;
		}

		public async Task<TEntity> Update(TEntity entity)
		{
			var existingEntity = await FindById((uint)GetPrimaryKeyValue(entity));

			if (existingEntity == null)
			{
				throw new InvalidOperationException("Entity not found.");
			}

			_helpdeskDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
			await _helpdeskDbContext.SaveChangesAsync();
			return existingEntity;
		}

		public async Task<bool> Delete(uint id)
		{
			TEntity? entity = await FindById(id);

			if (entity is not null)
			{
				try
				{
					_dbSet.Remove(entity);
					await _helpdeskDbContext.SaveChangesAsync();
					return true;
				}
				catch (Exception ex)
				{
					_helpdeskDbContext.Entry(entity).State = EntityState.Unchanged;
					Console.WriteLine(ex.Message);
					throw;
				}
			}
			return false;
		}

		private object GetPrimaryKeyValue(TEntity entity)
		{
			try
			{
				var keyName = _helpdeskDbContext.Model
					.FindEntityType(typeof(TEntity))!
					.FindPrimaryKey()!.Properties
					.Select(x => x.Name).Single();

				return entity.GetType()
					.GetProperty(keyName)!
					.GetValue(entity, null)!;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(ex.Message);
			}
		}
	}
}