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
            lstTodo.SelectedIndex = -1; // Limpa a seleção da lista de tarefas
            // Abre o formulário de nova tarefa
            if (utilizadorRecebido is Gestor gestor)
            {
                frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido);
                detalhesTarefa.ShowDialog();
                ReloadData(); // Atualiza a lista após a criação de uma nova tarefa
            }
            else
            {
                // Se o utilizador não for um gestor, mostra uma mensagem de erro
                MessageBox.Show("Apenas gestores podem criar novas tarefas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Eventos do Menu tool Strip 
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
        // Eventos do KanBan
        private void btSetDoing_Click(object sender, EventArgs e)
        {
            var tarefaSelecionada = lstTodo.SelectedItem as Tarefa;

            if (tarefaSelecionada != null)
            {
                if (TarefaController.countTarefasPorEstadoProgramador(Tarefa.Estado.Doing, utilizadorRecebido) < 2)
                {
                    // Muda o estado da tarefa para Done
                    TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.Doing, utilizadorRecebido);
                    MessageBox.Show("Tarefa movida para Doing.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadData(); // Atualiza a lista após a mudança de estado
                }
                else
                {
                    MessageBox.Show("Não pode mover mais de 2 tarefas para Doing.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para mover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btSetDone_Click(object sender, EventArgs e)
        {
            var tarefaSelecionada = lstDoing.SelectedItem as Tarefa;

            if (tarefaSelecionada != null)
            {
                // Muda o estado da tarefa para Done
                TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.Done, utilizadorRecebido);
                MessageBox.Show("Tarefa movida para Done.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadData(); // Atualiza a lista após a mudança de estado
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para mover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btSetTodo_Click(object sender, EventArgs e)
        {
            var tarefaSelecionada = lstDoing.SelectedItem as Tarefa;

            if (tarefaSelecionada != null)
            {
                // Muda o estado da tarefa para ToDo
                TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.ToDo, utilizadorRecebido);
                MessageBox.Show("Tarefa movida para ToDo.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadData(); // Atualiza a lista após a mudança de estado
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para mover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Eventos de duplo clique nas listas de tarefas
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
        private void lstDoing_DoubleClick(object sender, EventArgs e)
        {
            var tarefaSelecionada = lstDoing.SelectedItem as Tarefa;
            if (tarefaSelecionada != null)
            {
                frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido, tarefaSelecionada);
                detalhesTarefa.ShowDialog();
                ReloadData(); // Atualiza a lista após possíveis alterações
            }
        }
        private void lstDone_DoubleClick(object sender, EventArgs e)
        {
            var tarefaSelecionada = lstDone.SelectedItem as Tarefa;
            if (tarefaSelecionada != null)
            {
                frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido, tarefaSelecionada);
                detalhesTarefa.ShowDialog();
                ReloadData(); // Atualiza a lista após possíveis alterações
            }
        }
        private void ReloadData()
        {
            // Atualiza as listas de tarefas que estão no estado Todo, Doing e Done
            lstTodo.DataSource = null;
            lstTodo.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.ToDo, utilizadorRecebido);
            lstDoing.DataSource = null;
            lstDoing.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.Doing, utilizadorRecebido);
            lstDone.DataSource = null;
            lstDone.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.Done, utilizadorRecebido);
            // Limpa as seleções das listas
            lstDoing.SelectedIndex = -1;
            lstDone.SelectedIndex = -1;
            lstTodo.SelectedIndex = -1;
            // Atualiza os labels com o número de tarefas em cada estado
            label2.Text = lstTodo.Items.Count.ToString();
            label3.Text = lstDoing.Items.Count.ToString();
            label4.Text = lstDone.Items.Count.ToString();
        }

        //Abre a janela de consulta de tarefas concluídas
        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
            frmConsultarTarefasConcluidas tarefasConcluidas = new frmConsultarTarefasConcluidas(utilizadorRecebido);
            tarefasConcluidas.ShowDialog();
        }

        //Abre a vista de tarefas em curso
        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (utilizadorRecebido is Gestor gestor)
            {
                frmConsultaTarefasEmCurso tarefasEmCurso = new frmConsultaTarefasEmCurso(utilizadorRecebido);
                tarefasEmCurso.ShowDialog();
            }
            else
            {
                MessageBox.Show("Apenas gestores podem consultar tarefas em curso.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (utilizadorRecebido is Gestor gestor)
                {
                    if (TarefaController.ExportarCSV(gestor))
                    {
                        MessageBox.Show("Tarefas exportadas com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    } 
}

