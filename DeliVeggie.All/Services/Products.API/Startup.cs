using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Products.API.Business;
using Products.API.Data;
using Products.API.MessageConsumers;
using Products.API.Repository;
using System;

namespace Products.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IBus>(RabbitHutch.CreateBus(Configuration["MessageBroker:ConnectionString"]));
			services.AddSingleton(RabbitHutch.CreateBus(Configuration["MessageBroker:ConnectionString"]));
			services.AddScoped<IProductsContext, ProductsContext>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IDiscountRepository, DiscountRepository>();
			services.AddScoped<IProductManager, ProductManager>();
			services.AddScoped<IDiscountManager, DiscountManager>();
			services.AddTransient<IProductRequestConsumer, ProductRequestConsumer>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseEasyNetQSubscribe("ProductMessageService");

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
