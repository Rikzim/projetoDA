using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks.Models;

namespace iTasks.Controllers
{
    class TipoTarefaController
    {
        public static BasedeDados db = new BasedeDados();
        public static string contaTipoTarefa()
        {
            // Cria uma instância da base de dados

            // Conta o número de TipoTarefa e adiciona 1, começando em 1 se não houver nenhum
            int count = db.TipoTarefa.Count();

            return (count + 1).ToString();
        }
    }
}
