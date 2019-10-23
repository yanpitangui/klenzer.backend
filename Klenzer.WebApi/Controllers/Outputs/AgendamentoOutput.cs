using Klenzer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klenzer.WebApi.Controllers.Outputs
{
    public class AgendamentoOutput
    {
        public Guid Id { get; set; }
        public ClienteOutput Cliente { get; set; }

        public IEnumerable<ServicoOutput> AgendamentosServicos { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public decimal Valor { get; set; }

        public UserOutput Funcionario { get; set; }
    }
}
