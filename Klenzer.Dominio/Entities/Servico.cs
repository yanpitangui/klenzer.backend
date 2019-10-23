using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Klenzer.Domain.Entities
{
    public class Servico : BaseEntity
    {
        public string Descricao { get; set; }

        [Column(TypeName = "numeric(15, 2)")]
        public decimal Preco { get; set; }

        public Guid TipoServicoId { get; set; }
        public TipoServico TipoServico { get; set; }
    }
}
