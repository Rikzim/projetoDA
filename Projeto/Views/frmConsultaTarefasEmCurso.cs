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
    public partial class frmConsultaTarefasEmCurso : Form
    {
        Utilizador utilizadorRecebido;
        public frmConsultaTarefasEmCurso(Utilizador utilizador)
        {
            InitializeComponent();
            this.utilizadorRecebido = utilizador;
            gvTarefasEmCurso.DataSource = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.Doing, utilizadorRecebido);
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
