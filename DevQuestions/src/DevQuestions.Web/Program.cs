using DevQuestions.Infrastructure.Postgresql;
using DevQuestions.Web;
using DevQuestions.Web.Middlewares;
using DevQuestions.Web.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies();

var app = builder.Build();
app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "DevQuestions.Web v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();