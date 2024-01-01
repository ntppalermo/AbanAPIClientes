using AbanChallenge.Controllers;
using AbanChallenge.Models;
using AbanChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace AbanChallenge_Test
{
    [TestFixture]
    public class ClientesControllerTests
    {
        private Mock<IClienteService> _clienteServiceMock;
        private ClientesController _clientesController;

        [SetUp]
        public void SetUp()
        {
            _clienteServiceMock = new Mock<IClienteService>();
            _clientesController = new ClientesController(_clienteServiceMock.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturnOkResult()
        {
            _clienteServiceMock.Setup(x => x.GetAllClientesAsync()).ReturnsAsync(new List<Cliente>());

            var result = await _clientesController.GetAll();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Get_ExistingId_ShouldReturnOkResult()
        {
            const int existingId = 1;
            _clienteServiceMock.Setup(x => x.GetClienteByIdAsync(existingId)).ReturnsAsync(new Cliente
            {
                Nombres = "Nombre",
                Apellidos = "Apellido"
            });

            var result = await _clientesController.Get(existingId);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Get_NonExistingId_ShouldReturnNotFoundResult()
        {
            const int nonExistingId = 2;
            _clienteServiceMock.Setup(x => x.GetClienteByIdAsync(nonExistingId)).ReturnsAsync((Cliente)null);

            var result = await _clientesController.Get(nonExistingId);

            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

      
        [Test]
        public async Task Insert_ValidInput_ReturnsCreatedAtActionResult()
        {
            var cliente = new Cliente { Id = 1, Nombres = "Nombre" , Apellidos= "Apellido"};
            _clienteServiceMock.Setup(x => x.ClienteExiste(cliente.Id)).Returns(false);
            _clienteServiceMock.Setup(x => x.InsertClienteAsync(cliente));

            var result = await _clientesController.Insert(cliente);

            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }


        [Test]
        public async Task Update_ValidInput_ReturnsNoContentResult()
        {
            const int existingId = 1;
            var cliente = new Cliente { Id = existingId, Nombres = "Nombre", Apellidos = "Apellido" };
            _clienteServiceMock.Setup(x => x.ClienteExiste(existingId)).Returns(true);
            _clienteServiceMock.Setup(x => x.UpdateClienteAsync(existingId, cliente));

            var result = await _clientesController.Update(existingId, cliente);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public void Delete_Client_ReturnsNoContentResult()
        {
            const int existingId = 1;
            _clienteServiceMock.Setup(x => x.ClienteExiste(existingId)).Returns(true);
            _clienteServiceMock.Setup(x => x.DeleteCliente(existingId));

            var result = _clientesController.Delete(existingId);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }
    }
}