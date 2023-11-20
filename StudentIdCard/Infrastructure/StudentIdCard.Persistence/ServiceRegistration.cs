using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentIdCard.Application.Repositories.BaseCard;
using StudentIdCard.Application.Repositories.Basket;
using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Application.Repositories.Product;
using StudentIdCard.Application.Repositories.User;
using StudentIdCard.Persistence.Context;
using StudentIdCard.Persistence.Repositories.BaseCard;
using StudentIdCard.Persistence.Repositories.Basket;
using StudentIdCard.Persistence.Repositories.Card;
using StudentIdCard.Persistence.Repositories.Product;
using StudentIdCard.Persistence.Repositories.User;
using StudentIdCard.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<StudentIdCardContext>(opt => opt.UseSqlServer("Server = 104.247.167.130\\MSSQLSERVER2022; Initial Catalog = okulkart_DBSet; User ID = okulkart_Admin; Password = cc2#y24N0;"));
       
            services.AddScoped<ICardUpdateRepository, CardUpdateRepository>();
            services.AddScoped<ICardWriteRepository, CardWriteRepository>();
            services.AddScoped<ICardReadRepository, CardReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IBaseCardReadRepository, BaseCardReadRepository>();
            services.AddScoped<IBaseCardWriteRepository, BaseCardWriteRepository>();
            services.AddHostedService<CafeteriaCardService>();

        }
    }
}
