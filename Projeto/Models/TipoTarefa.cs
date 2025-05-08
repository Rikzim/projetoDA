using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    class TipoTarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public TipoTarefa(string nome)
        {
            this.Nome = nome;
        }
    }
}
