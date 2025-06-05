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
        public static void GravarTipoTarefa(string nomeTarefa)
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Adiciona o novo TipoTarefa à base de dados
            db.TipoTarefa.Add(new TipoTarefa(nomeTarefa));
            // Salva as alterações na base de dados
            db.SaveChanges();
        }
        public static void EditarTipoTarefa(TipoTarefa tipoTarefaSelecionada, string nomeTarefa)
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;

            TipoTarefa tipoTarefa = db.TipoTarefa.Find(tipoTarefaSelecionada.Id);

            tipoTarefa.Nome = nomeTarefa;
            // Salva as alterações na base de dados
            db.SaveChanges();
        }
        public static void EliminarTipoTarefa(TipoTarefa tipoTarefaSelecionada)
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;

            TipoTarefa tipoTarefa = db.TipoTarefa.Find(tipoTarefaSelecionada.Id);
            // Remove o TipoTarefa selecionado da base de dados
            db.TipoTarefa.Remove(tipoTarefa);
            // Salva as alterações na base de dados
            db.SaveChanges();
        }
    }
}
