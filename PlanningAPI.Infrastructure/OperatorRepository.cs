using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanningAPI.Model;
using System.Linq.Expressions;

namespace PlanningAPI.Infrastructure
{
	public class OperatorRepository : IRepository<Operator>
	{
		private readonly ApplicationDbContext _dbContext;
		
		public OperatorRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public async Task<bool> AddAsync(Operator entity)
        {
            await _dbContext.Operators.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(int operatorId)
		{
			var operatorEntity = await GetByIdAsync(operatorId);

			if (operatorEntity == null)
			{
				return false;
			}

			_dbContext.Remove(operatorEntity);

			return await SaveAsync();
		}

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Operators.AnyAsync(x => x.OperatorId == id);
        }

        public async Task<Operator> GetByIdAsync(int id)
		{
			var operatorEntity = await _dbContext.Operators
				.AsNoTracking()
				.Include(x => x.Lines)
					.ThenInclude(x => x.Trips)
				.FirstOrDefaultAsync(x => x.OperatorId == id);

			return operatorEntity;
		}

		public async Task<IEnumerable<Operator>> ListAsync(Expression<Func<Operator, bool>> filter)
		{
			if (filter == null)
			{
				filter = x => true;
			}

			var operators = await _dbContext.Operators.Where(filter).ToListAsync();

			return operators;
		}

        public async Task<bool> UpdateAsync(Operator entity)
        {
            _dbContext.Operators.Update(entity);

            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
		{
			return await _dbContext.SaveChangesAsync() > 0;
		}
	}
}
