using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Klenzer.Domain.Entities;
using Klenzer.Persistence.Configuration;
using Klenzer.Domain.Repositories;
using Klenzer.WebApi.Controllers.Inputs.TipoServicos;
using Klenzer.WebApi.Extensions;

namespace Klenzer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServicosController : ControllerBase
    {
        private readonly ITipoServicoRepository _tipoServicos;

        public TipoServicosController(ITipoServicoRepository tipoServicos)
        {
            _tipoServicos = tipoServicos;
        }

        // GET: api/TipoServicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoServico>>> GetTipoServicos()
        {
            return await _tipoServicos.GetAll().ToListAsync();
        }

        // GET: api/TipoServicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoServico>> GetTipoServico(Guid id)
        {
            var tipoServico = await _tipoServicos.GetById(id);

            if (tipoServico == null)
            {
                return NotFound();
            }

            return tipoServico;
        }

        // PUT: api/TipoServicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoServico(Guid id, TipoServico tipoServico)
        {
            if (id != tipoServico.Id)
            {
                return BadRequest();
            }

            await _tipoServicos.Update(id, tipoServico);

            try
            {
                await _tipoServicos.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoServicoExists(id))
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

        // POST: api/TipoServicos
        [HttpPost]
        public async Task<ActionResult<TipoServico>> PostTipoServico(PostTipoServico tipoServico)
        {
            var novoTipo = tipoServico.MapTo<TipoServico>();
            await _tipoServicos.Create(novoTipo);
            await _tipoServicos.Commit();

            return CreatedAtAction("GetTipoServico", new { id = novoTipo.Id }, novoTipo);
        }

        // DELETE: api/TipoServicos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTipoServico(Guid id)
        {
            if (!TipoServicoExists(id))
            {
                return NotFound();
            }

            await _tipoServicos.Delete(id);
            await _tipoServicos.Commit();

            return Ok();
        }

        private bool TipoServicoExists(Guid id)
        {
            return _tipoServicos.GetAll().Any(e => e.Id == id);
        }
    }
}
