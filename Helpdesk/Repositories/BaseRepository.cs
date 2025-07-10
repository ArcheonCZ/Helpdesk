using Helpdesk.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;


namespace Helpdesk.Repositories

{



	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		protected readonly HelpdeskDbContext helpdeskDbContext;
		protected readonly DbSet<TEntity> dbSet;


		public BaseRepository(HelpdeskDbContext helpdeskDbContext)
		{
			this.helpdeskDbContext = helpdeskDbContext;
			dbSet = helpdeskDbContext.Set<TEntity>();
		}


		public async Task<TEntity?> FindById(uint id)
		{
			return await dbSet.FindAsync(id);
		}

		public async Task<bool> ExistsWithId(uint id)
		{
			TEntity? entity = await FindById(id);
			if (entity is not null)
				helpdeskDbContext.Entry(entity).State = EntityState.Detached;
			return entity is not null;
		}

		public async Task<IList<TEntity>> GetAll()
		{
			return await dbSet.ToListAsync();
		}

		public async Task<TEntity> Insert(TEntity entity)
		{
			EntityEntry<TEntity> entityEntry = await dbSet.AddAsync(entity);
			await helpdeskDbContext.SaveChangesAsync();
			return entityEntry.Entity;
		}

		public async Task<TEntity> Update(TEntity entity)
		{
			var existingEntity = await FindById((uint)GetPrimaryKey(entity));

			if (existingEntity == null)
			{
				throw new InvalidOperationException("Entita nenalezena.");
			}

			helpdeskDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
			await helpdeskDbContext.SaveChangesAsync();
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
				helpdeskDbContext.SaveChanges();
			}
			catch
			{
				helpdeskDbContext.Entry(entity).State = EntityState.Unchanged;
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