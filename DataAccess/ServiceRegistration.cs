using DataAccess.Contexts;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessRegistration(this IServiceCollection services)
        {
            services.AddIdentity<AppUser,AppRole>(opt =>
            {
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;

            }).AddRoles<AppRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddSignInManager<SignInManager<AppUser>>();
    }   }
}
