using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanningAPI.Model;
using System.Linq.Expressions;

namespace PlanningAPI.Infrastructure
{
	public class TripRepository : IRepository<Trip>
	{
		private readonly ApplicationDbContext _dbContext;
		
		public TripRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public async Task<bool> AddAsync(Trip entity)
        {
            await _dbContext.Trips.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(int tripId)
		{
			var tripEntity = await GetByIdAsync(tripId);

			if (tripEntity == null)
			{
				return false;
			}

			_dbContext.Remove(tripEntity);

			return await SaveAsync();
		}

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.Trips.AnyAsync(x => x.TripId == id);
        }

        public async Task<Trip> GetByIdAsync(int id)
		{
			var tripEntity = await _dbContext.Trips.AsNoTracking().FirstOrDefaultAsync(x => x.TripId == id);

			return tripEntity;
		}

		public async Task<IEnumerable<Trip>> ListAsync(Expression<Func<Trip, bool>> filter)
		{
			if (filter == null)
			{
				filter = x => true;
			}

			var trips = await _dbContext.Trips.Where(filter).ToListAsync();

			return trips;
		}

        public async Task<bool> UpdateAsync(Trip entity)
        {
            _dbContext.Trips.Update(entity);

            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
		{
			return await _dbContext.SaveChangesAsync() > 0;
		}
	}
}
