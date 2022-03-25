using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure
{
    public class AppClientContext : DbContext, IDbContext
    {
        #region [ Propriedades ]

        public DbSet<Advertisement>? Advertisements         { get; set; }
        public DbSet<Category>? Categories                  { get; set; }
        public DbSet<Integration>? Integrations             { get; set; }
        public DbSet<Metadata>? Metadatas                   { get; set; }
        public DbSet<Product>? Products                     { get; set; }
        public DbSet<Producer>? Producers                   { get; set; }
        public DbSet<Provider>? Providers                   { get; set; }
        public DbSet<ProductAttribute>? ProductAttributes   { get; set; }
        public DbSet<Sku>? Skus                             { get; set; }

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public AppClientContext(DbContextOptions<AppClientContext> options) : base(options) { }

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

            modelBuilder.Entity<Category>().HasIndex(a => a.Code);
            modelBuilder.Entity<Category>().HasIndex(a => a.ExternalCode);
            modelBuilder.Entity<Category>().HasIndex(a => a.Id);

            base.OnModelCreating(modelBuilder);
        }

        #endregion [ FIM - Metodos ]
    }
}
