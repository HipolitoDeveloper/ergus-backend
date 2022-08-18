using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ergus.Backend.Infrastructure.Repositories
{
    public interface IPaymentFormRepository : IRepository
    {
        Task<PaymentForm> Add(PaymentForm paymentForm);
        Task<PaymentForm?> Get(int id, bool keepTrack);
        Task<PaymentForm?> GetByCode(string code);
        Task<List<PaymentForm>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<PaymentForm> Update(PaymentForm paymentForm);
    }

    internal class PaymentFormRepository : IPaymentFormRepository
    {
        #region [ Propriedades ]

        private readonly AppClientContext _context;
        public IUnitOfWork UnitOfWork => _context;

        #endregion [ FIM - Propriedades ]

        #region [ Construtores ]

        public PaymentFormRepository() { }

        public PaymentFormRepository(AppClientContext context)
        {
            this._context = context;
        }

        #endregion [ FIM - Construtores ]

        #region [ Metodos ]

        public async Task<PaymentForm> Add(PaymentForm paymentForm)
        {
            var createdPaymentForm = await this._context.PaymentForms!.AddAsync(paymentForm);

            return createdPaymentForm.Entity;
        }

        public async Task<PaymentForm?> Get(int id, bool keepTrack)
        {
            var query = this._context.PaymentForms!.Where(c => c.Id == id);
            var sql = query.ToQueryString();

            if (!keepTrack)
                query = query.AsNoTracking();

            var paymentForm = await query.FirstOrDefaultAsync();

            return paymentForm;
        }

        public async Task<PaymentForm?> GetByCode(string code)
        {
            var query = this._context.PaymentForms!.Where(c => c.Code.ToLower() == code.ToLower());
            var paymentForm = await query.AsNoTracking().FirstOrDefaultAsync();

            return paymentForm;
        }

        public async Task<List<PaymentForm>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            var query = this._context.PaymentForms!;
            var paymentForms = query.OrderBy(q => q.Name).AsNoTracking();

            if (disablePagination)
                return await paymentForms.ToListAsync();

            return await paymentForms
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<List<int>> GetAllIds()
        {
            var ids = await this._context.PaymentForms!.Select(a => a.Id).OrderBy(q => q).ToListAsync();
            return ids;
        }

        public async Task<PaymentForm> Update(PaymentForm paymentForm)
        {
            this._context.PaymentForms!.Update(paymentForm);

            return await Task.FromResult(paymentForm);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        #endregion [ FIM - Metodos ]
    }
}
