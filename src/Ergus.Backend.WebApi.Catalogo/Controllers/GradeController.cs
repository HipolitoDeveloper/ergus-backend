using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Grids.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("grade")]
    public class GradeController : ControllerBase
    {
        private readonly IGridService _gridService;

        public GradeController(IGridService gridService)
        {
            this._gridService = gridService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GridAddRequest request)
        {
            var grid = await this._gridService.Add(request.ToGrid()!);

            if (grid == null || !grid.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta grade", grid?.Erros));

            var response = new GridResponse(grid!);
            return new ApiResult(new Saida(true, "Grade adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var grid = await this._gridService.Delete(id);

            if (grid == null)
                return new ApiResult(new BadRequestApiResponse("Grade não encontrada"));

            var response = new GridResponse(grid);
            return new ApiResult(new Saida(true, "Grade removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var grid = await this._gridService.Get(id);

            if (grid == null)
                return new ApiResult(new BadRequestApiResponse("Grade não encontrada"));

            var response = new GridResponse(grid);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var categories = await this._gridService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = categories.ConvertAll(c => new GridResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._gridService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] GridUpdateRequest? request)
        {
            var grid = await this._gridService.Update(request!.ToGrid()!);

            if (grid == null || !grid.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta grade", grid?.Erros));

            var response = new GridResponse(grid!);

            return new ApiResult(new Saida(true, "Grade atualizada com sucesso", response));
        }
    }
}