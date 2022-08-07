using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.VerticalVariations.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("variacao-vertical")]
    public class VariacaoVerticalController : ControllerBase
    {
        private readonly IVerticalVariationService _verticalVariationService;

        public VariacaoVerticalController(IVerticalVariationService verticalVariationService)
        {
            this._verticalVariationService = verticalVariationService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VerticalVariationAddRequest request)
        {
            var verticalVariation = await this._verticalVariationService.Add(request.ToVerticalVariation()!);

            if (verticalVariation == null || !verticalVariation.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta variação vertical", verticalVariation?.Erros));

            var response = new VerticalVariationResponse(verticalVariation!);
            return new ApiResult(new Saida(true, "Variação Vertical adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var verticalVariation = await this._verticalVariationService.Delete(id);

            if (verticalVariation == null)
                return new ApiResult(new BadRequestApiResponse("Variação Vertical não encontrada"));

            var response = new VerticalVariationResponse(verticalVariation);
            return new ApiResult(new Saida(true, "Variação Vertical removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var verticalVariation = await this._verticalVariationService.Get(id);

            if (verticalVariation == null)
                return new ApiResult(new BadRequestApiResponse("Variação Vertical não encontrada"));

            var response = new VerticalVariationResponse(verticalVariation);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var categories = await this._verticalVariationService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = categories.ConvertAll(c => new VerticalVariationResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._verticalVariationService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] VerticalVariationUpdateRequest? request)
        {
            var verticalVariation = await this._verticalVariationService.Update(request!.ToVerticalVariation()!);

            if (verticalVariation == null || !verticalVariation.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta variação vertical", verticalVariation?.Erros));

            var response = new VerticalVariationResponse(verticalVariation!);

            return new ApiResult(new Saida(true, "Variação Vertical atualizada com sucesso", response));
        }
    }
}