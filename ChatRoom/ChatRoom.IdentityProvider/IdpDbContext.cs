using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.IdentityProvider
{
    public class IdpDbContext : IdentityDbContext
    {
        public IdpDbContext(DbContextOptions<IdpDbContext> options): base(options)
        {
        }
    }
}
