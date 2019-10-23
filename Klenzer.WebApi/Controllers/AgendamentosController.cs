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
using Microsoft.AspNetCore.Authorization;
using Klenzer.WebApi.Controllers.Inputs.Agendamentos;
using Klenzer.WebApi.Extensions;
using Klenzer.WebApi.Controllers.Outputs;

namespace Klenzer.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentosController : ControllerBase
    {
        private readonly IAgendamentoRepository _agendamentos;
        public AgendamentosController(IAgendamentoRepository agendamentos)
        {
            _agendamentos = agendamentos;

        }

        // GET: api/Agendamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgendamentoOutput>>> GetAgendamentos()
        {
            return await _agendamentos.GetAll()
                .Include(x=> x.AgendamentosServicos)
                .ThenInclude(x=> x.Servico)
                .ThenInclude(x=> x.TipoServico)
                .Include(x=> x.Funcionario)
                .Include(x=> x.Cliente)
                .Select(x=> x.MapTo<AgendamentoOutput>())
                .ToListAsync();
        }

        // GET: api/Agendamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgendamentoOutput>> GetAgendamento(Guid id)
        {
            var agendamento = await _agendamentos.GetAll()
                .Include(x => x.AgendamentosServicos)
                .ThenInclude(x => x.Servico)
                .ThenInclude(x => x.TipoServico)
                .Include(x => x.Funcionario)
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return agendamento.MapTo<AgendamentoOutput>();
        }

        // PUT: api/Agendamentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendamento(Guid id, Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                return BadRequest();
            }

            await _agendamentos.Update(id, agendamento);
            try
            {
                await _agendamentos.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendamentoExists(id))
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

        // POST: api/Agendamentos
        [HttpPost]
        public async Task<ActionResult<AgendamentoOutput>> PostAgendamento(PostAgendamento agendamento)
        {
            var agendamentoNovo = agendamento.MapTo<Agendamento>();
            await _agendamentos.Create(agendamentoNovo);
            await _agendamentos.Commit();

            return CreatedAtAction("GetAgendamento", new { id = agendamentoNovo.Id }, agendamentoNovo.MapTo<AgendamentoOutput>());
        }

        // DELETE: api/Agendamentos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AgendamentoOutput>> DeleteAgendamento(Guid id)
        {
            var agendamento = await _agendamentos.GetById(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            await _agendamentos.Delete(id);
            await _agendamentos.Commit();

            return agendamento.MapTo<AgendamentoOutput>();
        }

        private bool AgendamentoExists(Guid id)
        {
            return _agendamentos.GetAll().Any(e => e.Id == id);
        }
    }
}
