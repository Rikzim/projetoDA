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
using iTasks.Models;

namespace iTasks
{
    public partial class frmDetalhesTarefa : Form
    {
        // Inicializa os campos necessários
        Utilizador utilizadorRecebido;
        Tarefa tarefaSelecionada;
        public frmDetalhesTarefa(Utilizador utilizadorRecebido, Tarefa tarefaSelecionada = null)
        {
            InitializeComponent();

            // Recebe o utilizador e a tarefa selecionada (se houver)
            this.utilizadorRecebido = utilizadorRecebido;
            this.tarefaSelecionada = tarefaSelecionada;

            if (utilizadorRecebido is Programador)
            {
                readOnlyUtilizador();
            }

            //Inicializa os comboboxes com os dados necessários
            InicializarComboboxes();

            // Verifica se uma tarefa foi selecionada
            if (tarefaSelecionada != null)
            {
                // Se uma tarefa foi selecionada, preenche os campos com os dados da tarefa
                PreencherCamposImutaveis();
                PreencherCamposMutaveis();
            }
            else
            {
                // Se não há tarefa selecionada, prepara o formulário para criar uma nova tarefa
                PrepararNovaTarefa();
            }
        }

        private void readOnlyUtilizador() 
        {
            // Se o utilizador for um programador, desabilita CRUD de tarefas
            btGravar.Enabled = false;
            btEditarTarefa.Enabled = false;
            btApagarTarefa.Enabled = false;

            // Campos readonly
            txtDesc.ReadOnly = true;
            txtOrdem.ReadOnly = true;
            txtStoryPoints.ReadOnly = true;

            // ComboBoxes readonly
            cbProgramador.Enabled = false;
            cbTipoTarefa.Enabled = false;

            // DateTimePickers readonly
            dtInicio.Enabled = false;
            dtFim.Enabled = false;

        }
        private void InicializarComboboxes()
        {
            // Preenche os comboboxes com os dados necessários
            cbTipoTarefa.DataSource = null;
            cbTipoTarefa.DataSource = TipoTarefaController.ListarTipoTarefa();

            cbProgramador.DataSource = null;
            cbProgramador.DataSource = ProgramadorController.ListarProgramadoresPorGestor(utilizadorRecebido);
        }

        private void PreencherCamposImutaveis()
        {
            // Preenche os campos imutáveis com os dados da tarefa selecionada
            txtId.Text = tarefaSelecionada.Id.ToString();
            txtEstado.Text = tarefaSelecionada.EstadoAtual.ToString();
            txtDataCriacao.Text = tarefaSelecionada.DataCriacao.ToString("dd/MM/yyyy");

            txtDataRealini.Text = tarefaSelecionada.DataRealInicio?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
            txtdataRealFim.Text = tarefaSelecionada.DataRealFim?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
        }

        private void PreencherCamposMutaveis()
        {
            // Preenche os campos mutáveis com os dados da tarefa selecionada
            txtDesc.Text = tarefaSelecionada.Descricao;
            cbTipoTarefa.SelectedItem = tarefaSelecionada.TipoTarefa;
            cbProgramador.SelectedItem = tarefaSelecionada.IdProgramador;
            txtOrdem.Text = tarefaSelecionada.OrdemExecucao.ToString();
            txtStoryPoints.Text = tarefaSelecionada.StoryPoints.ToString();
            dtInicio.Value = tarefaSelecionada.DataPrevistaInicio;
            dtFim.Value = tarefaSelecionada.DataPrevistaFim;
        }

        private void PrepararNovaTarefa()
        {
            // Prepara o formulário para criar uma nova tarefa
            txtId.Text = TarefaController.countTarefas().ToString();

            txtDesc.Clear();
            cbTipoTarefa.SelectedIndex = -1;
            cbProgramador.SelectedIndex = -1;
            txtOrdem.Clear();
            txtStoryPoints.Clear();
            dtInicio.Value = DateTime.Now;
            dtFim.Value = DateTime.Now;
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            try
            {
                Programador programador = (Programador)cbProgramador.SelectedItem;
                Gestor gestor = (Gestor)utilizadorRecebido;
                TipoTarefa tipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;
                TarefaController.GravarTarefa(
                    gestor,
                    programador,
                    Convert.ToInt32(txtOrdem.Text),
                    txtDesc.Text,
                    dtInicio.Value,
                    dtFim.Value,
                    tipoTarefa,
                    Convert.ToInt32(txtStoryPoints.Text),
                    DateTime.Now,
                    Tarefa.Estado.ToDo);

                MessageBox.Show("Tarefa gravada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEditarTarefa_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtém a tarefa selecionada do formulário
                Tarefa tarefaSelecionada = this.tarefaSelecionada;

                // Verifica se a tarefa selecionada não é nula
                if (tarefaSelecionada != null)
                {
                    Gestor gestor = (Gestor)utilizadorRecebido;
                    Programador programador = (Programador)cbProgramador.SelectedItem;
                    TipoTarefa tipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;

                    TarefaController.EditarTarefa(
                        tarefaSelecionada,
                        gestor,
                        programador,
                        Convert.ToInt32(txtOrdem.Text),
                        txtDesc.Text,
                        dtInicio.Value,
                        dtFim.Value,
                        tipoTarefa,
                        Convert.ToInt32(txtStoryPoints.Text)
                    );
                    MessageBox.Show("Tarefa editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btApagarTarefa_Click(object sender, EventArgs e)
        {
            try
            {
                Tarefa tarefaSelecionada = this.tarefaSelecionada;
                // Verifica se a tarefa selecionada não é nula
                TarefaController.EliminarTarefa(tarefaSelecionada);

                MessageBox.Show("Tarefa apagada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar a tarefa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Erro ao fechar o formulário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
