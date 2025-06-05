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
        // Cria um novo TipoTarefa e adiciona à base de dados
        public static string contaTipoTarefa()
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Cria uma instância da base de dados

            // Conta o número de TipoTarefa e adiciona 1, começando em 1 se não houver nenhum
            int count = db.TipoTarefa.Count();

            return (count + 1).ToString();
        }
        // Método para gravar um novo TipoTarefa na base de dados
        public static List<TipoTarefa> ListarTipoTarefa()
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Retorna uma lista de TipoTarefa da base de dados
            return db.TipoTarefa.ToList();
        }
    }
}
