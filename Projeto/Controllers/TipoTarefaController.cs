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
            try
            {
                // Obtém a instância da base de dados
                BasedeDados db = BasedeDados.Instance;
                // Cria uma instância da base de dados

                // Conta o número de TipoTarefa e adiciona 1, começando em 1 se não houver nenhum
                int count = db.TipoTarefa.Count();

                return (count + 1).ToString();
            }
            catch (Exception ex)
            {
                // Lança uma exceção se ocorrer um erro ao contar os TipoTarefa
                throw new Exception("Erro ao contar TipoTarefa: " + ex.Message);
            }
        }
        // Método para gravar um novo TipoTarefa na base de dados
        public static List<TipoTarefa> ListarTipoTarefa()
        {
            try
            {
                // Obtém a instância da base de dados
                BasedeDados db = BasedeDados.Instance;
                // Retorna uma lista de TipoTarefa da base de dados
                return db.TipoTarefa.ToList();
            }
            catch (Exception ex)
            {
                // Lança uma exceção se ocorrer um erro ao listar os TipoTarefa
                throw new Exception("Erro ao listar TipoTarefa: " + ex.Message);
            }
        }
    }
}
