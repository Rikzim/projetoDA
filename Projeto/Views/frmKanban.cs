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
<<<<<<< Updated upstream
        Utilizador utilizadorRecebido;
=======
        // Guarda o utilizador autenticado
        Utilizador utilizadorRecebido;

        // Construtor do Formulário Kanban
>>>>>>> Stashed changes
        public frmKanban(Utilizador utilizadorRecebido)
        {
            InitializeComponent();

            this.utilizadorRecebido = utilizadorRecebido;

            // Exibe o nome do utilizador logado
            label1.Text = "Bem-Vindo, " + utilizadorRecebido.nome;

            // Se for programador, limita as permissões
            if (utilizadorRecebido is Programador programador)
            {
<<<<<<< Updated upstream
                // Se o utilizador for um gestor, mostra o botão de gestão de utilizadores
                btNova.Enabled = false;
                utilizadoresToolStripMenuItem.Enabled = false;
            }

            ReloadData();
        }

=======
                // Menu ToolStrip
                utilizadoresToolStripMenuItem.Enabled = false; // Desativa acesso à gestão de utilizadores
                exportarParaCSVToolStripMenuItem.Enabled = false; // Desativa exportação de tarefas
                // Botões
                btNova.Enabled = false; // Desativa botão de criar nova tarefa
            }
            ReloadData(); // Atualiza o conteúdo do formulário
        }

        // ==============================
        //     EVENTOS DE BOTÕES (CLICK)
        // ==============================
        private void btPrevisao_Click(object sender, EventArgs e)
        {
            try
            {
                // Abre o formulário de previsão de tarefas
                frmDetalhesPrevisao MostrarPrevisao = new frmDetalhesPrevisao(utilizadorRecebido);
                MostrarPrevisao.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
>>>>>>> Stashed changes
        private void btNova_Click(object sender, EventArgs e)
        {
            lstTodo.SelectedIndex = -1; // Limpa a seleção da lista de tarefas
            // Abre o formulário de nova tarefa
            if (utilizadorRecebido is Gestor gestor)
            {
<<<<<<< Updated upstream
                frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido);
                detalhesTarefa.ShowDialog();
                ReloadData(); // Atualiza a lista após a criação de uma nova tarefa
=======
                lstTodo.SelectedIndex = -1; // Limpa a seleção da lista de tarefas

                // Só gestores podem criar novas tarefas
                if (utilizadorRecebido is Gestor gestor)
                {
                    // Abre o formulário de detalhes da tarefa
                    frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido, frmDetalhesTarefa.DetalhesTarefaState.Novo);
                    detalhesTarefa.ShowDialog();
                    ReloadData(); // Atualiza a lista após a criação de uma nova tarefa
                }
                else
                {
                    throw new Exception("Apenas gestores podem criar novas tarefas.");
                }
>>>>>>> Stashed changes
            }
            else
            {
<<<<<<< Updated upstream
                // Se o utilizador não for um gestor, mostra uma mensagem de erro
                MessageBox.Show("Apenas gestores podem criar novas tarefas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
=======
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                ReloadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btSetDoing_Click(object sender, EventArgs e)
        {
            try
            {
               
                var tarefaSelecionada = lstTodo.SelectedItem as Tarefa; // Obtem a tarefa que foi seleciona na listbox to do

                // Verifica se está na ordem correta
                if (!TarefaController.VerificarOrdem(tarefaSelecionada, Tarefa.Estado.Doing))
                    throw new Exception("A tarefa não pode ser movida para Doing porque não está na ordem correta de execução.");

                // Limita a 2 tarefas em Doing por programador

                if (TarefaController.countTarefasPorEstadoProgramador(Tarefa.Estado.Doing, utilizadorRecebido) >= 2)
                    throw new Exception("Não é possível mover a tarefa para Doing porque já existem 2 tarefas em Doing atribuídas a si.");

                TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.Doing, utilizadorRecebido); // Muda o estado da tarefa para Doing
                MessageBox.Show("Tarefa movida para Doing.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadData(); // Atualiza a lista após a mudança de estado
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btSetDone_Click(object sender, EventArgs e)
        {
            try
            {
                var tarefaSelecionada = lstDoing.SelectedItem as Tarefa; // Obtem a tarefa que foi seleciona na listbox doing

                // Verifica se está na ordem correta
                if (!TarefaController.VerificarOrdem(tarefaSelecionada, Tarefa.Estado.Done))
                    throw new Exception("A tarefa não pode ser movida para Done porque não está na ordem correta de execução.");
                // Muda o estado da tarefa para Done
                TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.Done, utilizadorRecebido);
                MessageBox.Show("Tarefa movida para Done.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadData(); // Atualiza a lista após a mudança de estado
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btSetTodo_Click(object sender, EventArgs e)
        {
            try
            {
                var tarefaSelecionada = lstDoing.SelectedItem as Tarefa; // Obtem a tarefa que foi seleciona na listbox doing

                // Muda o estado da tarefa para Todo
                TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.ToDo, utilizadorRecebido);
                MessageBox.Show("Tarefa movida para ToDo.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadData(); // Atualiza a lista após a mudança de estado
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
>>>>>>> Stashed changes
            }
        }
        // Eventos do Menu tool Strip 
        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
<<<<<<< Updated upstream
            frmGereUtilizadores gereUtilizadores = new frmGereUtilizadores(utilizadorRecebido);
            gereUtilizadores.ShowDialog();
=======
            try
            {
                // Abre o formulário de gestão de utilizadores
                frmGereUtilizadores gereUtilizadores = new frmGereUtilizadores(utilizadorRecebido);
                gereUtilizadores.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            ReloadData();
=======
            try
            {
                // Abre o formulário de consulta de tarefas concluídas
                frmConsultarTarefasConcluidas tarefasConcluidas = new frmConsultarTarefasConcluidas(utilizadorRecebido);
                tarefasConcluidas.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
>>>>>>> Stashed changes
        }
        // Eventos do KanBan
        private void btSetDoing_Click(object sender, EventArgs e)
        {
            var tarefaSelecionada = lstTodo.SelectedItem as Tarefa;

            if (tarefaSelecionada != null)
            {
<<<<<<< Updated upstream
                if (TarefaController.countTarefasPorEstadoProgramador(Tarefa.Estado.Doing, utilizadorRecebido) < 2)
                {
                    // Muda o estado da tarefa para Done
                    TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.Doing, utilizadorRecebido);
                    MessageBox.Show("Tarefa movida para Doing.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadData(); // Atualiza a lista após a mudança de estado
=======
                // Abre o formulário de consulta de tarefas em curso se o utilizador for um gestor
                if (utilizadorRecebido is Gestor)
                {
                    // Abre o formulário de consulta de tarefas em curso
                    frmConsultaTarefasEmCurso tarefasEmCurso = new frmConsultaTarefasEmCurso(utilizadorRecebido);
                    tarefasEmCurso.ShowDialog();
>>>>>>> Stashed changes
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
                    // Verifica se o programador já realizou as tarefas anteriores

                    var controlo = TarefaController.VerificarOrdem(tarefaSelecionada, Tarefa.Estado.Done);
                    if (controlo == false)
                    {
                        MessageBox.Show("Tem de concluir a tarefa anterior", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
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
                // Verifica se o utilizador é um gestor antes de permitir a exportação
                if (utilizadorRecebido is Gestor gestor)
                {
                    //Chama o método de exportação de tarefas para CSV
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
<<<<<<< Updated upstream
=======
        // Eventos de duplo clique
        private void lstTodo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var tarefaSelecionada = lstTodo.SelectedItem as Tarefa; // Obtem a tarefa que foi seleciona na listbox todo

                // Verifica se existe uma tarefa selecionada
                if (tarefaSelecionada != null)
                {
                    // Abre o formulário de detalhes da tarefa
                    frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido, frmDetalhesTarefa.DetalhesTarefaState.Editar, tarefaSelecionada);
                    detalhesTarefa.ShowDialog();
                    ReloadData(); // Atualiza a lista após possíveis alterações
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lstDoing_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var tarefaSelecionada = lstDoing.SelectedItem as Tarefa; // Obtem a tarefa que foi seleciona na listbox doing

                // Verifica se existe uma tarefa selecionada
                if (tarefaSelecionada != null)
                {
                    // Abre o formulário de detalhes da tarefa
                    frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido, frmDetalhesTarefa.DetalhesTarefaState.Editar , tarefaSelecionada);
                    detalhesTarefa.ShowDialog();
                    ReloadData(); // Atualiza a lista após possíveis alterações
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lstDone_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var tarefaSelecionada = lstDone.SelectedItem as Tarefa; // Obtem a tarefa que foi seleciona na listbox done

                // Verifica se existe uma tarefa selecionada
                if (tarefaSelecionada != null)
                {
                    // Abre o formulário de detalhes da tarefa
                    frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido, frmDetalhesTarefa.DetalhesTarefaState.Editar, tarefaSelecionada);
                    detalhesTarefa.ShowDialog();
                    ReloadData(); // Atualiza a lista após possíveis alterações
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Funcoes auxiliares
        private void ReloadData()
        {
            try
            {
                // Atualiza as listas de tarefas que estão no estado Todo, Doing e Done
                lstTodo.DataSource = null;
                lstTodo.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.ToDo);
                lstDoing.DataSource = null;
                lstDoing.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.Doing);
                lstDone.DataSource = null;
                lstDone.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.Done);
                // Limpa as seleções das listas
                lstDoing.SelectedIndex = -1;
                lstDone.SelectedIndex = -1;
                lstTodo.SelectedIndex = -1;
                // Atualiza os labels com o número de tarefas em cada estado
                label2.Text = lstTodo.Items.Count.ToString();
                label3.Text = lstDoing.Items.Count.ToString();
                label4.Text = lstDone.Items.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
>>>>>>> Stashed changes
    } 
}

