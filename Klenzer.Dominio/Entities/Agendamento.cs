using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Klenzer.Domain.Entities
{
    public class Agendamento : BaseEntity
    {
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public IEnumerable<AgendamentoServico> AgendamentosServicos { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        [Column(TypeName = "numeric(15, 2)")]
        public decimal Valor { get; set; }

        public Guid FuncionarioId { get; set; }
        public User Funcionario { get; set; }
    }
}
