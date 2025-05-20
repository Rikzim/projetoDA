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
        public frmDetalhesTarefa()
        {
            InitializeComponent();

            // Preenche o comboBox com os tipos de tarefa
            cbTipoTarefa.Items.Add(new TipoTarefa());

            //Atualizar a combobox com os tipos de tarefa
            cbTipoTarefa.DataSource = null;
            cbTipoTarefa.DataSource = TipoTarefaController.ListarTipoTarefa();
            //Atualizar a combobox com os programadores
            cbProgramador.DataSource = null;
            cbProgramador.DataSource = ProgramadorController.ListarProgramadores();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            Tarefa tarefa = new Tarefa();
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
