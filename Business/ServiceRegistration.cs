using Business.Services.Abstracts;
using Business.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class ServiceRegistration
    {
        public static void AddBusinnesRegistration(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }
}
