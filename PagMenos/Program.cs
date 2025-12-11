using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PagMenos.Application.Interfaces.Services;
using PagMenos.Application.Mappings;
using PagMenos.Application.Services;
using PagMenos.Domain.Interfaces;
using PagMenos.Domain.Validators;
using PagMenos.Infraestructure.Data;
using PagMenos.Infraestructure.Repositories;
using PagMenos.Infrastructure.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PagMenos
{
	public partial class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
					options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
					options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
				});

			builder.Services.AddControllers(options =>
			{
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();

				//options.Filters.Add(new AuthorizeFilter(policy));
			});


			builder.Services.AddDbContext<EFDbContext>(options =>
	options.UseInMemoryDatabase("PagMenosDataBase")
		   .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<CollaboratorProfile>();
				cfg.AddProfile<OrderProfile>();
				cfg.AddProfile<OrderItemProfile>();
				cfg.AddProfile<ProductProfile>();
			});

			// Registros do validators
			builder.Services.AddScoped<OrderValidator>();
			builder.Services.AddScoped<ProductValidator>();
			builder.Services.AddScoped<CollaboratorValidator>();

			// Registro dos repositorios 
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IOrderRepository, OrderRepository>();
			builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			// Registro dos serviços 
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IOrderService, OrderService>();
			builder.Services.AddScoped<IOrderItemService, OrderItemService>();
			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<ICollaboratorService, CollaboratorService>();
			builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin()
						  .AllowAnyMethod()
						  .AllowAnyHeader();
				});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseCors("AllowAll");
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
