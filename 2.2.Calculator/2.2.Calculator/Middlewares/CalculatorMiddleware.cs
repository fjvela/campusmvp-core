using Calculator.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Calculator.Middlewares
{
    public class CalculatorMiddleware
    {
        private readonly string basePath;
        private readonly ICalculatorService calculatorServices;
        private readonly RequestDelegate next;

        public CalculatorMiddleware(string basePath,
                                    RequestDelegate next,
                                    ICalculatorService calculatorServices)
        {
            this.basePath = basePath;
            this.next = next;
            this.calculatorServices = calculatorServices;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(basePath))
            {
                if (context.Request.Path.StartsWithSegments($"{basePath}/results"))
                {
                    await SendCalculationResults(context);
                }
                else if (context.Request.Path.Value == basePath)
                {
                    await SendCalculatorHomePage(context);
                }
                else
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 404;
                }
            }
            else await next(context);
        }

        private async Task SendCalculationResults(HttpContext context)
        {
            if (int.TryParse(context.Request.Form["a"], out int a) &&
                int.TryParse(context.Request.Form["b"], out int b))
            {
                string operation = context.Request.Form["operation"];

                var result = calculatorServices.Calculate(a, b, operation);
                await SendHtmlPage(
                    context,
                    "Results",
                    $@"<h2>{a}{operation}{b}={result}</h2>
                    <p><a href='{basePath}'>Back</a></p>"
                );
            }
            else await SendHtmlPage(context, 
                "Error", 
                $"Some values aren't valid {context.Request.Form["a"]} {context.Request.Form["b"]}");
        }

        private async Task SendCalculatorHomePage(HttpContext context)
        {
            await SendHtmlPage(
                context,
                "Start",
                $@" <form method='post' action='{basePath}/results'>
                <input type='number' name='a'>
                <select name='operation'>
                    <option value='+'>+</option>
                    <option value='-'>-</option>
                    <option value='*'>*</option>
                    <option value='/'>/</option>
                </select>
                <input type='number' name='b'>
                <input type='submit' value='Calculate'>
            </form>
        ");
        }

        private async Task SendHtmlPage(HttpContext context, string title, string body)
        {
            var content = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8' />
                    <title>{title} - Awesome calculator</title>
                    <link href='/styles/calculator.css' rel='stylesheet' />
                </head>
                <body>
                    <h1>
                        Awesome calculator
                    </h1>
                    {body}
                </body>
                </html>";
            context.Response.Clear();
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(content);
        }
    }
}