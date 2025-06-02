using System.Linq.Expressions;

namespace PlanningAPI.Model
{
	public interface IRepository<T>
	{
		Task<bool> AddAsync(T entity);
		Task<T> GetByIdAsync(int id);
		Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter = null);
		Task<bool> UpdateAsync(T entity);
		Task<bool> DeleteAsync(int id);
		Task<bool> Exists(int id);
	}
}
