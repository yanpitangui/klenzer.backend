using System;
using System.Collections.Generic;

namespace Klenzer.WebApi.Controllers.Inputs.Agendamentos
{
    public class PostAgendamento
    {
        public Guid ClienteId { get; set; }

        public IEnumerable<PostAgendamentoServicos> AgendamentosServicos { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }
        public decimal Valor { get; set; }

        public Guid FuncionarioId { get; set; }
    }

    public class PostAgendamentoServicos
    {
        public Guid ServicoId { get; set; }
    }
}
