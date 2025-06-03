using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iTasks.Models.Tarefa;


namespace iTasks.Controllers
{
    class TarefaController
    {
        public static void GravarTarefa(Gestor idGestor, Programador idProgramador, int ordemExecucao, string descricao,
            DateTime dataPrevistaInicio, DateTime dataPrevistaFim, TipoTarefa tipoTarefa, int storyPoints, DateTime dataCriacao, Estado estadoAtual)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar tarefa: " + ex.Message);
            }
        }

        public static void EditarTarefa(Tarefa tarefaSelecionada, Gestor idGestor, Programador idProgramador, int ordemExecucao, string descricao,
            DateTime dataPrevistaInicio, DateTime dataPrevistaFim, TipoTarefa tipoTarefa, int storyPoints)
        {
            try
            {
                BasedeDados db = BasedeDados.Instance;
                // Verifica se a tarefa selecionada é nula
                if (tarefaSelecionada != null)
                {
                    // Atualiza os dados da tarefa selecionada
                    tarefaSelecionada.IdGestor = idGestor;
                    tarefaSelecionada.IdProgramador = idProgramador;
                    tarefaSelecionada.OrdemExecucao = ordemExecucao;
                    tarefaSelecionada.Descricao = descricao;
                    tarefaSelecionada.DataPrevistaInicio = dataPrevistaInicio;
                    tarefaSelecionada.DataPrevistaFim = dataPrevistaFim;
                    tarefaSelecionada.TipoTarefa = tipoTarefa;
                    tarefaSelecionada.StoryPoints = storyPoints;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar tarefa: " + ex.Message);
            }
        }
        public static int countTarefas()
        {
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de tarefas na base de dados e adiciona 1, começando em 1 se não houver nenhuma
            int count = db.Tarefa.Count();
            return count + 1;
        }
        public static int countTarefasPorEstado(Estado estado)
        {
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de tarefas na base de dados e adiciona 1, começando em 1 se não houver nenhuma
            int count = db.Tarefa.Where(t => t.EstadoAtual == estado).Count();
            return count + 1;
        }
        public static int countTarefasPorEstadoProgramador(Estado estado, Utilizador utilizadorRecebido)
        {
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de tarefas na base de dados e adiciona 1, começando em 1 se não houver nenhuma
            int count = db.Tarefa.
                Where(t => t.EstadoAtual == estado
                && t.IdProgramador.id == utilizadorRecebido.id)
                .Count();
            return count;
        }

        public static void MudarEstadoTarefa(Tarefa tarefaSelecionada, Estado estado, Utilizador utilizadorRecebido)
        {
            BasedeDados db = BasedeDados.Instance;
            // Verifica se a tarefa selecionada é nula
            // Verifica se a tarefa selecionada não é nula
            if (tarefaSelecionada != null)
            {
                if (tarefaSelecionada.IdProgramador.id == utilizadorRecebido.id)
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
                        tarefaSelecionada.DataRealInicio = null; // Define a data real de início como a data atual
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
                        MessageBox.Show("Estado inválido."); // É PARA POR UM THROW EXCEPTION AQUI!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    }
                }
                else
                {
                    MessageBox.Show("Apenas o programador responsável pela tarefa pode alterar o seu estado.");
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
            return db.Tarefa
                .Include(t => t.IdGestor)
                .Include(t => t.IdProgramador)
                .Include(t => t.TipoTarefa)
                .Where(t => t.EstadoAtual == estado)
                .ToList();
        }

        public static bool ExportarCSV(Gestor gestor)
        {
            try
            {
                // Configurar diálogo antes de mostrar
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv",
                    Title = "Exportar tarefas concluídas em CSV.",
                    InitialDirectory = Application.StartupPath,
                    FileName = $"TarefasConcluidas_{gestor.nome}_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return false;

                string caminhoFicheiro = saveFileDialog.FileName;

                var tarefasConcluidas = ListarTarefasPorEstado(Tarefa.Estado.Done, gestor);

                var sb = new StringBuilder();
                sb.AppendLine("IdTarefa;IdGestor;IdProgramador;OrdemExecucao;Descricao;DataPrevistaInicio;DataPrevistaFim;IdTipoTarefa;StoryPoints;DataRealInicio;DataRealFim;DataCriacao;EstadoAtual");

                foreach (var tarefa in tarefasConcluidas)
                {
                    string linha = string.Join(";",
                        tarefa.Id,
                        tarefa.IdGestor?.id.ToString() ?? "N/A",
                        tarefa.IdProgramador?.id.ToString() ?? "N/A",
                        tarefa.OrdemExecucao,
                        tarefa.Descricao ?? "N/A",
                        tarefa.DataPrevistaInicio.ToString("yyyy-MM-dd"),
                        tarefa.DataPrevistaFim.ToString("yyyy-MM-dd"),
                        tarefa.TipoTarefa?.Id.ToString() ?? "N/A",
                        tarefa.StoryPoints,
                        tarefa.DataRealInicio?.ToString("yyyy-MM-dd") ?? "N/A",
                        tarefa.DataRealFim?.ToString("yyyy-MM-dd") ?? "N/A",
                        tarefa.DataCriacao.ToString("yyyy-MM-dd"),
                        tarefa.EstadoAtual.ToString()
                    );

                    sb.AppendLine(linha);
                }

                File.WriteAllText(caminhoFicheiro, sb.ToString(), new UTF8Encoding(true));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao exportar tarefas para CSV: " + ex.Message);
            }
        }

    }
}
