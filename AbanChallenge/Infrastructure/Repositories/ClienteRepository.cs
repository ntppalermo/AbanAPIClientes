using AbanChallenge.Infrastructure.Data;
using AbanChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace AbanChallenge.Infrastructure.Repositories
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id) => await _context.Clientes.FindAsync(id);

        public async Task<List<Cliente>> SearchClientesAsync(string nombre)
        {
            return await _context.Clientes
                .Where(c => c.Nombres.Contains(nombre))
                .ToListAsync();
        }

        public async Task InsertClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void DeleteCliente(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }

        public bool ClienteExiste(int id)
        {
            return _context.Clientes.Any(c => c.Id == id);
        }
    }
}
