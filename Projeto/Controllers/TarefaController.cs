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
            db.SaveChanges();
        }
        public static int countTarefas()
        {
            int count = db.Tarefa.Count();

        public static void MudarEstadoTarefa(Tarefa tarefaSelecionada, Estado estado)
        {
            // Verifica se a tarefa selecionada não é nula
            if (tarefaSelecionada != null)
            {
                if (estado == Estado.Doing) 
                {
                    // Atualiza o estado da tarefa
                    tarefaSelecionada.EstadoAtual = estado;
                    tarefaSelecionada.DataRealInicio = DateTime.Now; // Define a data real de início como a data atual
                    db.SaveChanges();
                }
                else if (estado == Estado.ToDo)
                {
                    // Atualiza o estado da tarefa
                    tarefaSelecionada.EstadoAtual = estado;
                    //TODO: Se necessário, pode-se definir a data real de início como nula ou não alterar
                    tarefaSelecionada.DataRealInicio = DateTime.Now; // Define a data real de início como a data atual
                    db.SaveChanges();
                }
                else if (estado == Estado.Done)
                {
                    // Atualiza o estado da tarefa
                    tarefaSelecionada.EstadoAtual = estado;
                    tarefaSelecionada.DataRealFim = DateTime.Now; // Define a data real de fim como a data atual
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Estado inválido.");
                }
            }
            else
            {
                MessageBox.Show("Nenhuma tarefa selecionada.");
            }
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


    }
}
