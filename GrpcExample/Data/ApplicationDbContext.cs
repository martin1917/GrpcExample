using GrpcExample.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcExample.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	public DbSet<Film> Films { get; set; }
}
