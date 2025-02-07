using FalconOne.API;
using FalconOne.API.Config;
using FalconOne.Middleware;
using FalconOne.Security;

var cancellationToken = new CancellationTokenSource().Token;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

FalconOneConfiguration.Register(builder, cancellationToken);

WebApplication app = builder.Build();

FalconOneConfiguration.Bootstrap(app.Services);

FalconOneConfiguration.Seed(app.Services, cancellationToken);

if (app.Environment.IsDevelopment())
{
}
    app.UseDeveloperExceptionPage();


app.UseRateLimiter();

app.UseSwagger();

app.UseHealthChecks("/status");

app.UseSwaggerUI(o =>
{
    o.DefaultModelsExpandDepth(-1);
    o.DisplayRequestDuration();
    o.DocumentTitle = "FalconOne APIs";
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(CORSPolicy.ReactAppPolicy);

app.UseAuthentication();
app.UseAuthorization();

HubsConfig.Configure(app);
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.MapControllers();

await app.RunAsync(cancellationToken);

