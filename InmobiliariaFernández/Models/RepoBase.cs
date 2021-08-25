
namespace InmobiliariaFernández.Models;
public abstract class RepoBase
{
    protected readonly IConfiguration configuration;
    protected readonly string connectionString;

    protected RepoBase(IConfiguration configuration) { 
    this.configuration = configuration;
        connectionString = "Server = (localdb)\\MSSQLLocalDB; Database = InmobiliariaFernandez; Trusted_Connection = True; MultipleActiveResultSets = true";
    }
}
