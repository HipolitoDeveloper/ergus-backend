using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure
{
    public class AppServerContext : DbContext, IDbContext
    {
        #region [ Propriedades ]

        public DbSet<User>? Users           { get; set; }
        public DbSet<UserToken>? UserTokens { get; set; }

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public AppServerContext(DbContextOptions<AppServerContext> options) : base(options) { }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<bool> Commit()
        {
            try
            {
                var sucesso = base.SaveChanges() > 0;

                return await Task.FromResult(sucesso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> CommitReturningAggregateRootId()
        {
            throw new NotImplementedException();
        }

        public void ReloadContext()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                entry.Reload();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            base.OnModelCreating(modelBuilder);
        }

        #endregion [ FIM - Metodos ]
    }
}
