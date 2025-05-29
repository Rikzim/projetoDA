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
        public static void GravarTarefa(Gestor idGestor, Programador idProgramador, int ordemExecucao, string descricao,
            DateTime dataPrevistaInicio, DateTime dataPrevistaFim, TipoTarefa tipoTarefa, int storyPoints, DateTime dataCriacao, Estado estadoAtual)
        {
            BasedeDados db = BasedeDados.Instance;
            // Verifica se o gestor e programador existem
            db.Tarefa.Add(new Tarefa
            (
                idGestor,
                idProgramador,
                ordemExecucao,
                descricao,
                dataPrevistaInicio,
                dataPrevistaFim,
                tipoTarefa,
                storyPoints,
                dataCriacao,
                estadoAtual
            ));
            db.SaveChanges();
        }
        public static int countTarefas()
        {
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de tarefas na base de dados e adiciona 1, começando em 1 se não houver nenhuma
            int count = db.Tarefa.Count();
            return count + 1;
        }

        public static void MudarEstadoTarefa(Tarefa tarefaSelecionada, Estado estado)
        {
            BasedeDados db = BasedeDados.Instance;
            // Verifica se a tarefa selecionada é nula
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

        /*public static List<Tarefa> ListarTarefas(Utilizador utilizadorLogado)
        {
            return db.Tarefa
                .Include(t => t.IdGestor)
                .Include(t => t.IdProgramador)
                .Include(t => t.TipoTarefa)
                .ToList();
        }
        */
        public static List<Tarefa> ListarTarefasPorEstado(Tarefa.Estado estado, Utilizador utilizadorLogado)
        {
            BasedeDados db = BasedeDados.Instance;
            // Verifica se o utilizador logado é um Programador ou Gestor
            if (utilizadorLogado is Programador)
            {
                return db.Tarefa
                    .Include(t => t.IdGestor)
                    .Include(t => t.IdProgramador)
                    .Include(t => t.TipoTarefa)
                    .Where(t => t.IdProgramador.id == utilizadorLogado.id && t.EstadoAtual == estado)
                    .ToList();
            }
            else
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
}
