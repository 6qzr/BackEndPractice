using FirstWebAPI.Repositories;
using FirstWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ECommerceContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ProductRepo>();
            //builder.Services.AddScoped<CategoryRepo>();
            //builder.Services.AddScoped<UserRepo>();
            //builder.Services.AddScoped<OrderRepo>();
            //builder.Services.AddScoped<ReviewRepo>();

            builder.Services.AddScoped<ProductService>();
            //builder.Services.AddScoped<CategoryService>();
            //builder.Services.AddScoped<UserService>();
            //builder.Services.AddScoped<OrderService>();
            //builder.Services.AddScoped<ReveiwService>();

            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();  // End line of service container


            ////////////////////////////////////////////////////////////////////////////


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            
            // Run application
            app.Run();
        }
    }
}
