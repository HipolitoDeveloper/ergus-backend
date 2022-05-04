using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IProducerService
    {
        Task<Producer?> Add(Producer producer);
        Task<Producer?> Delete(int id);
        Task<Producer?> Get(int id);
        Task<List<Producer>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Producer?> Update(Producer producer);
    }

    internal class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerService(IProducerRepository producerRepository)
        {
            this._producerRepository = producerRepository;
        }

        public async Task<Producer?> Add(Producer producer)
        {
            if (!producer.EhValido())
                return producer;

            await this._producerRepository.Add(producer);

            var success = await this._producerRepository.UnitOfWork.Commit();

            return success ? producer : null;
        }

        public async Task<Producer?> Delete(int id)
        {
            var producer = await this._producerRepository.Get(id, true);

            if (producer == null)
                return null;

            producer.DefinirComoRemovido(id);

            await this._producerRepository.Update(producer);

            var success = await this._producerRepository.UnitOfWork.Commit();

            return success ? producer : null;
        }

        public async Task<Producer?> Get(int id)
        {
            return await this._producerRepository.Get(id, false);
        }

        public async Task<List<Producer>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._producerRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._producerRepository.GetAllIds();
        }

        public async Task<Producer?> Update(Producer producer)
        {
            var oldProducer = await this._producerRepository.Get(producer.Id, true);

            if (oldProducer == null)
                return null;

            oldProducer.MergeToUpdate(producer);

            if(!oldProducer.EhValido())
                return oldProducer;

            await this._producerRepository.Update(oldProducer);

            var success = await this._producerRepository.UnitOfWork.Commit();

            return success ? producer : null;
        }
    }
}
