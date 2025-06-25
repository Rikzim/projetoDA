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
using iTasks.Views;

namespace iTasks
{
    public partial class frmKanban : Form
    {
        // Inicializacao de variaveis
        Utilizador utilizadorRecebido;
        // Construtor do Formulário Kanban
        public frmKanban(Utilizador utilizadorRecebido)
        {
            InitializeComponent();

            this.utilizadorRecebido = utilizadorRecebido;
            
            // Exibe o nome do utilizador logado
            label1.Text = "Bem-Vindo, " + utilizadorRecebido.nome;

            // Se for programador, limita as permissões
            if (utilizadorRecebido is Programador programador)
            {
                // Menu ToolStrip
                utilizadoresToolStripMenuItem.Enabled = false;
                exportarParaCSVToolStripMenuItem.Enabled = false;
                // Botões
                btNova.Enabled = false;
            }
            ReloadData();
        }
        // Eventos dos Botoes (Click)
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
        private void btNova_Click(object sender, EventArgs e)
        {
            try
            {
                lstTodo.SelectedIndex = -1; // Limpa a seleção da lista de tarefas
                
                // Só gestores podem criar novas tarefas
                if (utilizadorRecebido is Gestor gestor)
                {
                    frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa(utilizadorRecebido, frmDetalhesTarefa.DetalhesTarefaState.Novo);
                    detalhesTarefa.ShowDialog();
                    ReloadData(); // Atualiza a lista após a criação de uma nova tarefa
                }
                else
                {
                    throw new Exception("Apenas gestores podem criar novas tarefas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                ReloadData(); // Atualiza a interface
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
                var tarefaSelecionada = lstTodo.SelectedItem as Tarefa; // Obtem a tarefa que foi seleciona na listbox todo
                
                //Verifica se está na ordem correta
                if (!TarefaController.VerificarOrdem(tarefaSelecionada, Tarefa.Estado.Doing))
                    throw new Exception("A tarefa não pode ser movida para Doing porque não está na ordem correta de execução.");
                    
                // Limita a 2 tarefas em Doing por programador
                if (TarefaController.countTarefasPorEstadoProgramador(Tarefa.Estado.Doing, utilizadorRecebido) >= 2)
                    throw new Exception("Não é possível mover a tarefa para Doing porque já existem 2 tarefas em Doing atribuídas a si.");
                
                // Altera o estado da tarefa
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
                var tarefaSelecionada = lstDoing.SelectedItem as Tarefa; //Tarefa seleciona em doing
                
                // Valida ordem correta de execução
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
                var tarefaSelecionada = lstDoing.SelectedItem as Tarefa; //Tarefa selecionada em doing
                
                // Muda o estado da tarefa para Todo
                TarefaController.MudarEstadoTarefa(tarefaSelecionada, Tarefa.Estado.ToDo, utilizadorRecebido);
                MessageBox.Show("Tarefa movida para ToDo.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadData(); // Atualiza a lista após a mudança de estado
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Eventos do Menu tool Strip 
        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Abre a vista de gerir utilizadores
                frmGereUtilizadores gereUtilizadores = new frmGereUtilizadores(utilizadorRecebido);
                gereUtilizadores.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abre o formulário de gestão de tipos de tarefas
            try
            {
                //Abre a vista de gerir tipos de tarefas
                frmGereTiposTarefas gereTiposTarefas = new frmGereTiposTarefas();
                gereTiposTarefas.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Fecha a aplicação
            try
            {
                DialogResult result = MessageBox.Show("Tem a certeza que deseja sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                //Verifica a resposta de saida
                if (result == DialogResult.Yes)
                {
                    Application.Exit(); //Termina a aplicação
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Abre vista de tarefas concluidas
                frmConsultarTarefasConcluidas tarefasConcluidas = new frmConsultarTarefasConcluidas(utilizadorRecebido);
                tarefasConcluidas.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Se for gestor
                if (utilizadorRecebido is Gestor)
                {
                    //Abre a vista de tarefas em curso
                    frmConsultaTarefasEmCurso tarefasEmCurso = new frmConsultaTarefasEmCurso(utilizadorRecebido);
                    tarefasEmCurso.ShowDialog();
                }
                else
                {
                    throw new Exception("Apenas gestores podem consultar tarefas em curso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Se for gestor 
                if (utilizadorRecebido is Gestor gestor)
                {
                    //Exporta tarefas concluidas em CSV
                    if (TarefaController.ExportarCSV(gestor))
                    {
                        MessageBox.Show("Tarefas exportadas com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Eventos de duplo clique
        private void lstTodo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var tarefaSelecionada = lstTodo.SelectedItem as Tarefa;
                
                if (tarefaSelecionada != null)
                {
                    //Abre vista de detalhes de tarefa
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
                var tarefaSelecionada = lstDoing.SelectedItem as Tarefa;
                if (tarefaSelecionada != null)
                {
                    //Abre vista de detalhes de tarefa
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
                var tarefaSelecionada = lstDone.SelectedItem as Tarefa; //Tarefa selecionada em done
                
                if (tarefaSelecionada != null)
                {
                    //Abre vista de detalhes de tarefa
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

        //Botão de Logout
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Tem a certeza que deseja sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Efetua logout
                    UserController.logoutUtilizador();

                    // Fecha ou esconde o formulário atual
                    this.Hide();

                    // Abre o formulário de login
                    frmLogin loginForm = new frmLogin();
                    loginForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    } 
}
