using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {           
        }


    }
}
