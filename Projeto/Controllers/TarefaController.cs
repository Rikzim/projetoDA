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
        //Método para gravar uma nova tarefa na base de dados
        public static void GravarTarefa(Gestor idGestor, Programador idProgramador, int ordemExecucao, string descricao,
            DateTime dataPrevistaInicio, DateTime dataPrevistaFim, TipoTarefa tipoTarefa, int storyPoints, DateTime dataCriacao, Estado estadoAtual)
        {
            // Cria uma instância da base de dados
            BasedeDados db = BasedeDados.Instance;

            // Verifica se já existe uma tarefa com a mesma ordem para o mesmo programador e gestor
            bool ordemExistente = db.Tarefa.Any(t =>
                t.IdGestor.id == idGestor.id &&
                t.IdProgramador.id == idProgramador.id &&
                t.OrdemExecucao == ordemExecucao
            );

            if (ordemExistente)
            {
                throw new Exception("Já existe uma tarefa com esta ordem de execução para este programador.");
            }

            // Cria uma nova tarefa e adiciona-a à tabela de tarefas
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
        // Método para editar uma tarefa existente na base de dados
        public static void EditarTarefa(Tarefa tarefaSelecionada, Gestor idGestor, Programador idProgramador, int ordemExecucao, string descricao,
            DateTime dataPrevistaInicio, DateTime dataPrevistaFim, TipoTarefa tipoTarefa, int storyPoints)
        {
            // Cria uma instância da base de dados
            BasedeDados db = BasedeDados.Instance;

            // Verifica se já existe uma tarefa com a mesma ordem para o mesmo programador e gestor
            bool ordemExistente = db.Tarefa.Any(t =>
                t.Id != tarefaSelecionada.Id && // Ignora a própria tarefa
                t.IdGestor.id == idGestor.id &&
                t.IdProgramador.id == idProgramador.id &&
                t.OrdemExecucao == ordemExecucao);

            if (ordemExistente)
            {
                throw new Exception("Já existe uma tarefa com esta ordem de execução para este programador.");
            }

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
        public static void EliminarTarefa(Tarefa TarefaSelecionada)
        {
            // Cria uma instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Verifica se a tarefa selecionada é nula
            if (TarefaSelecionada != null)
            {
                // Remove a tarefa selecionada da base de dados
                db.Tarefa.Remove(TarefaSelecionada);
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Nenhuma tarefa selecionada para eliminar.");
            }
        }
        // Método para listar todas as tarefas na base de dados
        public static int countTarefas()
        {
            // Cria uma instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de tarefas na base de dados e adiciona 1, começando em 1 se não houver nenhuma
            int count = db.Tarefa.Count();
            return count + 1;
        }
        // Método para contar o número de tarefas por estado de um programador específico
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
        // Método para mudar o estado de uma tarefa
        public static void MudarEstadoTarefa(Tarefa tarefaSelecionada, Estado estado, Utilizador utilizadorRecebido)
        {
            // Valida se a tarefa selecionada é nula e se o utilizador é o programador responsável pela tarefa
            if (tarefaSelecionada == null)
                throw new Exception("Nenhuma tarefa selecionada.");

            if (tarefaSelecionada.IdProgramador.id != utilizadorRecebido.id)
                throw new Exception("Apenas o programador responsável pela tarefa pode alterar o seu estado.");

            // Atualiza o estado da tarefa selecionada
            tarefaSelecionada.EstadoAtual = estado;

            // Atualiza as datas de início e fim reais com base no novo estado
            switch (estado)
            {
                case Tarefa.Estado.Done:
                    tarefaSelecionada.DataRealFim = DateTime.Now;
                    break;
                case Tarefa.Estado.Doing:
                    tarefaSelecionada.DataRealInicio = DateTime.Now;
                    break;
                case Tarefa.Estado.ToDo:
                    tarefaSelecionada.DataRealInicio = null;
                    tarefaSelecionada.DataRealFim = null;
                    break;
                default:
                    throw new Exception("Estado inválido. Deve ser ToDo, Doing ou Done.");
            }

            // Salva as alterações na base de dados
            BasedeDados.Instance.SaveChanges();
        }
        public static List<Tarefa> ListarTarefasPorEstado(Tarefa.Estado estado)
        {
            // Cria uma instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            return db.Tarefa
                .Include(t => t.IdGestor)
                .Include(t => t.IdProgramador)
                .Include(t => t.TipoTarefa)
                .Where(t => t.EstadoAtual == estado)
                .ToList();
        }
        public static List<Tarefa> ListarTarefasPorEstadoProgramador(Tarefa.Estado estado, Utilizador utilizadorLogado)
        {
            // Cria uma instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            if (utilizadorLogado is Gestor)
            {
                // Se o utilizador for um gestor, retorna todas as tarefas do estado especificado
                return db.Tarefa
                    .Include(t => t.IdGestor)
                    .Include(t => t.IdProgramador)
                    .Include(t => t.TipoTarefa)
                    .Where(t => t.EstadoAtual == estado)
                    .ToList();
            }
            return db.Tarefa
                .Include(t => t.IdGestor)
                .Include(t => t.IdProgramador)
                .Include(t => t.TipoTarefa)
                .Where(t => t.EstadoAtual == estado && t.IdProgramador.id == utilizadorLogado.id)
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
                // Verifica se o utilizador selecionou um caminho válido
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return false;

                // Obtém o caminho do ficheiro selecionado
                string caminhoFicheiro = saveFileDialog.FileName;

                // Guarda as tarefas concluídas do gestor selecionado
                var tarefasConcluidas = ListarTarefasPorEstado(Tarefa.Estado.Done);

                // Cria o conteúdo do CSV
                var sb = new StringBuilder();
                sb.AppendLine("sep=;"); // Define o separador de campos como vírgula
                sb.AppendLine("IdTarefa;IdGestor;IdProgramador;OrdemExecucao;Descricao;DataPrevistaInicio;DataPrevistaFim;IdTipoTarefa;StoryPoints;DataRealInicio;DataRealFim;DataCriacao;EstadoAtual");

                // Itera sobre as tarefas concluídas e adiciona cada uma como uma linha no CSV
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
                    // Adiciona a linha ao StringBuilder
                    sb.AppendLine(linha);
                }
                // Escreve o conteúdo do StringBuilder no ficheiro CSV
                File.WriteAllText(caminhoFicheiro, sb.ToString(), new UTF8Encoding(true));
                return true;
            }
            catch (Exception ex)
            {
                // Lança uma exceção se ocorrer um erro ao exportar as tarefas para CSV
                throw new Exception("Erro ao exportar tarefas para CSV: " + ex.Message);
            }
        }

        /*public static bool ImportarTarefas(string caminhoFicheiro, Gestor gestor)
        {
            try
            {
                // Verifica se o ficheiro existe
                if (!File.Exists(caminhoFicheiro))
                    throw new FileNotFoundException("O ficheiro especificado não foi encontrado.");
                // Lê todas as linhas do ficheiro
                var linhas = File.ReadAllLines(caminhoFicheiro);
                // Verifica se o ficheiro está vazio
                if (linhas.Length == 0)
                    throw new Exception("O ficheiro está vazio.");
                // Cria uma instância da base de dados
                BasedeDados db = BasedeDados.Instance;
                // Itera sobre cada linha do ficheiro, ignorando a primeira linha (cabeçalho)
                for (int i = 1; i < linhas.Length; i++)
                {
                    var colunas = linhas[i].Split(';');
                    // Verifica se a linha tem o número correto de colunas
                    if (colunas.Length != 13)
                        throw new Exception($"Linha {i + 1} inválida: {linhas[i]}");
                    // Cria uma nova tarefa e adiciona à base de dados
                    db.Tarefa.Add(new Tarefa
                    (
                        db.Gestor.Find(gestor.id),
                        db.Programador.Find(int.Parse(colunas[2])),
                        int.Parse(colunas[3]),
                        colunas[4],
                        DateTime.Parse(colunas[5]),
                        DateTime.Parse(colunas[6]),
                        db.TipoTarefa.Find(int.Parse(colunas[7])),
                        int.Parse(colunas[8]),
                        DateTime.Parse(colunas[11]),
                        (Estado)Enum.Parse(typeof(Estado), colunas[12])
                    ));
                }
                // Salva as alterações na base de dados
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Lança uma exceção se ocorrer um erro ao importar as tarefas do ficheiro texto
                throw new Exception("Erro ao importar tarefas do ficheiro texto: " + ex.Message);
            }
        }*/ // Função para importar tarefas

        public static double EstimarTempoTotalToDo()
        {
            
            BasedeDados db = BasedeDados.Instance;

            // 1. Calcule médias por StoryPoints das tarefas concluídas

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

            // 2. Para cada tarefa ToDo, estime o tempo
            var tarefasToDo = db.Tarefa
                .Where(t => t.EstadoAtual == Tarefa.Estado.ToDo)
                .ToList();

            
            double totalHoras = 0;

            // Para cada tarefa ToDo, soma o tempo estimado com base na média de StoryPoints
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


            // Se não houver tarefas concluídas, retorna 0
            if (mediasPorSP.Count == 0)
                return 0; // Nenhuma tarefa concluída

            // Se a média para o StoryPoints específico existir, retorna essa média
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
        public static bool VerificarOrdem(Tarefa tarefaSelecionada, Estado novoEstado)
        {
            //Instanciar a base de dados
            BasedeDados db = BasedeDados.Instance;

            //Guardar as tarefas anteriores do programador selecionado
            var tarefasAnteriores = db.Tarefa
                .Where(t => t.IdProgramador.id == tarefaSelecionada.IdProgramador.id
                         && t.OrdemExecucao < tarefaSelecionada.OrdemExecucao)
                .OrderBy(t => t.OrdemExecucao)
                .ToList();

            // Se não houver tarefas
            if (!tarefasAnteriores.Any())
                return true;

            // Verificar se a transição de estado é válida
            switch (novoEstado)
            {
                case Estado.ToDo:
                    return true;

                case Estado.Doing:
                    return tarefasAnteriores.All(t => t.EstadoAtual == Estado.Doing ||
                                                      t.EstadoAtual == Estado.Done);

                case Estado.Done:
                    return tarefasAnteriores.All(t => t.EstadoAtual == Estado.Done);

                default:
                    throw new Exception("Estado inválido: " + novoEstado);
            }
        }
    }
}
