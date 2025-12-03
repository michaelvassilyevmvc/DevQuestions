using Framework;
using Tags;
using Web;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies();
builder.Services.AddEndpoints(TagsAssembly.Assembly);

var app = builder.Build();
app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Web v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapEndpoints();


app.Run();