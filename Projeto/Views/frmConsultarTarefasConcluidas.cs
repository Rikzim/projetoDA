using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTasks.Controllers;

namespace iTasks
{
    public partial class frmConsultarTarefasConcluidas : Form
    {
        // Variável para guardar o utilizador/programador passado ao formulário
        Utilizador utilizadorRecebido;

        // Construtor do formulário que recebe o utilizador como parâmetro
        public frmConsultarTarefasConcluidas(Utilizador utilizador)
        {
            InitializeComponent();
            try
            {
                this.utilizadorRecebido = utilizador;

                // Obtém a lista de tarefas concluídas ("Done") para o programador
                var tarefas = TarefaController.ListarTarefasPorEstadoProgramador(Tarefa.Estado.Done, utilizadorRecebido);

                // Cria uma lista com os dados formatados para exibir na tabela
                var tarefasComTempo = tarefas.Select(t => new
                {
                    t.Id,
                    IdGestor = t.IdGestor?.id ?? 0, // Se o gestor for nulo, usa 0
                    IdProgramador = t.IdProgramador?.id ?? 0, // Se o programador for nulo, usa 0
                    t.OrdemExecucao, 
                    t.Descricao,
                    t.DataPrevistaInicio,
                    t.DataPrevistaFim,
                    t.TipoTarefa,
                    t.StoryPoints,
                    DataInicio = t.DataRealInicio?.ToString("dd/MM/yyyy HH:mm") ?? "N/A", // Formata a data de início real
                    DataFim = t.DataRealFim?.ToString("dd/MM/yyyy HH:mm") ?? "N/A", // Formata a data de fim real
                    t.DataCriacao,
                    t.EstadoAtual,
                    DiasExecucao = t.DataRealInicio != null && t.DataRealFim != null
                        ? (t.DataRealFim.Value - t.DataRealInicio.Value).TotalDays.ToString("0.## Dias") // Calcula os dias de execução
                        : "N/A"
                }).ToList();

                // Define a fonte de dados do DataGridView com as tarefas formatadas
                gvTarefasConcluidas.DataSource = tarefasComTempo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btFechar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao fechar a janela: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}