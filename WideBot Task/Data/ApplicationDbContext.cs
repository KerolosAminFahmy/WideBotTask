using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WideBot_Task.Model;

namespace WideBot_Task.Data
{
	public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		   : base(options)
		{
		}
		public DbSet<RefreshToken> RefreshTokens { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
	}
}
