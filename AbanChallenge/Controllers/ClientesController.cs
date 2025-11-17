using AbanChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using AbanChallenge.Services;
using System.ComponentModel.DataAnnotations;

namespace AbanChallenge.Controllers
{
    /// <summary>
    /// Controlador para manejar operaciones relacionadas con clientes.
    /// </summary>
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
		//Comentario para probar GitHub Desktop

		private readonly IClienteService _clienteService;

        /// <summary>
        /// Constructor para inicializar una nueva instancia del controlador de clientes.
        /// </summary>
        /// <param name="clienteService">Servicio para operaciones relacionadas con clientes.</param>
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Obtiene todos los clientes existentes.
        /// </summary>
        /// <remarks>
        /// <para>Recupera la lista completa de clientes.</para>
        /// </remarks>
        /// <returns>
        /// <para>200 OK con la lista de clientes si la operación se realiza con éxito.</para>
        /// <para>500 Internal Server Error si se produce un error interno al intentar obtener la lista de clientes.</para>
        /// </returns>
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Cliente>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clientes = await _clienteService.GetAllClientesAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error interno al intentar obtener todos los clientes");
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Obtiene un cliente por su identificador -> Id.
        /// </summary>
        /// <remarks>
        /// <para>Obtiene la información detallada de un cliente específico mediante su identificador -> Id.</para>
        /// </remarks>
        /// <param name="id">ID del cliente.</param>
        /// <returns>
        /// <para>200 OK con la información del cliente si se encuentra.</para>
        /// <para>404 Not Found si no se encuentra un cliente con el identificador proporcionado.</para>
        /// <para>500 Internal Server Error si se produce un error interno al intentar obtener el cliente.</para>
        /// </returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id);

                if (cliente == null)
                {
                    return NotFound($"El cliente con el Id {id} no se ha encontrado");
                }

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error interno al intentar obtener un cliente por Id");
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Realiza una búsqueda de un cliente por su nombre.
        /// </summary>
        /// <remarks>
        /// <para>Realiza una búsqueda de clientes cuyos nombres coincidan con el nombre proporcionado.</para>
        /// </remarks>
        /// <param name="nombre">El nombre a buscar entre los clientes.</param>
        /// <returns>
        /// <para>200 OK con la lista de clientes si la búsqueda es exitosa.</para>
        /// <para>400 Bad Request si el parámetro 'nombre' está vacío o es nulo, o no es una cadena válida.</para>
        /// <para>404 Not Found si no se encuentran clientes con el nombre proporcionado.</para>
        /// <para>500 Internal Server Error si se produce un error interno durante la búsqueda.</para>
        /// </returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Cliente>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] string nombre)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    return BadRequest("El parámetro 'nombre' no puede estar vacío o ser nulo");
                }

                nombre = JsonConvert.DeserializeObject<string>(nombre);

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return BadRequest("El parámetro 'nombre' después de deserializar no puede estar vacío o contener solo espacios en blanco");
                }

                var clientes = await _clienteService.SearchClientesAsync(nombre);

                if (clientes == null || clientes.Count == 0)
                {
                    return NotFound($"No se encontraron clientes con el nombre '{nombre}'");
                }

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error interno al intentar buscar clientes por nombre");
                return new StatusCodeResult(500);
            }
        }


        /// <summary>
        /// Agrega un nuevo cliente.
        /// </summary>
        /// <remarks>
        /// Crea un nuevo registro de cliente con la información proporcionada.
        /// </remarks>
        /// <param name="cliente">Datos del nuevo cliente.</param>
        /// <returns>
        /// <para>201 Created si el cliente se crea correctamente.</para>
        /// <para>400 Bad Request si el campo 'id' es nulo o cero, o los datos del cliente no son válidos.</para>
        /// <para>409 Conflict si ya existe un cliente con el mismo ID.</para>
        /// <para>500 Internal Server Error si se produce un error interno durante la inserción.</para>
        /// </returns>
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente.Id == 0)
                {
                    return BadRequest("el campo 'id' es obligatorio y no puede ser nulo o cero");
                }

                if (_clienteService.ClienteExiste(cliente.Id))
                {
                    return Conflict($"No es posible agregar un cliente con ese Id {cliente.Id}");
                }

                var validationErrorResult = ValidateEntity(cliente);
                if (validationErrorResult != null)
                {
                    return validationErrorResult;
                }

                await _clienteService.InsertClienteAsync(cliente);

                return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error interno al intentar agregar un nuevo cliente");
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <remarks>
        /// Actualiza la información de un cliente según su ID.
        /// </remarks>
        /// <param name="id">Identificador único del cliente -> ID.</param>
        /// <param name="cliente">Datos actualizados del cliente.</param>
        /// <returns>
        /// <para>204 No Content si el cliente se actualiza correctamente.</para>
        /// <para>400 Bad Request si los datos del cliente no son válidos o el ID no coincide.</para>
        /// <para>404 Not Found si el cliente con el ID especificado no se encuentra.</para>
        /// <para>409 Conflict si se produce un error de concurrencia (el cliente fue modificado por otro usuario).</para>
        /// <para>500 Internal Server Error si se produce un error interno durante la actualización.</para>
        /// </returns>
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if (id != cliente.Id || !ModelState.IsValid)
                {
                    return BadRequest("Datos de cliente no válidos o el Id no coincide");
                }

                var validationErrorResult = ValidateEntity(cliente);
                if (validationErrorResult != null)
                {
                    return validationErrorResult;
                }

                if (!_clienteService.ClienteExiste(id))
                {
                    return NotFound($"El cliente con el Id {id} no se ha encontrado");
                }

                await _clienteService.UpdateClienteAsync(id, cliente);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                Log.Error($"Error de concurrencia al intentar actualizar el cliente. Id: {id}");
                return Conflict("Error de concurrencia: El cliente fue modificado por otro usuario.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error interno al intentar actualizar el cliente. Id: {id}");
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Elimina un cliente existente.
        /// </summary>
        /// <remarks>
        /// Elimina de forma permanente un cliente según su ID.
        /// </remarks>
        /// <param name="id">Identificador único del cliente -> ID</param>
        /// <returns>
        /// <para>204 No Content si el cliente se elimina correctamente.</para>
        /// <para>404 Not Found si el cliente con el ID especificado no se encuentra.</para>
        /// <para>500 Internal Server Error si se produce un error interno durante la eliminación.</para>
        /// </returns>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!_clienteService.ClienteExiste(id))
                {
                    return NotFound($"El cliente con el Id {id} no se ha encontrado");
                }

                _clienteService.DeleteCliente(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error al intentar eliminar el cliente id: {id}");
                return new StatusCodeResult(500);
            }
        }

        private IActionResult? ValidateEntity(object entity)
        {
            var validationContext = new ValidationContext(entity, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(entity, validationContext, validationResults, validateAllProperties: true))
            {
                var errorMessages = validationResults.Select(r => r.ErrorMessage);
                return BadRequest(errorMessages);
            }

            return null;
        }
    }
}
