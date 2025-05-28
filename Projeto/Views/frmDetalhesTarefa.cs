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

            MessageBox.Show($"Utilizador: {utilizadorRecebido}");
            // Se a tarefa selecionada não for nula, preenche os campos com os dados da tarefa
            if (tarefaSelecionada != null)
            {
                this.tarefaSelecionada = tarefaSelecionada;
                txtOrdem.Text = tarefaSelecionada.OrdemExecucao.ToString();
                txtDesc.Text = tarefaSelecionada.Descricao;
                dtInicio.Value = tarefaSelecionada.DataPrevistaInicio;
                dtFim.Value = tarefaSelecionada.DataPrevistaFim;
                cbTipoTarefa.SelectedItem = tarefaSelecionada.TipoTarefa;
                txtStoryPoints.Text = tarefaSelecionada.StoryPoints.ToString();
                cbProgramador.SelectedItem = tarefaSelecionada.IdProgramador;
            }
            else
            {
                // Se não houver uma tarefa selecionada, define os valores padrão
                txtOrdem.Text = TarefaController.countTarefas().ToString();
                dtInicio.Value = DateTime.Now;
                dtFim.Value = DateTime.Now.AddDays(1);
            }
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            Programador programador = (Programador)cbProgramador.SelectedItem;
            Gestor gestor = (Gestor)utilizadorRecebido;
            MessageBox.Show($"Gestor: {gestor}\n Programador: {programador}");
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
