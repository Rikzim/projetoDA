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
        BasedeDados db;
        public frmLogin()
        {
            InitializeComponent();

            db = new BasedeDados();


        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            // Guarda os dados do utilizador
            Utilizador user = UserController.loginUtilizador(txtUsername.Text, txtPassword.Text);
            //Verifica se o utilizador existe e se a password está correta
            if (user != null)
            {
                // Se o utilizador existir e a password estiver correta, abre o formulário principal
                this.Hide();
                frmKanban kanban = new frmKanban(user);
                kanban.Show();
            }
            else
            {
                // Se o utilizador não existir ou a password estiver incorreta, mostra uma mensagem de erro
                MessageBox.Show("Utilizador ou password inválidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
