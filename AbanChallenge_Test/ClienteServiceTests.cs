using Moq;
using AbanChallenge.Infrastructure.Repositories;
using AbanChallenge.Models;
using AbanChallenge.Services;

namespace AbanChallenge.Tests
{
    [TestFixture]
    public class ClienteServiceTests
    {
        private Mock<IClienteRepository> _clienteRepositoryMock;
        private IClienteService _clienteService;

        [SetUp]
        public void Setup()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_clienteRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllClientesShouldReturnListOfClientes()
        {
            var expectedClientes = new List<Cliente> {
              new Cliente { Nombres = "Jose" , Apellidos = "Perez" },
                new Cliente { Nombres = "Estaban" , Apellidos = "Gonzalez" },
            };

            _clienteRepositoryMock.Setup(x => x.GetAllClientesAsync()).ReturnsAsync(expectedClientes);

            var result = await _clienteService.GetAllClientesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.InstanceOf<List<Cliente>>());
            Assert.That(result, Has.Count.EqualTo(2));
        }

    
        [Test]
        public async Task GetClienteById_ExistingId_ShouldReturnCliente()
        {
            const int existingId = 1;
            var expectedCliente = new Cliente { Nombres = "Nombre", Apellidos = "Apellido" };
            _clienteRepositoryMock.Setup(x => x.GetClienteByIdAsync(existingId)).ReturnsAsync(expectedCliente);

            var result = await _clienteService.GetClienteByIdAsync(existingId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedCliente));
        }

        [Test]
        public async Task SearchClientes_ValidInput_ShouldReturnListOfClientes()
        {
            const string nombre = "cliente";
            var expectedClientes = new List<Cliente> { };
            _clienteRepositoryMock.Setup(x => x.SearchClientesAsync(nombre)).ReturnsAsync(expectedClientes);

            var result = await _clienteService.SearchClientesAsync(nombre);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedClientes));
        }
    }
}
