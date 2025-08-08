using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RecipeSharingPlatform.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=RecipeSharingPlatformDb;User Id=sa;Password=Pass#pass12;TrustServerCertificate=True;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}