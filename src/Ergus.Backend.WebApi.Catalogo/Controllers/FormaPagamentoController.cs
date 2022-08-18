using Ergus.Backend.Application.Services;
using Ergus.Backend.Core.Domain;
using Ergus.Backend.WebApi.Catalogo.Helpers;
using Ergus.Backend.WebApi.Catalogo.Models;
using Ergus.Backend.WebApi.Catalogo.Models.PaymentForms.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergus.Backend.WebApi.Catalogo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("forma-pagamento")]
    public class FormaPagamentoController : ControllerBase
    {
        private readonly IPaymentFormService _paymentFormService;

        public FormaPagamentoController(IPaymentFormService paymentFormService)
        {
            this._paymentFormService = paymentFormService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PaymentFormAddRequest request)
        {
            var paymentForm = await this._paymentFormService.Add(request.ToPaymentForm()!);

            if (paymentForm == null || !paymentForm.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar adicionar esta forma de pagamento", paymentForm?.Erros));

            var response = new PaymentFormResponse(paymentForm!);
            return new ApiResult(new Saida(true, "Forma de Pagamento adicionada com sucesso", response));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paymentForm = await this._paymentFormService.Delete(id);

            if (paymentForm == null)
                return new ApiResult(new BadRequestApiResponse("Forma de Pagamento não encontrada"));

            var response = new PaymentFormResponse(paymentForm);
            return new ApiResult(new Saida(true, "Forma de Pagamento removida com sucesso", response));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var paymentForm = await this._paymentFormService.Get(id);

            if (paymentForm == null)
                return new ApiResult(new BadRequestApiResponse("Forma de Pagamento não encontrada"));

            var response = new PaymentFormResponse(paymentForm);
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest paginationRequest)
        {
            var categories = await this._paymentFormService.GetAll(paginationRequest.Page, paginationRequest.PageSize, paginationRequest.DisablePagination);

            var response = categories.ConvertAll(c => new PaymentFormResponse(c));
            return new ApiResult(new Saida(true, new List<string>(), response));
        }

        [HttpGet]
        [Route("get-ids")]
        public async Task<IActionResult> GetAllIds()
        {
            var ids = await this._paymentFormService.GetAllIds();
            return new ApiResult(new Saida(true, new List<string>(), ids));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PaymentFormUpdateRequest? request)
        {
            var paymentForm = await this._paymentFormService.Update(request!.ToPaymentForm()!);

            if (paymentForm == null || !paymentForm.ValidationResult.IsValid)
                return new ApiResult(new BadRequestApiResponse("Houve um problema ao tentar atualizar esta forma de pagamento", paymentForm?.Erros));

            var response = new PaymentFormResponse(paymentForm!);

            return new ApiResult(new Saida(true, "Forma de Pagamento atualizada com sucesso", response));
        }
    }
}