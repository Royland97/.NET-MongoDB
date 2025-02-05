using Infrastructure.DataAccess.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace UserInterface.Web.Installation
{
    /// <summary>
    /// Initialize Db and Populate it with data
    /// </summary>
    public class DbInitializer
    {
        public async static Task Initialize(ApplicationDbContext applicationDbContext)
        {
            await applicationDbContext.Database.EnsureCreatedAsync();
        }

    }
}
