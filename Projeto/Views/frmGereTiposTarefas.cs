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
                    TipoTarefaController.GravarTipoTarefa(txtDesc.Text);

                    // Preenche o campo de id
                    txtId.Text = TipoTarefaController.contaTipoTarefa();

                    lstLista.DataSource = null;
                    lstLista.DataSource = TipoTarefaController.ListarTipoTarefa();
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

        private void btEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstLista.SelectedItem != null)
                {
                    TipoTarefa tipoTarefaSelecionada = (TipoTarefa)lstLista.SelectedItem;
                    TipoTarefaController.EditarTipoTarefa(tipoTarefaSelecionada, txtDesc.Text);

                    lstLista.DataSource = null;
                    lstLista.DataSource = TipoTarefaController.ListarTipoTarefa();
                }
                else
                {
                    throw new Exception("Nenhum tipo de tarefa selecionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstLista.SelectedItem != null)
                {
                    TipoTarefa tipoTarefaSelecionada = (TipoTarefa)lstLista.SelectedItem;
                    TipoTarefaController.EliminarTipoTarefa(tipoTarefaSelecionada);
                    

                    lstLista.DataSource = null;
                    lstLista.DataSource = TipoTarefaController.ListarTipoTarefa();
                }
                else
                {
                    throw new Exception("Nenhum tipo de tarefa selecionado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
