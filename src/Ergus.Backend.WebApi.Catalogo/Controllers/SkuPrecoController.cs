using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.SkuPrices.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("skupreco")]
    public class SkuPrecoController : ControllerBase
    {
        private readonly ISkuPriceService _advertisementSkuPriceService;

        public SkuPrecoController(ISkuPriceService advertisementSkuPriceService)
        {
            this._advertisementSkuPriceService = advertisementSkuPriceService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SkuPriceAddRequest request)
        {
            var advertisementSkuPrice = await this._advertisementSkuPriceService.Add(request.ToSkuPrice()!);

            if (advertisementSkuPrice == null || !advertisementSkuPrice.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar este dado", advertisementSkuPrice?.Erros));

            var response = new SkuPriceResponse(advertisementSkuPrice!);
            return new ApiResult(new Saida(true, "Dado adicionado com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var advertisementSkuPrice = await this._advertisementSkuPriceService.Delete(id);

            if (advertisementSkuPrice == null)
                return new ApiResult(new BadRequestApiResponse("Dado não encontrado"));

            var response = new SkuPriceResponse(advertisementSkuPrice);
            return new ApiResult(new Saida(true, "Dado removido com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var advertisementSkuPrice = await this._advertisementSkuPriceService.Get(id);

            if (advertisementSkuPrice == null)
                return new ApiResult(new BadRequestApiResponse("Dado não encontrado"));

            var response = new SkuPriceResponse(advertisementSkuPrice);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var advertisementSkuPriceAttrs = await this._advertisementSkuPriceService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = advertisementSkuPriceAttrs.ConvertAll(c => new SkuPriceResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._advertisementSkuPriceService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SkuPriceUpdateRequest? request)
        {
            var advertisementSkuPrice = await this._advertisementSkuPriceService.Update(request!.ToSkuPrice()!);

            if (advertisementSkuPrice == null || !advertisementSkuPrice.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar este dado", advertisementSkuPrice?.Erros));

            var response = new SkuPriceResponse(advertisementSkuPrice!);

            return new ApiResult(new Saida(true, "Dado atualizado com sucesso", response));
        }
    }
}