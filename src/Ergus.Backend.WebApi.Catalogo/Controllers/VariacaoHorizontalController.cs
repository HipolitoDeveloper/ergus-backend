using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.HorizontalVariations.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("variacao-horizontal")]
    public class VariacaoHorizontalController : ControllerBase
    {
        private readonly IHorizontalVariationService _horizontalVariationService;

        public VariacaoHorizontalController(IHorizontalVariationService horizontalVariationService)
        {
            this._horizontalVariationService = horizontalVariationService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] HorizontalVariationAddRequest request)
        {
            var horizontalVariation = await this._horizontalVariationService.Add(request.ToHorizontalVariation()!);

            if (horizontalVariation == null || !horizontalVariation.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta variação horizontal", horizontalVariation?.Erros));

            var response = new HorizontalVariationResponse(horizontalVariation!);
            return new ApiResult(new Saida(true, "Variação Horizontal adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var horizontalVariation = await this._horizontalVariationService.Delete(id);

            if (horizontalVariation == null)
                return new ApiResult(new BadRequestApiResponse("Variação Horizontal não encontrada"));

            var response = new HorizontalVariationResponse(horizontalVariation);
            return new ApiResult(new Saida(true, "Variação Horizontal removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var horizontalVariation = await this._horizontalVariationService.Get(id);

            if (horizontalVariation == null)
                return new ApiResult(new BadRequestApiResponse("Variação Horizontal não encontrada"));

            var response = new HorizontalVariationResponse(horizontalVariation);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var categories = await this._horizontalVariationService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = categories.ConvertAll(c => new HorizontalVariationResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._horizontalVariationService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] HorizontalVariationUpdateRequest? request)
        {
            var horizontalVariation = await this._horizontalVariationService.Update(request!.ToHorizontalVariation()!);

            if (horizontalVariation == null || !horizontalVariation.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta variação horizontal", horizontalVariation?.Erros));

            var response = new HorizontalVariationResponse(horizontalVariation!);

            return new ApiResult(new Saida(true, "Variação Horizontal atualizada com sucesso", response));
        }
    }
}