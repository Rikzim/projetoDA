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
            try
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

                    // Preenche o campo de id
                    txtId.Text = TipoTarefaController.contaTipoTarefa();
                }
                else
                {
                    throw new Exception("Campo de descrição não pode ser nulo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
