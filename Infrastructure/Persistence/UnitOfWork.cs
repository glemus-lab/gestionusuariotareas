using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private IDbContextTransaction? _currentTransaction;

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);

        public async Task BeginTransactionAsync(CancellationToken ct = default)
            => _currentTransaction = await context.Database.BeginTransactionAsync(ct);

        public async Task CommitTransactionAsync(CancellationToken ct = default)
        {
            try
            {
                await context.SaveChangesAsync(ct);
                if (_currentTransaction != null) await _currentTransaction.CommitAsync(ct);
            }
            catch
            {
                await RollbackTransactionAsync(ct);
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken ct = default)
        {
            if (_currentTransaction != null) await _currentTransaction.RollbackAsync(ct);
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }

        public void Dispose() => context.Dispose();
    }
}
