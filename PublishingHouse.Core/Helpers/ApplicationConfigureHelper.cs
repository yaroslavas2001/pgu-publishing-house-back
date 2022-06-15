using PublishingHouse.Interfaces.Constants;
using PublishingHouse.Middlewares;

namespace PublishingHouse.Helpers;

public static class ApplicationConfigureHelper
{
	public static WebApplication ConfigureApplication(this WebApplication app)
	{
		app.UseMiddleware<ExceptionMiddleware>();
		app.UseSpaStaticFiles();
		app.UseDeveloperExceptionPage();
		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseDefaultFiles();
		app.UseStaticFiles();
		app.UseCors(CommonConstants.MyAllowSpecificOrigins);
		app.UseSwagger(x => { x.SerializeAsV2 = true; });
		app.UseSwaggerUI(x =>
		{
			x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			//x.RoutePrefix = "atashol";
		});

		app.UseCors(x =>
		{
			x.AllowAnyHeader();
			x.AllowAnyMethod();
			x.AllowAnyOrigin();
		});

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();

			// Expose Swagger/OpenAPI JSON in new (v3) and old (v2) formats
			endpoints.MapSwagger("swagger/{documentName}/swagger.json");
			endpoints.MapSwagger("swagger/{documentName}/swaggerv2.json", c => { c.SerializeAsV2 = true; });
		});

		app.UseSpa(x => x.Options.SourcePath = "ClientApp");

		return app;
	}
}