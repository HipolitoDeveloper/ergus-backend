using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IProducerRepository : IRepository
    {
        Task<Producer> Add(Producer producer);
        Task<Producer?> Get(int id, bool keepTrack);
        Task<Producer?> GetByCode(string code);
        Task<List<Producer>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Producer> Update(Producer producer);
    }

    internal class ProducerRepository : IProducerRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public ProducerRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Producer> Add(Producer producer)
        {
            var createdProducer = await this._context.Producers!.AddAsync(producer);

            return createdProducer.Entity;
        }

        public async Task<Producer?> Get(int id, bool keepTrack)
        {
            var query = this._context.Producers!.Include(p => p.Address).Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var producer = await query.FirstOrDefaultAsync();

            return producer;
        }

        public async Task<Producer?> GetByCode(string code)
        {
            var query = this._context.Producers!.Where(p => p.Code.ToLower() == code.ToLower());
            var producer = await query.AsNoTracking().FirstOrDefaultAsync();

            return producer;
        }

        public async Task<List<Producer>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.Producers;
            var producers = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await producers.ToListAsync();

            return await producers
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.Producers!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<Producer> Update(Producer producer)
        {
            this._context.Producers!.Update(producer);

            return await Task.FromResult(producer);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
