using FirstProject.Models;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;
using System.Text.Json;

namespace FirstProject.Extentions
{
    public static class GlobalExceptionHandlingExtention
    {
        public static void ConfigureExtention(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(appError =>
            {

                appError.Run(async context =>
                    {
                        await Console.Out.WriteLineAsync("Exception hadle is working");
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            Log.Error($"Error: {contextFeature.Error}");
                            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = $"Error: {contextFeature.Error}"
                            }));
                        }
                    });
            });
        }
    }
}
