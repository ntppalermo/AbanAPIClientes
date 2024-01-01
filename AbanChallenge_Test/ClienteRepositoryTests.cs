using AbanChallenge.Infrastructure.Repositories;
using AbanChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace AbanChallenge_Test
{
    [TestFixture]
    public class ClienteRepositoryTests
    {
        private DbContextOptions<TestDbContext> _options;
        private TestDbContext _context;
        private ClienteRepository _clienteRepository;

        [SetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            _context = new TestDbContext(_options);
            _clienteRepository = new ClienteRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllClientesAsync_ShouldReturnListOfClientes()
        {
            var clientes = new List<Cliente> {
                new Cliente { Id = 1, Nombres = "Nombre1", Apellidos ="Apellido1" },
                new Cliente { Id = 2, Nombres = "Nombre2", Apellidos ="Apellido2" }
            };
            
            _context.Clientes.AddRange(clientes);
            _context.SaveChanges();

            var result = await _clienteRepository.GetAllClientesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetClienteByIdAsync_ShouldReturnCliente()
        {
            var cliente = new Cliente { Id = 1, Nombres = "Nombre1", Apellidos = "Apellido1" };
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            var result = await _clienteRepository.GetClienteByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(1));
                Assert.That(result.Nombres, Is.EqualTo("Nombre1"));
            });
        }

        [Test]
        public async Task SearchClientesAsync_ShouldReturnMatchingClientes()
        {
            var clientes = new List<Cliente> {
                new Cliente { Id = 1, Nombres = "Nombre1", Apellidos ="Apellido1" },
                new Cliente { Id = 2, Nombres = "Nombre2", Apellidos ="Apellido1" }
            };

            _context.Clientes.AddRange(clientes);
            _context.SaveChanges();

            var result = await _clienteRepository.SearchClientesAsync("Nombre1");

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task InsertClienteAsync_ShouldAddClienteToDatabase()
        {
            var cliente = new Cliente { Id = 1, Nombres = "Nombre1", Apellidos = "Apellido1" };

            await _clienteRepository.InsertClienteAsync(cliente);

            var result = _context.Clientes.Find(1);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Nombres, Is.EqualTo("Nombre1"));
        }

        [Test]
        public async Task UpdateClienteAsync_ShouldUpdateClienteInDatabase()
        {
            var cliente = new Cliente { Id = 1, Nombres = "Nombre1", Apellidos = "Apellido1" };
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            cliente.Nombres = "Cliente1Modificado";

            await _clienteRepository.UpdateClienteAsync(cliente);

            var result = _context.Clientes.Find(1);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Nombres, Is.EqualTo("Cliente1Modificado"));
        }

        [Test]
        public void DeleteCliente_ShouldRemoveClienteFromDatabase()
        {
            var cliente = new Cliente { Id = 1, Nombres = "Nombre1", Apellidos = "Apellido1" };
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            _clienteRepository.DeleteCliente(1);

            var result = _context.Clientes.Find(1);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ClienteExiste_ShouldReturnTrueForExistingCliente()
        {
            var cliente = new Cliente { Id = 1, Nombres = "Nombre1", Apellidos = "Apellido1" };
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            var result = _clienteRepository.ClienteExiste(1);

            Assert.That(result, Is.True);
        }

        [Test]
        public void ClienteExiste_ShouldReturnFalseForNonExistingCliente()
        {
            var result = _clienteRepository.ClienteExiste(1);

            Assert.That(result, Is.False);
        }
    }

}
