using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PublishingHouse.Data;
using PublishingHouse.Interfaces;
using PublishingHouse.Interfaces.Constants;
using PublishingHouse.Services;

namespace PublishingHouse.Helpers;

internal static class DependenciesHelper
{
	internal static IServiceCollection InjectDependencies(this IServiceCollection serviceCollection,
		IConfiguration configuration)
	{
		serviceCollection.AddCors(options =>
		{
			options.AddPolicy(CommonConstants.MyAllowSpecificOrigins,
				builder =>
					builder.WithOrigins("http://192.168.0.102:5051",
							"http://31.31.24.200:5051",
							"http://localhost:8081",
							"http://192.168.0.107:8080",
							"http://192.168.0.107:8081",
							"http://localhost:8080")
						.AllowAnyHeader()
						.AllowAnyMethod());
		});

		serviceCollection.AddServices();
		serviceCollection.AddCustomSwagger();
		serviceCollection.ConfigureAuthentication();
		serviceCollection.AddControllers();
		serviceCollection.AddCors();
		serviceCollection.AddSpaStaticFiles(options => options.RootPath = "ClientApp/dist");

		serviceCollection.AddDbContext<DataContext>(options =>
			options.UseSqlServer(configuration.GetConnectionString("Default"),
				sqliteDbContextOptionsBuilder =>
					sqliteDbContextOptionsBuilder.MigrationsAssembly($"{nameof(PublishingHouse)}.{nameof(PublishingHouse.Data)}")));

		return serviceCollection;
	}

	private static IServiceCollection AddServices(this IServiceCollection service)
	{
		service.AddScoped<IAuthService, AuthService>();
		service.AddScoped<IAuthorService, AuthorService>();
		service.AddScoped<IFacultyService, FacultyService>();

		return service;
	}

	private static IServiceCollection AddCustomSwagger(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddSwaggerGen(swaggerGenOptions =>
		{
			swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "v1",
				Version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString()
			});

			swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using the Bearer scheme.",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Scheme = "bearer",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT"
			});

			swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
					},
					new List<string>()
				}
			});

			swaggerGenOptions.DescribeAllParametersInCamelCase();
			swaggerGenOptions.UseOneOfForPolymorphism();
			swaggerGenOptions.UseAllOfForInheritance();
			swaggerGenOptions.SelectDiscriminatorNameUsing(_ => "TypeName");
			swaggerGenOptions.SelectDiscriminatorValueUsing(subType => subType.Name);
			var commentsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
				$"{Assembly.GetEntryAssembly()?.GetName().Name}.xml");
			if (!File.Exists(commentsFile))
				throw new Exception(
					$"SwaggerExtensions: Xml comments file does not exist! ({commentsFile})");
			swaggerGenOptions.IncludeXmlComments(commentsFile);
			swaggerGenOptions.UseOneOfForPolymorphism();
			swaggerGenOptions.UseAllOfForInheritance();

			// Application specific swagger generation options that have been injected (nvm these)
			//options?.Invoke(swaggerGenOptions);
		});

		return serviceCollection;
	}

	private static IServiceCollection ConfigureAuthentication(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					// укзывает, будет ли валидироваться издатель при валидации токена
					ValidateIssuer = true,

					// строка, представляющая издателя
					ValidIssuer = AuthOptions.ISSUER,

					// будет ли валидироваться потребитель токена
					ValidateAudience = true,

					// установка потребителя токена
					ValidAudience = AuthOptions.AUDIENCE,

					// будет ли валидироваться время существования
					ValidateLifetime = true,

					// установка ключа безопасности
					IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

					// валидация ключа безопасности
					ValidateIssuerSigningKey = true
				};
			});

		return serviceCollection;
	}
}