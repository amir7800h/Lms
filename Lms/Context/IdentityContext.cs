using Lms.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lms.Context
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext()
        {

        }
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }

    }

}
