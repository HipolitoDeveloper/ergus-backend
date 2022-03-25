using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IIntegrationRepository : IRepository
    {
        Task<Integration> Add(Integration integration);
        Task<Integration?> Get(int id, bool keepTrack);
        Task<Integration?> GetByCode(string code);
        Task<List<Integration>> GetAll();
        Task<Integration> Update(Integration integration);
    }

    internal class IntegrationRepository : IIntegrationRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public IntegrationRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Integration> Add(Integration integration)
        {
            var createdIntegration = await this._context.Integrations!.AddAsync(integration);

            return createdIntegration.Entity;
        }

        public async Task<Integration?> Get(int id, bool keepTrack)
        {
            var query = this._context.Integrations!.Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var integration = await query.FirstOrDefaultAsync();

            return integration;
        }

        public async Task<Integration?> GetByCode(string code)
        {
            var query = this._context.Integrations!.Where(p => p.Code.ToLower() == code.ToLower());
            var integration = await query.AsNoTracking().FirstOrDefaultAsync();

            return integration;
        }

        public async Task<List<Integration>> GetAll()
        {
            var query = this._context.Integrations!;
            var integrations = await query.AsNoTracking().ToListAsync();

            return integrations;
        }

        public async Task<Integration> Update(Integration integration)
        {
            this._context.Integrations!.Update(integration);

            return await Task.FromResult(integration);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
