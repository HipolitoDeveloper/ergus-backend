using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.AdvertisementSkuPrices.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("anunciosku")]
    public class AnuncioSkuPrecoController : ControllerBase
    {
        private readonly IAdvertisementSkuPriceService _advertisementSkuPriceService;

        public AnuncioSkuPrecoController(IAdvertisementSkuPriceService advertisementSkuPriceService)
        {
            this._advertisementSkuPriceService = advertisementSkuPriceService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AdvertisementSkuPriceAddRequest request)
        {
            var advertisementSkuPrice = await this._advertisementSkuPriceService.Add(request.ToAdvertisementSkuPrice()!);

            if (advertisementSkuPrice == null || !advertisementSkuPrice.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar este dado", advertisementSkuPrice?.Erros));

            var response = new AdvertisementSkuPriceResponse(advertisementSkuPrice!);
            return new ApiResult(new Saida(true, "Dado adicionado com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var advertisementSkuPrice = await this._advertisementSkuPriceService.Delete(id);

            if (advertisementSkuPrice == null)
                return new ApiResult(new BadRequestApiResponse("Dado não encontrado"));

            var response = new AdvertisementSkuPriceResponse(advertisementSkuPrice);
            return new ApiResult(new Saida(true, "Dado removido com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var advertisementSkuPrice = await this._advertisementSkuPriceService.Get(id);

            if (advertisementSkuPrice == null)
                return new ApiResult(new BadRequestApiResponse("Dado não encontrado"));

            var response = new AdvertisementSkuPriceResponse(advertisementSkuPrice);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var advertisementSkuPriceAttrs = await this._advertisementSkuPriceService.GetAll();

            var response = advertisementSkuPriceAttrs.ConvertAll(c => new AdvertisementSkuPriceResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AdvertisementSkuPriceUpdateRequest? request)
        {
            var advertisementSkuPrice = await this._advertisementSkuPriceService.Update(request!.ToAdvertisementSkuPrice()!);

            if (advertisementSkuPrice == null || !advertisementSkuPrice.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar este dado", advertisementSkuPrice?.Erros));

            var response = new AdvertisementSkuPriceResponse(advertisementSkuPrice!);

            return new ApiResult(new Saida(true, "Dado atualizado com sucesso", response));
        }
    }
}