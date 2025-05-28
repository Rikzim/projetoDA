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
    public partial class frmKanban : Form
    {
        Utilizador utilizadorRecebido;
        public frmKanban(Utilizador utilizadorRecebido)
        {
            InitializeComponent();

            this.utilizadorRecebido = utilizadorRecebido;

            label1.Text = "Bem-Vindo, " + utilizadorRecebido.nome;

            // Verifica se o utilizador é um gestor
            if (utilizadorRecebido is Programador programador)
            {
                // Se o utilizador for um gestor, mostra o botão de gestão de utilizadores
                btNova.Enabled = false;
                utilizadoresToolStripMenuItem.Enabled = false;
            }

            ReloadData();
        }

        private void btNova_Click(object sender, EventArgs e)
        {
            // Abre o formulário de nova tarefa
            if (utilizadorRecebido is Gestor gestor)
            {
                frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido);
                detalhesTarefa.ShowDialog();
            }
            else
            {
                // Se o utilizador não for um gestor, mostra uma mensagem de erro
                MessageBox.Show("Apenas gestores podem criar novas tarefas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereUtilizadores gereUtilizadores = new frmGereUtilizadores(utilizadorRecebido);
            gereUtilizadores.ShowDialog();
        }

        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abre o formulário de gestão de tipos de tarefas
            frmGereTiposTarefas gereTiposTarefas = new frmGereTiposTarefas();
            gereTiposTarefas.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Fecha a aplicação
            DialogResult result = MessageBox.Show("Tem a certeza que deseja sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            // Atualiza as listas de tarefas que estão no estado Todo, Doing e Done
            lstTodo.DataSource = null;
            lstTodo.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.ToDo);
            lstDoing.DataSource = null;
            lstDoing.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.Doing);
            lstDone.DataSource = null;
            lstDone.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.Done);
        }

        private void lstTodo_DoubleClick(object sender, EventArgs e)
        {
            var tarefaSelecionada = lstTodo.SelectedItem as Tarefa;
            if (tarefaSelecionada != null)
            {
                frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido, tarefaSelecionada);
                detalhesTarefa.ShowDialog();
                ReloadData(); // Atualiza a lista após possíveis alterações
            }
        }

        private void btSetDoing_Click(object sender, EventArgs e)
        {
            var tarefaSelecionada = lstTodo.SelectedItem as Tarefa;

            if (tarefaSelecionada != null)
            {
                // Muda o estado da tarefa para Doing
                TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.Doing);
                MessageBox.Show("Tarefa movida para Doing.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadData(); // Atualiza a lista após a mudança de estado
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para mover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btSetDone_Click(object sender, EventArgs e)
        {
            var tarefaSelecionada = lstTodo.SelectedItem as Tarefa;

            if (tarefaSelecionada != null)
            {
                // Muda o estado da tarefa para Doing
                TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.Done);
                MessageBox.Show("Tarefa movida para Doing.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadData(); // Atualiza a lista após a mudança de estado
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para mover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
