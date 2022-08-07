using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IHorizontalVariationService
    {
        Task<HorizontalVariation?> Add(HorizontalVariation horizontalVariation);
        Task<HorizontalVariation?> Delete(int id);
        Task<HorizontalVariation?> Get(int id);
        Task<List<HorizontalVariation>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<HorizontalVariation?> Update(HorizontalVariation horizontalVariation);
    }

    internal class HorizontalVariationService : IHorizontalVariationService
    {
        private readonly IHorizontalVariationRepository _horizontalVariationRepository;

        public HorizontalVariationService(IHorizontalVariationRepository horizontalVariationRepository)
        {
            this._horizontalVariationRepository = horizontalVariationRepository;
        }

        public async Task<HorizontalVariation?> Add(HorizontalVariation horizontalVariation)
        {
            if (!horizontalVariation.EhValido())
                return horizontalVariation;

            await this._horizontalVariationRepository.Add(horizontalVariation);

            var success = await this._horizontalVariationRepository.UnitOfWork.Commit();

            return success ? horizontalVariation : null;
        }

        public async Task<HorizontalVariation?> Delete(int id)
        {
            var horizontalVariation = await this._horizontalVariationRepository.Get(id, true);

            if (horizontalVariation == null)
                return null;

            horizontalVariation.DefinirComoRemovido(id);

            await this._horizontalVariationRepository.Update(horizontalVariation);

            var success = await this._horizontalVariationRepository.UnitOfWork.Commit();

            return success ? horizontalVariation : null;
        }

        public async Task<HorizontalVariation?> Get(int id)
        {
            return await this._horizontalVariationRepository.Get(id, false);
        }

        public async Task<List<HorizontalVariation>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._horizontalVariationRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._horizontalVariationRepository.GetAllIds();
        }

        public async Task<HorizontalVariation?> Update(HorizontalVariation horizontalVariation)
        {
            var oldHorizontalVariation = await this._horizontalVariationRepository.Get(horizontalVariation.Id, true);

            if (oldHorizontalVariation == null)
                return null;

            oldHorizontalVariation.MergeToUpdate(horizontalVariation);

            if(!oldHorizontalVariation.EhValido())
                return oldHorizontalVariation;

            await this._horizontalVariationRepository.Update(oldHorizontalVariation);

            var success = await this._horizontalVariationRepository.UnitOfWork.Commit();

            return success ? horizontalVariation : null;
        }
    }
}
