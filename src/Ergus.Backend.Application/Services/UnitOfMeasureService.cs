using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface IUnitOfMeasureService
    {
        Task<UnitOfMeasure?> Add(UnitOfMeasure unitOfMeasure);
        Task<UnitOfMeasure?> Delete(int id);
        Task<UnitOfMeasure?> Get(int id);
        Task<List<UnitOfMeasure>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<UnitOfMeasure?> Update(UnitOfMeasure unitOfMeasure);
    }

    internal class UnitOfMeasureService : IUnitOfMeasureService
    {
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;

        public UnitOfMeasureService(IUnitOfMeasureRepository unitOfMeasureRepository)
        {
            this._unitOfMeasureRepository = unitOfMeasureRepository;
        }

        public async Task<UnitOfMeasure?> Add(UnitOfMeasure unitOfMeasure)
        {
            if (!unitOfMeasure.EhValido())
                return unitOfMeasure;

            await this._unitOfMeasureRepository.Add(unitOfMeasure);

            var success = await this._unitOfMeasureRepository.UnitOfWork.Commit();

            return success ? unitOfMeasure : null;
        }

        public async Task<UnitOfMeasure?> Delete(int id)
        {
            var unitOfMeasure = await this._unitOfMeasureRepository.Get(id, true);

            if (unitOfMeasure == null)
                return null;

            unitOfMeasure.DefinirComoRemovido(id);

            await this._unitOfMeasureRepository.Update(unitOfMeasure);

            var success = await this._unitOfMeasureRepository.UnitOfWork.Commit();

            return success ? unitOfMeasure : null;
        }

        public async Task<UnitOfMeasure?> Get(int id)
        {
            return await this._unitOfMeasureRepository.Get(id, false);
        }

        public async Task<List<UnitOfMeasure>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._unitOfMeasureRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._unitOfMeasureRepository.GetAllIds();
        }

        public async Task<UnitOfMeasure?> Update(UnitOfMeasure unitOfMeasure)
        {
            var oldUnitOfMeasure = await this._unitOfMeasureRepository.Get(unitOfMeasure.Id, true);

            if (oldUnitOfMeasure == null)
                return null;

            oldUnitOfMeasure.MergeToUpdate(unitOfMeasure);

            if(!oldUnitOfMeasure.EhValido())
                return oldUnitOfMeasure;

            await this._unitOfMeasureRepository.Update(oldUnitOfMeasure);

            var success = await this._unitOfMeasureRepository.UnitOfWork.Commit();

            return success ? unitOfMeasure : null;
        }
    }
}
