using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.StockUnits.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("unidade-estoque")]
    public class UnidadeEstoqueController : ControllerBase
    {
        private readonly IStockUnitService _stockUnitService;

        public UnidadeEstoqueController(IStockUnitService stockUnitService)
        {
            this._stockUnitService = stockUnitService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StockUnitAddRequest request)
        {
            var stockUnit = await this._stockUnitService.Add(request.ToStockUnit()!);

            if (stockUnit == null || !stockUnit.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta grade", stockUnit?.Erros));

            var response = new StockUnitResponse(stockUnit!);
            return new ApiResult(new Saida(true, "Grade adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stockUnit = await this._stockUnitService.Delete(id);

            if (stockUnit == null)
                return new ApiResult(new BadRequestApiResponse("Grade não encontrada"));

            var response = new StockUnitResponse(stockUnit);
            return new ApiResult(new Saida(true, "Grade removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var stockUnit = await this._stockUnitService.Get(id);

            if (stockUnit == null)
                return new ApiResult(new BadRequestApiResponse("Grade não encontrada"));

            var response = new StockUnitResponse(stockUnit);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var categories = await this._stockUnitService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = categories.ConvertAll(c => new StockUnitResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._stockUnitService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StockUnitUpdateRequest? request)
        {
            var stockUnit = await this._stockUnitService.Update(request!.ToStockUnit()!);

            if (stockUnit == null || !stockUnit.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta grade", stockUnit?.Erros));

            var response = new StockUnitResponse(stockUnit!);

            return new ApiResult(new Saida(true, "Grade atualizada com sucesso", response));
        }
    }
}