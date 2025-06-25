using iTasks.Controllers;
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

namespace iTasks
{
    public partial class frmGereUtilizadores : Form
    {
        // Guarda o utilizador que abriu o formulário (esperado: Gestor)
        Utilizador utilizadorRecebido;
        public frmGereUtilizadores(Utilizador utilizadorRecebido)
        {
            InitializeComponent();
            this.utilizadorRecebido = utilizadorRecebido;
            
             // Se for um gestor SEM permissões para gerir utilizadores
            if (utilizadorRecebido is Gestor gestor && gestor.gereUtilizadores == false)
            {
                txtNomeGestor.Enabled = false;
                txtPasswordGestor.Enabled = false;
                txtUsernameGestor.Enabled = false;
                cbDepartamento.Enabled = false;
                btGravarGestor.Enabled = false;
            }
            
            // Atualiza todos os dados visuais do formulário
            ReloadData();
        }
        
        // Eventos Guardar
        private void btGravarProg_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se os campos obrigatórios estão preenchidos
                if (txtNomeProg.Text == null || txtUsernameProg.Text == null || txtPasswordProg.Text == null)
                    throw new Exception("Por favor, preencha todos campos.");
                if (cbGestorProg.SelectedItem == null)
                    throw new Exception("Por favor, selecione um gestor.");
                if (cbNivelProg.SelectedItem == null)
                    throw new Exception("Por favor, selecione um nivel.");
                    
                // Guarda o novo programador
                ProgramadorController.GravarProgramador(
                    txtNomeProg.Text, 
                    txtUsernameProg.Text, 
                    txtPasswordProg.Text, 
                    (NivelExperiencia)cbNivelProg.SelectedItem, 
                    (Gestor)cbGestorProg.SelectedItem
                );

