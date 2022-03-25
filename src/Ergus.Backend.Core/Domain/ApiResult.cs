using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ergus.Backend.Core.Domain
{
    /// <summary>
    /// Resultado padrão para todas as rotas da API
    /// </summary>
    public class ApiResult : IActionResult
    {
        private readonly Saida _saida;

        public ApiResult(Saida saida)
        {
            _saida = saida;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(_saida)
            {
                StatusCode = !_saida.Sucesso
                    ? (int)HttpStatusCode.BadRequest
                    : (int)HttpStatusCode.OK
            };

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
