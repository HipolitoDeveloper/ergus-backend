using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Skus.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("sku")]
    public class SkuController : ControllerBase
    {
        private readonly ISkuService _skuService;

        public SkuController(ISkuService skuService)
        {
            this._skuService = skuService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SkuAddRequest request)
        {
            var sku = await this._skuService.Add(request.ToSku()!);

            if (sku == null || !sku.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar este sku", sku?.Erros));

            var response = new SkuResponse(sku!);
            return new ApiResult(new Saida(true, "Sku adicionado com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sku = await this._skuService.Delete(id);

            if (sku == null)
                return new ApiResult(new BadRequestApiResponse("Sku não encontrado"));

            var response = new SkuResponse(sku);
            return new ApiResult(new Saida(true, "Sku removido com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sku = await this._skuService.Get(id);

            if (sku == null)
                return new ApiResult(new BadRequestApiResponse("Sku não encontrado"));

            var response = new SkuResponse(sku);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var skus = await this._skuService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = skus.ConvertAll(c => new SkuResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._skuService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SkuUpdateRequest? request)
        {
            var sku = await this._skuService.Update(request!.ToSku()!);

            if (sku == null || !sku.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar este sku", sku?.Erros));

            var response = new SkuResponse(sku!);

            return new ApiResult(new Saida(true, "Sku atualizado com sucesso", response));
        }
    }
}