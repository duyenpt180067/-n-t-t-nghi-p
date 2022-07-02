using FoodManagement.Core.Interface.IService.General;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMFood;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMOrder;
using FoodManagement.Core.Interfaces.Infrastructure.IRepository.FMUser;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMFood;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMOrder;
using FoodManagement.Core.Interfaces.Infrastructure.IService.FMUser;
using FoodManagement.Core.Interfaces.Repository;
using FoodManagement.Core.Interfaces.Service;
using FoodManagement.Core.Service.FMFood;
using FoodManagement.Core.Service.FMOrder;
using FoodManagement.Core.Service.FMUser;
using FoodManagement.Core.Service.General;
using FoodManagement.Core.Services;
using FoodManagement.Repository;
using FoodManagement.Repository.Repository.FMFood;
using FoodManagement.Repository.Repository.FMUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderManagement.Repository.Repository.FMOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodManagement.API
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
            services.AddTransient<IStorageService, StorageService>();
            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(Configuration.GetSection("Storage:ConnectionString").Value);
            });

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodManagement.API", Version = "v1" });
            });

            //de cac bien cua entity viet hoa chu cai dau giong trong entity
            services.AddControllers()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            //de cac lop thuc hien cac phuong thuc cua cac interface tuong ung
            services.AddScoped(typeof(IBaseRepository<>), typeof(DBContext<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(ICartDetailRepository), typeof(CartDetailRepository));
            services.AddScoped(typeof(IFoodRepository), typeof(FoodRepository));
            services.AddScoped(typeof(IFoodService), typeof(FoodService));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodManagement.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(option => option.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
