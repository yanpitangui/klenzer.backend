using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Klenzer.Domain.Entities;
using Klenzer.Persistence.Configuration;
using Klenzer.Domain.Repositories;
using Klenzer.WebApi.Controllers.Inputs.Servicos;
using Klenzer.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Klenzer.WebApi.Controllers.Outputs;

namespace Klenzer.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly IServicoRepository _servicos;

        public ServicosController(IServicoRepository servicos)
        {
            _servicos = servicos;
        }

        // GET: api/Servicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoOutput>>> GetServicos()
        {
            return await _servicos.GetAll().Include(x=> x.TipoServico).Select(x=> x.MapTo<ServicoOutput>()).ToListAsync();
        }

        // GET: api/Servicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoOutput>> GetServico(Guid id)
        {
            var servico = await _servicos.GetById(id);

            if (servico == null)
            {
                return NotFound();
            }

            return servico.MapTo<ServicoOutput>();
        }

        // PUT: api/Servicos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServico(Guid id, Servico servico)
        {
            if (id != servico.Id)
            {
                return BadRequest();
            }

            await _servicos.Update(id, servico);

            try
            {
                await _servicos.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicoExists(id))
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

        // POST: api/Servicos
        [HttpPost]
        public async Task<ActionResult<ServicoOutput>> PostServico(PostServico servico)
        {
            var retorno = await _servicos.Create(servico.MapTo<Servico>());
            await _servicos.Commit();

            return CreatedAtAction("GetServico", new { id = retorno.Id }, retorno.MapTo<ServicoOutput>());
        }

        // DELETE: api/Servicos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServicoOutput>> DeleteServico(Guid id)
        {
            var servico = await _servicos.GetById(id);
            if (servico == null)
            {
                return NotFound();
            }

            await _servicos.Delete(id);
            await _servicos.Commit();

            return servico.MapTo<ServicoOutput>();
        }

        private bool ServicoExists(Guid id)
        {
            return _servicos.GetAll().Any(e => e.Id == id);
        }
    }
}
