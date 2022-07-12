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
    [Route("categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriaController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryAddRequest request)
        {
            var category = await this._categoryService.Add(request.ToCategory()!);

            if (category == null || !category.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta categoria", category?.Erros));

            var response = new CategoryResponse(category!);
            return new ApiResult(new Saida(true, "Categoria adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await this._categoryService.Delete(id);

            if (category == null)
                return new ApiResult(new BadRequestApiResponse("Categoria não encontrada"));

            var response = new CategoryResponse(category);
            return new ApiResult(new Saida(true, "Categoria removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await this._categoryService.Get(id);

            if (category == null)
                return new ApiResult(new BadRequestApiResponse("Categoria não encontrada"));

            var response = new CategoryResponse(category);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var categories = await this._categoryService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = categories.ConvertAll(c => new CategoryResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var categoryIds = await this._categoryService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), categoryIds));
        }

        [HttpGet]
        [Route("get-tree")]
        public async Task<IActionResult> GetCategoryTree()
        {
            var categoryTree= await this._categoryService.GetTree();
            return new ApiResult(new Saida(true, new List<string>(), categoryTree.ConvertAll(c => new CategoryTreeResponse(c))));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateRequest? request)
        {
            var category = await this._categoryService.Update(request!.ToCategory()!);

            if (category == null || !category.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta categoria", category?.Erros));

            var response = new CategoryResponse(category!);

            return new ApiResult(new Saida(true, "Categoria atualizada com sucesso", response));
        }
    }
}