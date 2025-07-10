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

		public async Task<bool> Delete(uint id)
		{
			TEntity? entity = await FindById(id);

			if (entity is not null)
			{
				try
				{
					dbSet.Remove(entity);
					await helpdeskDbContext.SaveChangesAsync();
					return true;
				}
				catch (Exception ex)
				{
					helpdeskDbContext.Entry(entity).State = EntityState.Unchanged;
					Console.WriteLine(ex.Message);
					throw;
				}
			}
			return false;
		}

		private object GetPrimaryKey(TEntity entity)
		{
			try
			{
				var keyName = helpdeskDbContext.Model
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