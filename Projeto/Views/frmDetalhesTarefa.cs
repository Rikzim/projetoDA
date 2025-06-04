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
        Utilizador utilizadorRecebido;
        Tarefa tarefaSelecionada;
        public frmDetalhesTarefa(Utilizador utilizadorRecebido, Tarefa tarefaSelecionada = null)
        {
            InitializeComponent();

            // Define o utilizador recebido
            this.utilizadorRecebido = utilizadorRecebido;
            if (utilizadorRecebido is Programador programador)
            {
                btGravar.Enabled = false;
                btEditarTarefa.Enabled = false;
                btApagarTarefa.Enabled = false;
            }

            //Atualizar a combobox com os tipos de tarefa
            cbTipoTarefa.DataSource = null;
            cbTipoTarefa.DataSource = TipoTarefaController.ListarTipoTarefa();
            //Atualizar a combobox com os programadores
            cbProgramador.DataSource = null;
            cbProgramador.DataSource = ProgramadorController.ListarProgramadores();
            // Se a tarefa selecionada não for nula, preenche os campos com os dados da tarefa
            if (tarefaSelecionada != null)
            {
                this.tarefaSelecionada = tarefaSelecionada;
                // Campos Imutáveis
                txtId.Text = tarefaSelecionada.Id.ToString();
                txtEstado.Text = tarefaSelecionada.EstadoAtual.ToString();
                txtDataCriacao.Text = tarefaSelecionada.DataCriacao.ToString("dd/MM/yyyy");
                if (tarefaSelecionada.DataRealInicio != null)
                    txtDataRealini.Text = tarefaSelecionada.DataRealInicio.Value.ToString("dd/MM/yyyy HH:mm");
                else
                    txtDataRealini.Text = "N/A"; // Se não houver data real de início
                if (tarefaSelecionada.DataRealFim != null)
                    txtdataRealFim.Text = tarefaSelecionada.DataRealFim.Value.ToString("dd/MM/yyyy HH:mm");
                else
                    txtdataRealFim.Text = "N/A"; // Se não houver data real de fim
                // Campos Mutáveis
                txtDesc.Text = tarefaSelecionada.Descricao;
                cbTipoTarefa.SelectedItem = tarefaSelecionada.TipoTarefa;
                cbProgramador.SelectedItem = tarefaSelecionada.IdProgramador;
                txtOrdem.Text = tarefaSelecionada.OrdemExecucao.ToString();
                txtStoryPoints.Text = tarefaSelecionada.StoryPoints.ToString();
                dtInicio.Value = tarefaSelecionada.DataPrevistaInicio;
                dtFim.Value = tarefaSelecionada.DataPrevistaFim; 
            }
            else
            {
                // Campos Imutáveis
                txtId.Text = TarefaController.countTarefas().ToString();
                // Campos Mutáveis
                txtDesc.Clear();
                cbTipoTarefa.SelectedIndex = -1;
                cbProgramador.SelectedIndex = -1;
                txtOrdem.Clear();
                txtStoryPoints.Clear();
                dtInicio.Value = DateTime.Now;
                dtFim.Value = DateTime.Now; 
            }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Interrompe a execução se ocorrer um erro
            }
            finally
            {
                MessageBox.Show("Tarefa gravada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Interrompe a execução se ocorrer um erro
            }
            finally
            {
                MessageBox.Show("Tarefa editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btApagarTarefa_Click(object sender, EventArgs e)
        {
            try
            {
                Tarefa tarefaSelecionada = this.tarefaSelecionada;
                // Verifica se a tarefa selecionada não é nula
                TarefaController.EliminarTarefa(tarefaSelecionada);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar a tarefa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Interrompe a execução se ocorrer um erro
            }
            finally
            {
                MessageBox.Show("Tarefa apagada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetalhesTarefa_Load(object sender, EventArgs e)
        {

        }
    }
}
