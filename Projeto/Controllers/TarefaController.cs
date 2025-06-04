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
                // Criar diálogo para escolher a pasta
                var folderBrowserDialog = new FolderBrowserDialog();
                DialogResult resultado = folderBrowserDialog.ShowDialog();

                if (resultado != DialogResult.OK)
                    return false;

                string pastaDestino = folderBrowserDialog.SelectedPath;

                // Gerar nome do ficheiro com nome do gestor e timestamp
                string nomeGestorLimpo = string.Concat(gestor.nome.Split(Path.GetInvalidFileNameChars()));
                string nomeFicheiro = $"TarefasConcluidas_{nomeGestorLimpo}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                string caminhoFicheiro = Path.Combine(pastaDestino, nomeFicheiro);

                // Obter tarefas concluídas do gestor
                var tarefasConcluidas = ListarTarefasPorEstado(Tarefa.Estado.Done, gestor);

                // Construir conteúdo CSV
                var sb = new StringBuilder();
                sb.AppendLine("Programador;Descricao;DataPrevistaInicio;DataPrevistaFim;TipoTarefa;DataRealInicio;DataRealFim");

                foreach (var tarefa in tarefasConcluidas)
                {
                    string linha = string.Join(";",
                        tarefa.IdProgramador?.nome ?? "",
                        tarefa.Descricao ?? "",
                        tarefa.DataPrevistaInicio.ToString("yyyy-MM-dd"),
                        tarefa.DataPrevistaFim.ToString("yyyy-MM-dd"),
                        tarefa.TipoTarefa?.Nome ?? "",
                        tarefa.DataRealInicio?.ToString("yyyy-MM-dd") ?? "",
                        tarefa.DataRealFim?.ToString("yyyy-MM-dd") ?? ""
                    );

                    sb.AppendLine(linha);
                }

                // Escrever no ficheiro
                File.WriteAllText(caminhoFicheiro, sb.ToString(), new UTF8Encoding(true));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao exportar CSV: " + ex.Message);
                return false;
            }
        }

<<<<<<< Updated upstream


=======
        public static bool VerificarOrdem(Tarefa tarefaSelecionada, Tarefa.Estado estado)
        {
            try
            {
                BasedeDados db = BasedeDados.Instance;
                // Verificação de pré-requisito de ordem de execução
                if (estado == Tarefa.Estado.Doing || estado == Tarefa.Estado.Done)
                {
                    var tarefasAnteriores = db.Tarefa
                        .Where(t => t.IdProgramador.id == tarefaSelecionada.IdProgramador.id
                                 && t.OrdemExecucao < tarefaSelecionada.OrdemExecucao)
                        .ToList();

                    bool todasAnterioresConcluidas = tarefasAnteriores.All(t => t.EstadoAtual == Tarefa.Estado.Done);

                    if (!todasAnterioresConcluidas)
                    {
                        return false;
                    }
                }
                return true; // Todas as tarefas anteriores estão concluídas
            }
            catch (Exception ex)
            { 
                throw new Exception("Erro ao verificar ordem de execução: " + ex.Message);
            }
        }
>>>>>>> Stashed changes
    }
}
