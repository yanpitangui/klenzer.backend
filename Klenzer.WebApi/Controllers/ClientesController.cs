using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Klenzer.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Klenzer.Domain.Repositories;
using Klenzer.WebApi.Controllers.Inputs.Clientes;
using Klenzer.WebApi.Extensions;
using Klenzer.WebApi.Controllers.Outputs;

namespace Klenzer.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clientes;

        public ClientesController(IClienteRepository clientes)
        {
            _clientes = clientes;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteOutput>>> GetClientes()
        {
            return await _clientes.GetAll().Select(x => x.MapTo<ClienteOutput>()).ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteOutput>> GetCliente(Guid id)
        {
            var cliente = await _clientes.GetById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente.MapTo<ClienteOutput>();
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(Guid id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            await _clientes.Update(id, cliente);

            try
            {
                _clientes.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<ClienteOutput>> PostCliente(PostCliente cliente)
        {
            var clienteNovo = cliente.MapTo<Cliente>();
            await _clientes.Create(clienteNovo);
            await _clientes.Commit();

            return CreatedAtAction("GetCliente", new { id = clienteNovo.Id }, clienteNovo.MapTo<ClienteOutput>());
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteOutput>> DeleteCliente(Guid id)
        {
            var cliente =  await _clientes.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }

            await _clientes.Delete(id);
            await _clientes.Commit();
            return cliente.MapTo<ClienteOutput>();
        }

        private bool ClienteExists(Guid id)
        {
            return _clientes.GetAll().Any(e => e.Id == id);
        }
    }
}
