using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTasks.Controllers;
using iTasks.Models;

namespace iTasks
{
    public partial class frmGereTiposTarefas : Form
    {
        BasedeDados db = new BasedeDados();
        public frmGereTiposTarefas()
        {
            InitializeComponent();

            // Preenche a lista com os tipos de tarefa
            db.TipoTarefa.Load();
            // Preenche o campo de id
            txtId.Text = TipoTarefaController.contaTipoTarefa();

            lstLista.DataSource = db.TipoTarefa.Local.ToBindingList();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            if (txtDesc.Text != null)
            {
                TipoTarefa tipoTarefa = new TipoTarefa(txtDesc.Text);

                // Adiciona o tipo de tarefa à base de dados
                db.TipoTarefa.Add(tipoTarefa);
                db.SaveChanges();

                // Atualiza a lista
                db.TipoTarefa.Load();
                lstLista.DataSource = db.TipoTarefa.Local.ToBindingList();
            }
            else
            {
                MessageBox.Show("Preencha o campo de descrição.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
