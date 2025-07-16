using Helpdesk.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;


namespace Helpdesk.Repositories

{
	public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
	{
		//protected readonly HelpdeskDbContext _helpdeskDbContext;
		//protected readonly DbSet<TEntity> _dbSet;
		protected readonly IDbContextFactory<HelpdeskDbContext> _contextFactory;

		public BaseRepository(IDbContextFactory<HelpdeskDbContext> contextFactory)//HelpdeskDbContext helpdeskDbContext)
		{
			//this._helpdeskDbContext = helpdeskDbContext;
			//_dbSet = helpdeskDbContext.Set<TEntity>();
			_contextFactory = contextFactory;
		}

		public async Task<IList<TEntity>> GetAll()
		{
			using var context = _contextFactory.CreateDbContext();
			return await context.Set<TEntity>().ToListAsync();
		}

		public async Task<TEntity?> FindById(uint id)
		{
			using var context = _contextFactory.CreateDbContext();
			return await context.Set<TEntity>().FindAsync(id);
		}

		public async Task<bool> ExistsWithId(uint id)
		{
			using var context = _contextFactory.CreateDbContext();
			TEntity? entity = await FindById(id);
			if (entity is not null)
				context.Entry(entity).State = EntityState.Detached;
			return entity is not null;
		}

		public async Task<TEntity> Insert(TEntity entity)
		{
			using var context = _contextFactory.CreateDbContext();
			EntityEntry<TEntity> entityEntry = await context.Set<TEntity>().AddAsync(entity);
			await context.SaveChangesAsync();
			return entityEntry.Entity;
		}

		public async Task<TEntity> Update(TEntity entity)
		{
			using var context = _contextFactory.CreateDbContext();
			var existingEntity = await FindById((uint)GetPrimaryKeyValue(entity));

			if (existingEntity == null)
			{
				throw new InvalidOperationException("Entity not found.");
			}

			context.Entry(existingEntity).CurrentValues.SetValues(entity);
			await context.SaveChangesAsync();
			return existingEntity;
		}

		public async Task<bool> Delete(uint id)
		{
			using var context = _contextFactory.CreateDbContext();
			TEntity? entity = await FindById(id);

			if (entity is not null)
			{
				try
				{
					context.Set<TEntity>()	.Remove(entity);
					await context.SaveChangesAsync();
					return true;
				}
				catch (Exception ex)
				{
					context.Entry(entity).State = EntityState.Unchanged;
					Console.WriteLine(ex.Message);
					throw;
				}
			}
			return false;
		}

		private object GetPrimaryKeyValue(TEntity entity)
		{
			using var context = _contextFactory.CreateDbContext();
			try
			{
				var keyName = context.Model
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