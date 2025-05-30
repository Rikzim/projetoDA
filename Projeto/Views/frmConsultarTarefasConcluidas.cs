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
            this.utilizadorRecebido = utilizador;
            gvTarefasConcluidas.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.Done, utilizadorRecebido);
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
