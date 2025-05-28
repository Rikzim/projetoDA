using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using static iTasks.Models.Tarefa;

namespace iTasks.Controllers
{
    class TarefaController
    {
        public static BasedeDados db = new BasedeDados();

        public static void GravarTarefa(Gestor idGestor, Programador idProgramador, int ordemExecucao, string descricao,
            DateTime dataPrevistaInicio, DateTime dataPrevistaFim, TipoTarefa tipoTarefa, int storyPoints, DateTime dataCriacao, Estado estadoAtual)
        {
            var gestor = db.Gestor.Find(idGestor.id);
            var programador = db.Programador.Find(idProgramador.id);
            var tipoTarefaExistente = db.TipoTarefa.Find(tipoTarefa.Id);

            db.Tarefa.Add(new Tarefa
            {
                IdGestor = gestor,
                IdProgramador = programador,
                OrdemExecucao = ordemExecucao,
                Descricao = descricao,
                DataPrevistaInicio = dataPrevistaInicio,
                DataPrevistaFim = dataPrevistaFim,
                TipoTarefa = tipoTarefaExistente,
                StoryPoints = storyPoints,
                DataRealInicio = DateTime.Now, // Leva a data da hora atual, mas pode ser alterada posteriormente
                DataRealFim = DateTime.Now, // Inicialmente não tem data real de fim
                DataCriacao = dataCriacao,
                EstadoAtual = estadoAtual
            });
            //MessageBox.Show($"g: {gestor}, p: {programador}");
            db.SaveChanges();
        }

        public static List<Tarefa> ListarTarefas()
        {
            return db.Tarefa
                .Include(t => t.IdGestor)
                .Include(t => t.IdProgramador)
                .Include(t => t.TipoTarefa)
                .ToList();
        }
        public static List<Tarefa> ListarTarefasPorEstado(Tarefa.Estado estado)
        {
            return db.Tarefa
                .Include(t => t.IdGestor)
                .Include(t => t.IdProgramador)
                .Include(t => t.TipoTarefa)
                .Where(t => t.EstadoAtual == estado)
                .ToList();
        }
        public static int countTarefas()
        {
            int count = db.Tarefa.Count();

            return count + 1;
        }
        public static int countTarefasEstado(Estado estado)
        {
            int count = db.Tarefa.Where(t => t.EstadoAtual == estado).Count();
            return count + 1;
        }
    }
}
