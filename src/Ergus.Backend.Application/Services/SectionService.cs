using Ergus.Backend.Infrastructure.Models;
using Ergus.Backend.Infrastructure.Repositories;

namespace Ergus.Backend.Application.Services
{
    public interface ISectionService
    {
        Task<Section?> Add(Section section);
        Task<Section?> Delete(int id);
        Task<Section?> Get(int id);
        Task<List<Section>> GetAll(int page, int pageSize, bool disablePagination = false);
        Task<List<int>> GetAllIds();
        Task<Section?> Update(Section section);
    }

    internal class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            this._sectionRepository = sectionRepository;
        }

        public async Task<Section?> Add(Section section)
        {
            if (!section.EhValido())
                return section;

            await this._sectionRepository.Add(section);

            var success = await this._sectionRepository.UnitOfWork.Commit();

            return success ? section : null;
        }

        public async Task<Section?> Delete(int id)
        {
            var section = await this._sectionRepository.Get(id, true);

            if (section == null)
                return null;

            section.DefinirComoRemovido(id);

            await this._sectionRepository.Update(section);

            var success = await this._sectionRepository.UnitOfWork.Commit();

            return success ? section : null;
        }

        public async Task<Section?> Get(int id)
        {
            return await this._sectionRepository.Get(id, false);
        }

        public async Task<List<Section>> GetAll(int page, int pageSize, bool disablePagination = false)
        {
            return await this._sectionRepository.GetAll(page, pageSize, disablePagination);
        }

        public async Task<List<int>> GetAllIds()
        {
            return await this._sectionRepository.GetAllIds();
        }

        public async Task<Section?> Update(Section section)
        {
            var oldSection = await this._sectionRepository.Get(section.Id, true);

            if (oldSection == null)
                return null;

            oldSection.MergeToUpdate(section);

            if(!oldSection.EhValido())
                return oldSection;

            await this._sectionRepository.Update(oldSection);

            var success = await this._sectionRepository.UnitOfWork.Commit();

            return success ? section : null;
        }
    }
}
