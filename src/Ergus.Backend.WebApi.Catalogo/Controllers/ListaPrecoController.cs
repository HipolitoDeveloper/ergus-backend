using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.PriceLists.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("listapreco")]
    public class ListaPrecoController : ControllerBase
    {
        private readonly IPriceListService _priceListService;

        public ListaPrecoController(IPriceListService priceListService)
        {
            this._priceListService = priceListService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PriceListAddRequest request)
        {
            var priceList = await this._priceListService.Add(request.ToPriceList()!);

            if (priceList == null || !priceList.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta lista de preço", priceList?.Erros));

            var response = new PriceListResponse(priceList!);
            return new ApiResult(new Saida(true, "Lista de preço adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var priceList = await this._priceListService.Delete(id);

            if (priceList == null)
                return new ApiResult(new BadRequestApiResponse("Lista de preço não encontrada"));

            var response = new PriceListResponse(priceList);
            return new ApiResult(new Saida(true, "Lista de preço removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var priceList = await this._priceListService.Get(id);

            if (priceList == null)
                return new ApiResult(new BadRequestApiResponse("Lista de preço não encontrada"));

            var response = new PriceListResponse(priceList);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var priceLists = await this._priceListService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = priceLists.ConvertAll(c => new PriceListResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._priceListService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PriceListUpdateRequest? request)
        {
            var priceList = await this._priceListService.Update(request!.ToPriceList()!);

            if (priceList == null || !priceList.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta lista de preço", priceList?.Erros));

            var response = new PriceListResponse(priceList!);

            return new ApiResult(new Saida(true, "Lista de preço atualizada com sucesso", response));
        }
    }
}