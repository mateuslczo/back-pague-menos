namespace PagueMenos.Domain.Interfaces
{
	public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync();

		Task BeginTransactionAsync();

		Task CommitTransactionAsync();

	}
}