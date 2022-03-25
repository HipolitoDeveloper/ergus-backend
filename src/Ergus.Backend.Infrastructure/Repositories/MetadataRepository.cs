using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IMetadataRepository : IRepository
    {
        Task<Metadata> Add(Metadata metadata);
        Task<Metadata?> Get(int id, bool keepTrack);
        Task<Metadata?> GetByCode(string code);
        Task<List<Metadata>> GetAll();
        Task<Metadata> Update(Metadata metadata);
    }

    internal class MetadataRepository : IMetadataRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public MetadataRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Metadata> Add(Metadata metadata)
        {
            var createdMetadata = await this._context.Metadatas!.AddAsync(metadata);

            return createdMetadata.Entity;
        }

        public async Task<Metadata?> Get(int id, bool keepTrack)
        {
            var query = this._context.Metadatas!.Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var metadata = await query.FirstOrDefaultAsync();

            return metadata;
        }

        public async Task<Metadata?> GetByCode(string code)
        {
            return null;
            //var query = this._context.Metadatas!.Where(p => p.Code.ToLower() == code.ToLower());
            //var metadata = await query.AsNoTracking().FirstOrDefaultAsync();

            //return metadata;
        }

        public async Task<List<Metadata>> GetAll()
        {
            var query = this._context.Metadatas!;
            var metadatas = await query.AsNoTracking().ToListAsync();

            return metadatas;
        }

        public async Task<Metadata> Update(Metadata metadata)
        {
            this._context.Metadatas!.Update(metadata);

            return await Task.FromResult(metadata);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
