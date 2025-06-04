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
        Utilizador utilizadorRecebido;
        public frmGereUtilizadores(Utilizador utilizadorRecebido)
        {
            InitializeComponent();
            this.utilizadorRecebido = utilizadorRecebido;

            if (utilizadorRecebido is Gestor gestor && gestor.gereUtilizadores == false)
            {
                txtNomeGestor.Enabled = false;
                txtPasswordGestor.Enabled = false;
                txtUsernameGestor.Enabled = false;
                cbDepartamento.Enabled = false;
                btGravarGestor.Enabled = false;
            }
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
            txtIdProg.Text = ProgramadorController.countProgramador().ToString();
            txtIdGestor.Text = GestorController.countGestor().ToString();

        }

        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            try
            {
                GestorController.GravarGestor(txtNomeGestor.Text, txtUsernameGestor.Text, txtPasswordGestor.Text, (Departamento)cbDepartamento.SelectedItem, chkGereUtilizadores.Checked);
                
                lstListaGestores.DataSource = null;
                lstListaGestores.DataSource = GestorController.ListarGestores();

                // Atualiza os ID do gestor
                txtIdGestor.Text = GestorController.countGestor().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MessageBox.Show("Gestor gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btGravarProg_Click(object sender, EventArgs e)
        {
            try
            {
                ProgramadorController.GravarProgramador(txtNomeProg.Text, txtUsernameProg.Text, txtPasswordProg.Text, (NivelExperiencia)cbNivelProg.SelectedItem, (Gestor)cbGestorProg.SelectedItem);

                lstListaProgramadores.DataSource = null;
                lstListaProgramadores.DataSource = ProgramadorController.ListarProgramadores();

                // Atualiza os ID do programador
                txtIdProg.Text = ProgramadorController.countProgramador().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MessageBox.Show("Programador gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
