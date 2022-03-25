using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Products.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProdutoController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductAddRequest request)
        {
            var product = await this._productService.Add(request.ToProduct()!);

            if (product == null || !product.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar este produto", product?.Erros));

            var response = new ProductResponse(product!);
            return new ApiResult(new Saida(true, "Produto adicionado com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await this._productService.Delete(id);

            if (product == null)
                return new ApiResult(new BadRequestApiResponse("Produto não encontrado"));

            var response = new ProductResponse(product);
            return new ApiResult(new Saida(true, "Produto removido com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await this._productService.Get(id);

            if (product == null)
                return new ApiResult(new BadRequestApiResponse("Produto não encontrado"));

            var response = new ProductResponse(product);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await this._productService.GetAll();

            var response = products.ConvertAll(c => new ProductResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest? request)
        {
            var product = await this._productService.Update(request!.ToProduct()!);

            if (product == null || !product.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar este produto", product?.Erros));

            var response = new ProductResponse(product!);

            return new ApiResult(new Saida(true, "Produto atualizado com sucesso", response));
        }
    }
}