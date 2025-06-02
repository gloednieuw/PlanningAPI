using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanningAPI.Model;
using System.Linq.Expressions;

namespace PlanningAPI.Infrastructure
{
	public class UpdateLogRepository : IRepository<UpdateLog>
	{
		private readonly ApplicationDbContext _dbContext;
		
		public UpdateLogRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public async Task<bool> AddAsync(UpdateLog entity)
        {
            await _dbContext.UpdateLogs.AddAsync(entity);
			return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(int updateLogId)
		{
			var updateLog = await GetByIdAsync(updateLogId);

			if (updateLog == null)
			{
				return false;
			}

			_dbContext.Remove(updateLog);

			return await SaveAsync();
		}

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.UpdateLogs.AnyAsync(x => x.UpdateLogId == id);
        }

        public async Task<UpdateLog> GetByIdAsync(int id)
		{
			var updateLog = await _dbContext.UpdateLogs.AsNoTracking().FirstOrDefaultAsync(x => x.UpdateLogId == id);

			return updateLog;
		}

		public async Task<IEnumerable<UpdateLog>> ListAsync(Expression<Func<UpdateLog, bool>> filter)
		{
			if (filter == null)
			{
				filter = x => true;
			}

			var updateLogs = await _dbContext.UpdateLogs.Where(filter).ToListAsync();

			return updateLogs;
		}

        public async Task<bool> UpdateAsync(UpdateLog entity)
        {
            _dbContext.UpdateLogs.Update(entity);

            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
		{
			return await _dbContext.SaveChangesAsync() > 0;
		}
	}
}
