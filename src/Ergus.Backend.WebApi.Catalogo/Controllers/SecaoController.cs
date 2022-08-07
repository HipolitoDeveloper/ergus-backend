using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Sections.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("secao")]
    public class SecaoController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SecaoController(ISectionService sectionService)
        {
            this._sectionService = sectionService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SectionAddRequest request)
        {
            var section = await this._sectionService.Add(request.ToSection()!);

            if (section == null || !section.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta seção", section?.Erros));

            var response = new SectionResponse(section!);
            return new ApiResult(new Saida(true, "Seção adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var section = await this._sectionService.Delete(id);

            if (section == null)
                return new ApiResult(new BadRequestApiResponse("Seção não encontrada"));

            var response = new SectionResponse(section);
            return new ApiResult(new Saida(true, "Seção removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var section = await this._sectionService.Get(id);

            if (section == null)
                return new ApiResult(new BadRequestApiResponse("Seção não encontrada"));

            var response = new SectionResponse(section);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var categories = await this._sectionService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = categories.ConvertAll(c => new SectionResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._sectionService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SectionUpdateRequest? request)
        {
            var section = await this._sectionService.Update(request!.ToSection()!);

            if (section == null || !section.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta seção", section?.Erros));

            var response = new SectionResponse(section!);

            return new ApiResult(new Saida(true, "Seção atualizada com sucesso", response));
        }
    }
}