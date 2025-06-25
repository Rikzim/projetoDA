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
<<<<<<< Updated upstream
        public frmDetalhesTarefa(Utilizador utilizadorRecebido, Tarefa tarefaSelecionada = null)
=======

        // Enum para representar o modo do formulário
        public enum DetalhesTarefaState
        {
            Novo,
            Editar,
            ReadOnly
        }

        private DetalhesTarefaState state;
        public frmDetalhesTarefa(Utilizador utilizadorRecebido, DetalhesTarefaState state, Tarefa tarefaSelecionada = null)
>>>>>>> Stashed changes
        {
            InitializeComponent();

            // Define o utilizador recebido
            this.utilizadorRecebido = utilizadorRecebido;
<<<<<<< Updated upstream

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

=======
            this.tarefaSelecionada = tarefaSelecionada;
            this.state = state;

            // Se o utilizador for Programador, força o modo só de leitura
            if (utilizadorRecebido is Programador)
            {
                this.state = DetalhesTarefaState.ReadOnly;
                state = this.state;
            }

            // Configura os botões de acordo com o estado da tarefa
            ConfigurarBotoes(state);

            //Inicializa os comboboxes com os dados necessários
            InicializarComboboxes();

            // Verifica se uma tarefa foi selecionada
            if (tarefaSelecionada != null)
            {
                // Se uma tarefa foi selecionada, preenche os campos com os dados da tarefa
                PreencherCamposImutaveis();// Preenche campos só de leitura (ID, Estado, etc.)
                PreencherCamposMutaveis();
            }
            else
            {
                // Se não há tarefa selecionada, prepara o formulário para criar uma nova tarefa
                PrepararNovaTarefa(); 
            }
        }

        private void ConfigurarBotoes(DetalhesTarefaState state)
        {
            // Configura os botões de acordo com o estado da tarefa
            switch (state)
            {
                case DetalhesTarefaState.Novo:
                    btGravar.Enabled = true;
                    btEditarTarefa.Enabled = false;
                    btApagarTarefa.Enabled = false;
                    break;
                case DetalhesTarefaState.Editar:
                    btGravar.Enabled = false;
                    btEditarTarefa.Enabled = true;
                    btApagarTarefa.Enabled = true;
                    break;
                case DetalhesTarefaState.ReadOnly:
                    readOnlyUtilizador(); // Desativa campos e botões para o programador
                    break;
            }
        }

        // Impede alterações no formulário
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

>>>>>>> Stashed changes
        private void btGravar_Click(object sender, EventArgs e)
        {
            try
            {
<<<<<<< Updated upstream
=======
                // Verifica se todos os campos obrigatórios estão preenchidos
                if (string.IsNullOrWhiteSpace(txtDesc.Text) || cbTipoTarefa.SelectedIndex == -1 || cbProgramador.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtOrdem.Text) || string.IsNullOrWhiteSpace(txtStoryPoints.Text))
                {
                    throw new Exception("Por favor, preencha todos os campos obrigatórios.");
                }

                // Converte e envia dados ao controlador
>>>>>>> Stashed changes
                Programador programador = (Programador)cbProgramador.SelectedItem;
                Gestor gestor = (Gestor)utilizadorRecebido;
                TipoTarefa tipoTarefa = (TipoTarefa)cbTipoTarefa.SelectedItem;

                //Grava a Tarefa
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

                    //Edita a Tarefa
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

                //TODO: METER EM MVC
                if (tarefaSelecionada != null)
                {
                    BasedeDados db = BasedeDados.Instance;
                    // Remove a tarefa selecionada da base de dados
                    db.Tarefa.Remove(tarefaSelecionada);
                    db.SaveChanges();
                }
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
    }
}
