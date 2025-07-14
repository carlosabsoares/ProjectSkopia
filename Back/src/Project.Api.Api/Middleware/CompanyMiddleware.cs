using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Project.Api.Api.Middleware
{
    public class CompanyMiddleware
    {
        private readonly RequestDelegate _next;

        public CompanyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Verifica se a rota requer autenticação
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }

            // Valida empresa do usuário
            var empresaId = context.User?.FindFirstValue("company_uuid");
            if (string.IsNullOrEmpty(empresaId))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Company not found");
                return;
            }

            await _next(context);
        }
    }
}