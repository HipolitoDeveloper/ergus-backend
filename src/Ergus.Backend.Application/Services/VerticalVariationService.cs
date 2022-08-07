using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IVerticalVariationService
    {
        Task<VerticalVariation?> Add(VerticalVariation verticalVariation);
        Task<VerticalVariation?> Delete(int id);
        Task<VerticalVariation?> Get(int id);
        Task<List<VerticalVariation>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<VerticalVariation?> Update(VerticalVariation verticalVariation);
    }

    internal class VerticalVariationService : IVerticalVariationService
    {
        private readonly IVerticalVariationRepository _verticalVariationRepository;

        public VerticalVariationService(IVerticalVariationRepository verticalVariationRepository)
        {
            this._verticalVariationRepository = verticalVariationRepository;
        }

        public async Task<VerticalVariation?> Add(VerticalVariation verticalVariation)
        {
            if (!verticalVariation.EhValido())
                return verticalVariation;

            await this._verticalVariationRepository.Add(verticalVariation);

            var success = await this._verticalVariationRepository.UnitOfWork.Commit();

            return success ? verticalVariation : null;
        }

        public async Task<VerticalVariation?> Delete(int id)
        {
            var verticalVariation = await this._verticalVariationRepository.Get(id, true);

            if (verticalVariation == null)
                return null;

            verticalVariation.DefinirComoRemovido(id);

            await this._verticalVariationRepository.Update(verticalVariation);

            var success = await this._verticalVariationRepository.UnitOfWork.Commit();

            return success ? verticalVariation : null;
        }

        public async Task<VerticalVariation?> Get(int id)
        {
            return await this._verticalVariationRepository.Get(id, false);
        }

        public async Task<List<VerticalVariation>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._verticalVariationRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._verticalVariationRepository.GetAllIds();
        }

        public async Task<VerticalVariation?> Update(VerticalVariation verticalVariation)
        {
            var oldVerticalVariation = await this._verticalVariationRepository.Get(verticalVariation.Id, true);

            if (oldVerticalVariation == null)
                return null;

            oldVerticalVariation.MergeToUpdate(verticalVariation);

            if(!oldVerticalVariation.EhValido())
                return oldVerticalVariation;

            await this._verticalVariationRepository.Update(oldVerticalVariation);

            var success = await this._verticalVariationRepository.UnitOfWork.Commit();

            return success ? verticalVariation : null;
        }
    }
}
