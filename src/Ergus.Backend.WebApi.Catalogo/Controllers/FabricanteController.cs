using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Producers.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("fabricante")]
    public class FabricanteController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public FabricanteController(IProducerService producerService)
        {
            this._producerService = producerService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProducerAddRequest request)
        {
            var producer = await this._producerService.Add(request.ToProducer()!);

            if (producer == null || !producer.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar este fabricante", producer?.Erros));

            var response = new ProducerResponse(producer!);
            return new ApiResult(new Saida(true, "Fabricante adicionado com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await this._producerService.Delete(id);

            if (producer == null)
                return new ApiResult(new BadRequestApiResponse("Fabricante não encontrado"));

            var response = new ProducerResponse(producer);
            return new ApiResult(new Saida(true, "Fabricante removido com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var producer = await this._producerService.Get(id);

            if (producer == null)
                return new ApiResult(new BadRequestApiResponse("Fabricante não encontrado"));

            var response = new ProducerResponse(producer);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var producers = await this._producerService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = producers.ConvertAll(c => new ProducerResponse(c));
            return new ApiResult(new Saida(true, String.Empty, response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._producerService.GetAllIds();
            return new ApiResult(new Saida(true, String.Empty, ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProducerUpdateRequest? request)
        {
            var producer = await this._producerService.Update(request!.ToProducer()!);

            if (producer == null || !producer.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar este fabricante", producer?.Erros));

            var response = new ProducerResponse(producer!);

            return new ApiResult(new Saida(true, "Fabricante atualizado com sucesso", response));
        }
    }
}