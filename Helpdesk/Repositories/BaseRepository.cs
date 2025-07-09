using Helpdesk.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;


namespace Helpdesk.Repositories

{



	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		protected readonly HelpdeskDbContext invoicesDbContext;
		protected readonly DbSet<TEntity> dbSet;


		public BaseRepository(HelpdeskDbContext invoicesDbContext)
		{
			this.invoicesDbContext = invoicesDbContext;
			dbSet = invoicesDbContext.Set<TEntity>();
		}


		public async Task<TEntity?> FindById(uint id)
		{
			return await dbSet.FindAsync(id);
		}

		public bool ExistsWithId(uint id)
		{
			TEntity? entity = dbSet.Find(id);
			if (entity is not null)
				invoicesDbContext.Entry(entity).State = EntityState.Detached;
			return entity is not null;
		}

		public IList<TEntity> GetAll()
		{
			return dbSet.ToList();
		}

		public TEntity Insert(TEntity entity)
		{
			EntityEntry<TEntity> entityEntry = dbSet.Add(entity);
			invoicesDbContext.SaveChanges();
			return entityEntry.Entity;
		}

		public TEntity Update(TEntity entity)
		{
			var existingEntity = FindById((uint)GetPrimaryKey(entity));

			if (existingEntity == null)
			{
				throw new InvalidOperationException("Entity not found.");
			}

			//EntityEntry<TEntity> entityEntry = dbSet.Update(entity);
			invoicesDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
			invoicesDbContext.SaveChanges();
			//return entityEntry.Entity;
			return existingEntity;
		}

		public void Delete(uint id)
		{
			TEntity? entity = dbSet.Find(id);

			if (entity is null)
				return;

			try
			{
				dbSet.Remove(entity);
				invoicesDbContext.SaveChanges();
			}
			catch
			{
				invoicesDbContext.Entry(entity).State = EntityState.Unchanged;
				throw;
			}
		}

		private object GetPrimaryKey(TEntity entity)
		{
			var keyName = invoicesDbContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties
				.Select(x => x.Name).Single();

			return entity.GetType().GetProperty(keyName).GetValue(entity, null);
		}
	}
}