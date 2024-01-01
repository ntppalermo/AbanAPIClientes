using AbanChallenge.Models;

namespace AbanChallenge.Services
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IClienteService
    {
        Task<List<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<List<Cliente>> SearchClientesAsync(string nombre);
        Task InsertClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(int id, Cliente cliente);
        void DeleteCliente(int id);
        bool ClienteExiste(int id);
    }
}
