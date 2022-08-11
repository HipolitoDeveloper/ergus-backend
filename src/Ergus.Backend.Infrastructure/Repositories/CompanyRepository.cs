using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface ICompanyRepository : IRepository
    {
        Task<Company> Add(Company company);
        Task<Company?> Get(int id, bool keepTrack);
        Task<Company?> GetByCode(string code);
        Task<List<Company>> GetAll();
        Task<Company> Update(Company company);
    }

    internal class CompanyRepository : ICompanyRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public CompanyRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Company> Add(Company company)
        {
            var createdCompany = await this._context.Companies!.AddAsync(company);

            return createdCompany.Entity;
        }

        public async Task<Company?> Get(int id, bool keepTrack)
        {
            var query = this._context.Companies!.Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var company = await query.FirstOrDefaultAsync();

            return company;
        }

        public async Task<Company?> GetByCode(string code)
        {
            var query = this._context.Companies!.Where(p => p.Code.ToLower() == code.ToLower());
            var company = await query.AsNoTracking().FirstOrDefaultAsync();

            return company;
        }

        public async Task<List<Company>> GetAll()
        {
            var query = this._context.Companies!;
            var companys = await query.AsNoTracking().ToListAsync();

            return companys;
        }

        public async Task<Company> Update(Company company)
        {
            this._context.Companies!.Update(company);

            return await Task.FromResult(company);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
