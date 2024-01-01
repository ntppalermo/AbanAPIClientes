using AbanChallenge.Models;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace AbanChallenge.Infrastructure.Data
{
    public interface IAppDbContext : IDisposable
    {
        DbSet<Cliente> Clientes { get; set; }
    }
}
