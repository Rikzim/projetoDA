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

            // Preenche o comboBox com os tipos de tarefa
            cbTipoTarefa.Items.Add(new TipoTarefa());

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
                txtId.Text = TarefaController.countTarefasEstado(Tarefa.Estado.ToDo).ToString();
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
            Programador programador = (Programador)cbProgramador.SelectedItem;
            Gestor gestor = (Gestor)utilizadorRecebido;
            TarefaController.GravarTarefa(
                gestor, 
                programador, 
                Convert.ToInt32(txtOrdem.Text),
                txtDesc.Text,
                dtInicio.Value,
                dtFim.Value,
                (TipoTarefa)cbTipoTarefa.SelectedItem,
                Convert.ToInt32(txtStoryPoints.Text),
                DateTime.Now, 
                Tarefa.Estado.ToDo);
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
