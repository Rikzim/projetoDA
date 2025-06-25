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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            // Configurações do formulário de login
            // Faz com que o form responda às teclas pressionadas
            this.KeyPreview = true;
            this.KeyPress += frmLogin_KeyPress;

            try
            {
                UserController.addAdmin(); // Adiciona o utilizador admin se não existir
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro ao adicionar o administrador, mostra uma mensagem de erro
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Guarda os dados do utilizador
                Utilizador user = UserController.loginUtilizador(txtUsername.Text, txtPassword.Text);
                //Verifica se o utilizador existe e se a password está correta
                if (user != null)
                {
                    // Se o utilizador existir e a password estiver correta, abre o formulário principal
                    this.Hide();
                    frmKanban kanban = new frmKanban(user);
                    kanban.FormClosed += (s, args) => this.Close(); // Fecha o login só depois do Kanban fechar
                    kanban.Show();
                }
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro ao fazer login, mostra uma mensagem de erro
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica se a tecla pressionada é Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                //Para o som
                e.Handled = true;
                // Se a tecla pressionada for Enter, chama o evento de clique do botão de login
                btLogin.PerformClick();
            }
        }
    }
}
