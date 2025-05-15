using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        }

        private void btNova_Click(object sender, EventArgs e)
        {
            // Abre o formulário de nova tarefa
            if (utilizadorRecebido is Gestor gestor)
            {
                frmDetalhesTarefa detalhesTarefa = new frmDetalhesTarefa();
                detalhesTarefa.ShowDialog();
            }
            else
            {
                // Se o utilizador não for um gestor, mostra uma mensagem de erro
                MessageBox.Show("Apenas gestores podem criar novas tarefas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
