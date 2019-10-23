using Klenzer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klenzer.WebApi.Controllers.Outputs
{
    public class ServicoOutput
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public TipoServicoOutput TipoServico { get; set; }
    }
}
