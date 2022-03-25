using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Advertisements.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("anuncio")]
    public class AnuncioController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;

        public AnuncioController(IAdvertisementService advertisementService)
        {
            this._advertisementService = advertisementService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AdvertisementAddRequest request)
        {
            var advertisement = await this._advertisementService.Add(request.ToAdvertisement()!);

            if (advertisement == null || !advertisement.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar este dado", advertisement?.Erros));

            var response = new AdvertisementResponse(advertisement!);
            return new ApiResult(new Saida(true, "Dado adicionado com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var advertisement = await this._advertisementService.Delete(id);

            if (advertisement == null)
                return new ApiResult(new BadRequestApiResponse("Dado não encontrado"));

            var response = new AdvertisementResponse(advertisement);
            return new ApiResult(new Saida(true, "Dado removido com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var advertisement = await this._advertisementService.Get(id);

            if (advertisement == null)
                return new ApiResult(new BadRequestApiResponse("Dado não encontrado"));

            var response = new AdvertisementResponse(advertisement);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var advertisementAttrs = await this._advertisementService.GetAll();

            var response = advertisementAttrs.ConvertAll(c => new AdvertisementResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AdvertisementUpdateRequest? request)
        {
            var advertisement = await this._advertisementService.Update(request!.ToAdvertisement()!);

            if (advertisement == null || !advertisement.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar este dado", advertisement?.Erros));

            var response = new AdvertisementResponse(advertisement!);

            return new ApiResult(new Saida(true, "Dado atualizado com sucesso", response));
        }
    }
}