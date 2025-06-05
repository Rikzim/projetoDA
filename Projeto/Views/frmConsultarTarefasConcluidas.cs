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
using iTasks.Controllers;

namespace iTasks
{
    public partial class frmConsultarTarefasConcluidas : Form
    {
        Utilizador utilizadorRecebido;
        public frmConsultarTarefasConcluidas(Utilizador utilizador)
        {
            InitializeComponent();
            try
            {
                this.utilizadorRecebido = utilizador;
                gvTarefasConcluidas.DataSource = TarefaController.ListarTarefasPorEstadoProgramador(Tarefa.Estado.Done, utilizadorRecebido);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao fechar a janela: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
