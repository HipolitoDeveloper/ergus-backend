using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IPaymentFormService
    {
        Task<PaymentForm?> Add(PaymentForm paymentForm);
        Task<PaymentForm?> Delete(int id);
        Task<PaymentForm?> Get(int id);
        Task<List<PaymentForm>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<PaymentForm?> Update(PaymentForm paymentForm);
    }

    internal class PaymentFormService : IPaymentFormService
    {
        private readonly IPaymentFormRepository _paymentFormRepository;

        public PaymentFormService(IPaymentFormRepository paymentFormRepository)
        {
            this._paymentFormRepository = paymentFormRepository;
        }

        public async Task<PaymentForm?> Add(PaymentForm paymentForm)
        {
            if (!paymentForm.EhValido())
                return paymentForm;

            await this._paymentFormRepository.Add(paymentForm);

            var success = await this._paymentFormRepository.UnitOfWork.Commit();

            return success ? paymentForm : null;
        }

        public async Task<PaymentForm?> Delete(int id)
        {
            var paymentForm = await this._paymentFormRepository.Get(id, true);

            if (paymentForm == null)
                return null;

            paymentForm.DefinirComoRemovido(id);

            await this._paymentFormRepository.Update(paymentForm);

            var success = await this._paymentFormRepository.UnitOfWork.Commit();

            return success ? paymentForm : null;
        }

        public async Task<PaymentForm?> Get(int id)
        {
            return await this._paymentFormRepository.Get(id, false);
        }

        public async Task<List<PaymentForm>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._paymentFormRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._paymentFormRepository.GetAllIds();
        }

        public async Task<PaymentForm?> Update(PaymentForm paymentForm)
        {
            var oldPaymentForm = await this._paymentFormRepository.Get(paymentForm.Id, true);

            if (oldPaymentForm == null)
                return null;

            oldPaymentForm.MergeToUpdate(paymentForm);

            if(!oldPaymentForm.EhValido())
                return oldPaymentForm;

            await this._paymentFormRepository.Update(oldPaymentForm);

            var success = await this._paymentFormRepository.UnitOfWork.Commit();

            return success ? paymentForm : null;
        }
    }
}
