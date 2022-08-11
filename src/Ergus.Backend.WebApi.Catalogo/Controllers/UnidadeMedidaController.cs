using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.UnitOfMeasures.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("unidade-medida")]
    public class UnidadeMedidaController : ControllerBase
    {
        private readonly IUnitOfMeasureService _unitOfMeasureService;

        public UnidadeMedidaController(IUnitOfMeasureService unitOfMeasureService)
        {
            this._unitOfMeasureService = unitOfMeasureService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UnitOfMeasureAddRequest request)
        {
            var unitOfMeasure = await this._unitOfMeasureService.Add(request.ToUnitOfMeasure()!);

            if (unitOfMeasure == null || !unitOfMeasure.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta unidade de medida", unitOfMeasure?.Erros));

            var response = new UnitOfMeasureResponse(unitOfMeasure!);
            return new ApiResult(new Saida(true, "Unidade de Medida adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var unitOfMeasure = await this._unitOfMeasureService.Delete(id);

            if (unitOfMeasure == null)
                return new ApiResult(new BadRequestApiResponse("Unidade de Medida não encontrada"));

            var response = new UnitOfMeasureResponse(unitOfMeasure);
            return new ApiResult(new Saida(true, "Unidade de Medida removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var unitOfMeasure = await this._unitOfMeasureService.Get(id);

            if (unitOfMeasure == null)
                return new ApiResult(new BadRequestApiResponse("Unidade de Medida não encontrada"));

            var response = new UnitOfMeasureResponse(unitOfMeasure);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var categories = await this._unitOfMeasureService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = categories.ConvertAll(c => new UnitOfMeasureResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._unitOfMeasureService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UnitOfMeasureUpdateRequest? request)
        {
            var unitOfMeasure = await this._unitOfMeasureService.Update(request!.ToUnitOfMeasure()!);

            if (unitOfMeasure == null || !unitOfMeasure.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta unidade de medida", unitOfMeasure?.Erros));

            var response = new UnitOfMeasureResponse(unitOfMeasure!);

            return new ApiResult(new Saida(true, "Unidade de Medida atualizada com sucesso", response));
        }
    }
}