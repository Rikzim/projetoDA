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
        public static void EliminarTarefa(Tarefa tarefaSelecionada)
        {
            BasedeDados db = BasedeDados.Instance;
            try
            {
                if (tarefaSelecionada != null)
                {
                    // Remove a tarefa selecionada da base de dados
                    db.Tarefa.Remove(tarefaSelecionada);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao eliminar tarefa: " + ex.Message);
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
            try
            {
                if (tarefaSelecionada != null) // Verifica se a tarefa selecionada não é nula
                {
                    if (tarefaSelecionada.IdProgramador.id == utilizadorRecebido.id) // Verifica se o utilizador é o programador responsável pela tarefa
                    {
                        tarefaSelecionada.EstadoAtual = estado; // Define o novo estado da tarefa
                        if (estado == Tarefa.Estado.Done) // Se o estado for Done, define as datas a data real fim
                        {
                            tarefaSelecionada.DataRealFim = DateTime.Now;
                        }
                        else if (estado == Tarefa.Estado.Doing) // Se o estado for Doing, define a data real início
                        {
                            tarefaSelecionada.DataRealInicio = DateTime.Now;
                        }
                        else if (estado == Tarefa.Estado.ToDo) // Se o estado for ToDo, define as datas real início e fim como nulas
                        {
                            tarefaSelecionada.DataRealInicio = null;
                            tarefaSelecionada.DataRealFim = null;
                        }
                        else
                        {
                            throw new Exception("Estado inválido. Deve ser ToDo, Doing ou Done."); // Lanca uma excecao se o estado for inválido
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Apenas o programador responsável pela tarefa pode alterar o seu estado."); // Lanca uma excecao se o utilizador não for o programador responsável pela tarefa
                    }
                }
                else
                {
                    throw new Exception("Nenhuma tarefa selecionada."); // Lanca uma excecao se a tarefa selecionada for nula
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao mudar estado da tarefa: " + ex.Message);
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
                    string linha = string.Join(",",
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

        public static double EstimarTempoTotalToDo()
        {
            BasedeDados db = BasedeDados.Instance;

            // 1. Calcule médias por StoryPoints das tarefas concluídas
            var concluidas = db.Tarefa
                .Where(t => t.EstadoAtual == Tarefa.Estado.Done && t.DataRealInicio != null && t.DataRealFim != null)
                .ToList();

            var mediasPorSP = concluidas
                .GroupBy(t => t.StoryPoints)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(t => (t.DataRealFim.Value - t.DataRealInicio.Value).TotalHours)
                );

            // 2. Para cada tarefa ToDo, estime o tempo
            var tarefasToDo = db.Tarefa
                .Where(t => t.EstadoAtual == Tarefa.Estado.ToDo)
                .ToList();

            double totalHoras = 0;

            foreach (var tarefa in tarefasToDo)
            {
                int sp = tarefa.StoryPoints;
                if (mediasPorSP.ContainsKey(sp))
                {
                    totalHoras += mediasPorSP[sp];
                }
                else if (mediasPorSP.Count > 0)
                {
                    // Busca a média mais próxima
                    int spMaisProximo = mediasPorSP.Keys.OrderBy(k => Math.Abs(k - sp)).First();
                    totalHoras += mediasPorSP[spMaisProximo];
                }
                // Se não houver nenhuma tarefa concluída, não soma nada
            }

            // Retorna o tempo total estimado como TimeSpan
            return totalHoras;
        }

        public static double CalcularMediaHorasPorStoryPoints(int storyPoints)
        {
            BasedeDados db = BasedeDados.Instance;

            // Busca tarefas concluídas com DataRealInicio e DataRealFim definidos
            var concluidas = db.Tarefa
                .Where(t => t.EstadoAtual == Tarefa.Estado.Done && t.DataRealInicio != null && t.DataRealFim != null)
                .ToList();

            // Agrupa por StoryPoints e calcula a média de horas
            var mediasPorSP = concluidas
                .GroupBy(t => t.StoryPoints)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(t => (t.DataRealFim.Value - t.DataRealInicio.Value).TotalHours)
                );

            if (mediasPorSP.Count == 0)
                return 0; // Nenhuma tarefa concluída

            if (mediasPorSP.ContainsKey(storyPoints))
            {
                return mediasPorSP[storyPoints];
            }
            else
            {
                // Busca a média mais próxima
                int spMaisProximo = mediasPorSP.Keys.OrderBy(k => Math.Abs(k - storyPoints)).First();
                return mediasPorSP[spMaisProximo];
            }
        }

    }
}
