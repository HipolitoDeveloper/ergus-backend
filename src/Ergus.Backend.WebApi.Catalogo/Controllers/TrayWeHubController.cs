using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Categories.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("traywehub")]
    public class TrayWeHubController : ControllerBase
    {
        private readonly IProductAttributeService _productAttributeService;

        public TrayWeHubController(IProductAttributeService productAttributeService)
        {
            this._productAttributeService = productAttributeService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductAttributeAddRequest request)
        {
            var productAttribute = await this._productAttributeService.Add(request.ToProductAttribute()!);

            if (productAttribute == null || !productAttribute.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar este dado", productAttribute?.Erros));

            var response = new ProductAttributeResponse(productAttribute!);
            return new ApiResult(new Saida(true, "Dado adicionado com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productAttribute = await this._productAttributeService.Delete(id);

            if (productAttribute == null)
                return new ApiResult(new BadRequestApiResponse("Dado não encontrado"));

            var response = new ProductAttributeResponse(productAttribute);
            return new ApiResult(new Saida(true, "Dado removido com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var productAttribute = await this._productAttributeService.Get(id);

            if (productAttribute == null)
                return new ApiResult(new BadRequestApiResponse("Dado não encontrado"));

            var response = new ProductAttributeResponse(productAttribute);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var productAttrs = await this._productAttributeService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = productAttrs.ConvertAll(c => new ProductAttributeResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._productAttributeService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductAttributeUpdateRequest? request)
        {
            var productAttribute = await this._productAttributeService.Update(request!.ToProductAttribute()!);

            if (productAttribute == null || !productAttribute.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar este dado", productAttribute?.Erros));

            var response = new ProductAttributeResponse(productAttribute!);

            return new ApiResult(new Saida(true, "Dado atualizado com sucesso", response));
        }
    }
}