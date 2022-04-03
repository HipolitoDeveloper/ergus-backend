using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.Providers.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("fornecedor")]
    public class FornecedorController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public FornecedorController(IProviderService providerService)
        {
            this._providerService = providerService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProviderAddRequest request)
        {
            var provider = await this._providerService.Add(request.ToProvider()!);

            if (provider == null || !provider.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar este fornecedor", provider?.Erros));

            var response = new ProviderResponse(provider!);
            return new ApiResult(new Saida(true, "Fornecedor adicionado com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var provider = await this._providerService.Delete(id);

            if (provider == null)
                return new ApiResult(new BadRequestApiResponse("Fornecedor não encontrado"));

            var response = new ProviderResponse(provider);
            return new ApiResult(new Saida(true, "Fornecedor removido com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var provider = await this._providerService.Get(id);

            if (provider == null)
                return new ApiResult(new BadRequestApiResponse("Fornecedor não encontrado"));

            var response = new ProviderResponse(provider);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var providers = await this._providerService.GetAll();

            var response = providers.ConvertAll(c => new ProviderResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProviderUpdateRequest? request)
        {
            var provider = await this._providerService.Update(request!.ToProvider()!);

            if (provider == null || !provider.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar este fornecedor", provider?.Erros));

            var response = new ProviderResponse(provider!);

            return new ApiResult(new Saida(true, "Fornecedor atualizado com sucesso", response));
        }
    }
}