                MessageBox.Show("Programador gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        
        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            try
            {
                  // Verifica campos obrigatórios
                if (string.IsNullOrWhiteSpace(txtNomeGestor.Text) || string.IsNullOrWhiteSpace(txtUsernameGestor.Text) || string.IsNullOrWhiteSpace(txtPasswordGestor.Text))
                    throw new Exception("Por favor, preencha todos os campos.");
                if (cbDepartamento.SelectedItem == null)
                    throw new Exception("Por favor, selecione um departamento.");
                    
                // Guardar novo gestor
                GestorController.GravarGestor(
                    txtNomeGestor.Text, 
                    txtUsernameGestor.Text, 
                    txtPasswordGestor.Text, 
                    (Departamento)cbDepartamento.SelectedItem, 
                    chkGereUtilizadores.Checked
                );

                MessageBox.Show("Gestor gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        // Eventos Editar
        private void btEditarProg_Click(object sender, EventArgs e)
        {
            try
            {
                //Verifica campos obrigatórios
                if (string.IsNullOrWhiteSpace(txtNomeProg.Text) || string.IsNullOrWhiteSpace(txtUsernameProg.Text) || string.IsNullOrWhiteSpace(txtPasswordProg.Text))
                    throw new Exception("Por favor, preencha todos os campos.");
                if (cbNivelProg.SelectedItem == null)
                    throw new Exception("Por favor, selecione um nivel de experiencia");
                    
                //Busca programador selecionado
                Programador progSelecionado = (Programador)lstListaProgramadores.SelectedItem;
                NivelExperiencia nivelExperiencia = (NivelExperiencia)cbNivelProg.SelectedItem;

                if (progSelecionado == null)
                    throw new Exception("Por favor, selecione um programador da lista.");
                    
                //Edita o programador selecionado
                ProgramadorController.EditarProgramador(
                    progSelecionado,
                    txtNomeProg.Text,
                    txtUsernameProg.Text,
                    txtPasswordProg.Text,
                    nivelExperiencia,
                    (Gestor)cbGestorProg.SelectedItem
                    );
                MessageBox.Show("Utilizador editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        private void btEditarGestor_Click(object sender, EventArgs e)
        {
            try
            {
                //Verifica campos obrigatórios
                if (txtNomeGestor.Text == null || txtUsernameGestor.Text == null || txtPasswordGestor.Text == null)
                    throw new Exception("Por favor, preencha todos campos.");
                if (cbDepartamento.SelectedItem == null)
                    throw new Exception("Por favor, selecione um departamento.");
                
                //Busca gestor selecionado
                Gestor gestorSelecionado = (Gestor)lstListaGestores.SelectedItem;
                Departamento departamento = (Departamento)cbDepartamento.SelectedItem;

                if (gestorSelecionado == null)
                    throw new Exception("Por favor, selecione um gestor da lista.");
                    
                //Edita Gestor
                GestorController.EditarGestor(
                    gestorSelecionado,
                    txtNomeGestor.Text,
                    txtUsernameGestor.Text,
                    txtPasswordGestor.Text,
                    departamento,
                    chkGereUtilizadores.Checked
                    );
                MessageBox.Show("Gestor editado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        // Eventos de Eliminar
        private void btEliminarProg_Click(object sender, EventArgs e)
        {
            try
            {
                // Busca programador selecionado
                Programador progSelecionado = (Programador)lstListaProgramadores.SelectedItem;

                if (progSelecionado == null)
                    throw new Exception("Por favor, selecione um programador da lista.");
                    
                //Elimina programador selecionado
                ProgramadorController.EliminarProgramador(progSelecionado);

                MessageBox.Show("Programador eliminado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        private void btEliminarGestor_Click(object sender, EventArgs e)
        {
            try
            {
                //Busca gestor selecionado
                Gestor gestorSelecionado = (Gestor)lstListaGestores.SelectedItem;

                if (gestorSelecionado == null)
                    throw new Exception("Por favor, selecione um gestor da lista.");
                    
                    
                //Elemina gestor selecionado
                GestorController.EliminarGestor(gestorSelecionado, utilizadorRecebido);

                MessageBox.Show("Gestor eliminado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ReloadData();
            }
        }
        // Eventos de Atualização
        private void lstListaGestores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Busca gestor selecionado
                Gestor gestorSelecionado = (Gestor)lstListaGestores.SelectedItem;

                if (gestorSelecionado == null)
                    return; // Se não houver gestor selecionado, não faz nada
                
                // Preenche os campos com os dados do gestor
                txtIdGestor.Text = gestorSelecionado.id.ToString();
                txtNomeGestor.Text = gestorSelecionado.nome;
                txtUsernameGestor.Text = gestorSelecionado.username;
                txtPasswordGestor.Text = gestorSelecionado.password;
                cbDepartamento.SelectedItem = gestorSelecionado.departamento;
                chkGereUtilizadores.Checked = gestorSelecionado.gereUtilizadores;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lstListaProgramadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Busca programador selecionado
                Programador progSelecionado = (Programador)lstListaProgramadores.SelectedItem;

                if (progSelecionado == null)
                    return; // Se não houver programador selecionado, não faz nada
                
                // Preenche os campos com os dados do programador
                txtIdProg.Text = progSelecionado.id.ToString();
                txtNomeProg.Text = progSelecionado.nome;
                txtUsernameProg.Text = progSelecionado.username;
                txtPasswordProg.Text = progSelecionado.password;
                cbNivelProg.SelectedItem = progSelecionado.nivelExperiencia;
                cbGestorProg.SelectedItem = progSelecionado.idGestor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Funções Auxiliares
        private void ReloadData()
        {
            // Atualiza a combobox dos departamentos disponivies aos gestores
            cbDepartamento.DataSource = null;
            cbDepartamento.DataSource = Enum.GetValues(typeof(Departamento));
            // Atualiza a combobox dos niveis de experiencia disponiveis aos programadores
            cbNivelProg.DataSource = null;
            cbNivelProg.DataSource = Enum.GetValues(typeof(NivelExperiencia));
            // Atualiza a combobox dos gestores disponiveis aos programadores
            cbGestorProg.DataSource = null;
            cbGestorProg.DataSource = GestorController.ListarGestores();
            // Atualiza a lista de Gestores
            lstListaGestores.DataSource = null;
            lstListaGestores.DataSource = GestorController.ListarGestores();
            // Atualiza a lista de Programadores
            lstListaProgramadores.DataSource = null;
            lstListaProgramadores.DataSource = ProgramadorController.ListarProgramadores();
            // Atualiza os IDs dos gestores e programadores
            txtIdProg.Text = UserController.countId().ToString();
            txtIdGestor.Text = UserController.countId().ToString();
            // Deseleciona as list box
            lstListaGestores.SelectedIndex = -1;
            lstListaProgramadores.SelectedIndex = -1;
            // Limpar todos os campos
            txtNomeProg.Clear();
            txtNomeGestor.Clear();
            txtUsernameProg.Clear();
            txtUsernameGestor.Clear();
            txtPasswordProg.Clear();
            txtPasswordGestor.Clear();

            
        }
    }
}