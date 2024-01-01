using AbanChallenge.Models;

namespace AbanChallenge.Infrastructure.Repositories
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<List<Cliente>> SearchClientesAsync(string nombre);
        Task InsertClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        void DeleteCliente(int id);
        bool ClienteExiste(int id);
    }
}
