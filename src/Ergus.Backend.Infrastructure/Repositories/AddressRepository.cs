using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IAddressRepository : IRepository
    {
        Task<Address> Add(Address address);
        Task<Address?> Get(int id, bool keepTrack);
        Task<Address?> GetByCode(string code);
        Task<List<Address>> GetAll();
        Task<Address> Update(Address address);
    }

    internal class AddressRepository : IAddressRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public AddressRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<Address> Add(Address address)
        {
            var createdAddress = await this._context.Addresses!.AddAsync(address);

            return createdAddress.Entity;
        }

        public async Task<Address?> Get(int id, bool keepTrack)
        {
            var query = this._context.Addresses!.Where(m => m.Id == id);
            //var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var address = await query.FirstOrDefaultAsync();

            return address;
        }

        public async Task<Address?> GetByCode(string code)
        {
            var query = this._context.Addresses!.Where(p => p.Code.ToLower() == code.ToLower());
            var address = await query.AsNoTracking().FirstOrDefaultAsync();

            return address;
        }

        public async Task<List<Address>> GetAll()
        {
            var query = this._context.Addresses!;
            var addresss = await query.AsNoTracking().ToListAsync();

            return addresss;
        }

        public async Task<Address> Update(Address address)
        {
            this._context.Addresses!.Update(address);

            return await Task.FromResult(address);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
