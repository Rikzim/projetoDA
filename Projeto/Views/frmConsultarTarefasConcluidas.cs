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
        Utilizador utilizadorRecebido;
        public frmConsultarTarefasConcluidas(Utilizador utilizador)
        {
            InitializeComponent();
            try
            {
                this.utilizadorRecebido = utilizador;
                var tarefas = TarefaController.ListarTarefasPorEstadoProgramador(Tarefa.Estado.Done, utilizadorRecebido);

                var tarefasComTempo = tarefas.Select(t => new
                {
                    t.Id,
                    IdGestor = t.IdGestor?.id ?? 0,
                    IdProgramador = t.IdProgramador?.id ?? 0,
                    t.OrdemExecucao,
                    t.Descricao,
                    t.DataPrevistaInicio,
                    t.DataPrevistaFim,
                    t.TipoTarefa,
                    t.StoryPoints,
                    DataInicio = t.DataRealInicio?.ToString("dd/MM/yyyy HH:mm") ?? "N/A",
                    DataFim = t.DataRealFim?.ToString("dd/MM/yyyy HH:mm") ?? "N/A",
                    t.DataCriacao,
                    t.EstadoAtual,
                    DiasExecucao = t.DataRealInicio != null && t.DataRealFim != null
                        ? (t.DataRealFim.Value - t.DataRealInicio.Value).TotalDays.ToString("0.## Dias")
                        : "N/A"
                }).ToList();

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