using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Currencies.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("moeda")]
    public class MoedaController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public MoedaController(ICurrencyService currencyService)
        {
            this._currencyService = currencyService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CurrencyAddRequest request)
        {
            var currency = await this._currencyService.Add(request.ToCurrency()!);

            if (currency == null || !currency.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta moeda", currency?.Erros));

            var response = new CurrencyResponse(currency!);
            return new ApiResult(new Saida(true, "Moeda adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currency = await this._currencyService.Delete(id);

            if (currency == null)
                return new ApiResult(new BadRequestApiResponse("Moeda não encontrada"));

            var response = new CurrencyResponse(currency);
            return new ApiResult(new Saida(true, "Moeda removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var currency = await this._currencyService.Get(id);

            if (currency == null)
                return new ApiResult(new BadRequestApiResponse("Moeda não encontrada"));

            var response = new CurrencyResponse(currency);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var categories = await this._currencyService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = categories.ConvertAll(c => new CurrencyResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._currencyService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CurrencyUpdateRequest? request)
        {
            var currency = await this._currencyService.Update(request!.ToCurrency()!);

            if (currency == null || !currency.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta moeda", currency?.Erros));

            var response = new CurrencyResponse(currency!);

            return new ApiResult(new Saida(true, "Moeda atualizada com sucesso", response));
        }
    }
}