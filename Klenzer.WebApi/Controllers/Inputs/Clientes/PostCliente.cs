using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Klenzer.WebApi.Controllers.Inputs.Clientes
{
    public class PostCliente
    {
        public string Nome { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Telefone { get; set; }

        public string Instagram { get; set; }

        public string DiaNascimento { get; set; }

        public string MesNascimento { get; set; }
    }
}
