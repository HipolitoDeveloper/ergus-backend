using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure
{
    public class AppClientContext : DbContext, IDbContext
    {
        #region [ Propriedades ]

        public virtual DbSet<Address>? Addresses                            { get; set; }
        public virtual DbSet<Advertisement>? Advertisements                 { get; set; }
        public virtual DbSet<AdvertisementSku>? AdvertisementSkus           { get; set; }
        public virtual DbSet<AdvertisementSkuPrice>? AdvertisementSkuPrices { get; set; }
        public virtual DbSet<Category>? Categories                          { get; set; }
        public virtual DbSet<CategoryText>? CategoryTexts                   { get; set; }
        public virtual DbSet<Grid>? Grids                                   { get; set; }
        public virtual DbSet<HorizontalVariation>? HorizontalVariations     { get; set; }
        public virtual DbSet<Integration>? Integrations                     { get; set; }
        public virtual DbSet<Metadata>? Metadatas                           { get; set; }
        public virtual DbSet<PriceList>? PriceLists                         { get; set; }
        public virtual DbSet<Product>? Products                             { get; set; }
        public virtual DbSet<Producer>? Producers                           { get; set; }
        public virtual DbSet<Provider>? Providers                           { get; set; }
        public virtual DbSet<ProductAttribute>? ProductAttributes           { get; set; }
        public virtual DbSet<Section>? Sections                             { get; set; }
        public virtual DbSet<Sku>? Skus                                     { get; set; }
        public virtual DbSet<SkuPrice>? SkuPrices                           { get; set; }
        public virtual DbSet<StockUnit>? StockUnits                         { get; set; }
        public virtual DbSet<VerticalVariation>? VerticalVariations         { get; set; }

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        internal AppClientContext() { }

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
