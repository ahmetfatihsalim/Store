using Microsoft.EntityFrameworkCore;

namespace SaleStore.Data
{
    // The class which we use to access EF
    public class ApplicationDbContext : DbContext
    {
        //Basic Configuration
        // We inject/configure this dbcontext by getting the connection string as a parameter in the constructor in order to pass the connection 
        // string to this class as a dbcontext option and that we'll be passing on to the base class 
        // This way, whatever options we configured in this constructor will be passed on to our base class DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
