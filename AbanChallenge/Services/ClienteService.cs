using AbanChallenge.Infrastructure.Repositories;
using AbanChallenge.Models;

namespace AbanChallenge.Services
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            return await _clienteRepository.GetAllClientesAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _clienteRepository.GetClienteByIdAsync(id);
        }

        public async Task<List<Cliente>> SearchClientesAsync(string nombre)
        {
            return await _clienteRepository.SearchClientesAsync(nombre);
        }

        public async Task InsertClienteAsync(Cliente cliente)
        {
            await _clienteRepository.InsertClienteAsync(cliente);
        }

        public async Task UpdateClienteAsync(int id, Cliente cliente)
        {
            await _clienteRepository.UpdateClienteAsync(cliente);
        }

        public void DeleteCliente(int id)
        {
            _clienteRepository.DeleteCliente(id);
        }

        public bool ClienteExiste(int id)
        {
            return _clienteRepository.ClienteExiste(id);
        }
    }
}
