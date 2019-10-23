using System;
using System.Collections.Generic;
using System.Text;

namespace Klenzer.Domain.Entities
{
    public class AgendamentoServico
    {
        public Guid AgendamentoId { get; set; }
        public Agendamento Agendamento { get; set; }
        public Guid ServicoId { get; set; }
        public Servico Servico { get; set; }
    }
}
