using Klenzer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klenzer.WebApi.Controllers.Inputs.Servicos
{
    public class PostServico
    {
        public string Descricao { get; set; }
        public decimal Preco { get; set; }

        public Guid TipoServicoId { get; set; }
    }
}
