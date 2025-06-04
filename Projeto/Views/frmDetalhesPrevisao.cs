using iTasks.Controllers;
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

namespace iTasks.Views
{
    public partial class frmDetalhesPrevisao: Form
    {
        Utilizador UtilizadorRecebido;
        public frmDetalhesPrevisao(Utilizador utilizadorRecebido)
        {
            InitializeComponent();

            UtilizadorRecebido = utilizadorRecebido;

            List<Tarefa> tarefasConcluidas = TarefaController.ListarTarefasPorEstado(Tarefa.Estado.ToDo, UtilizadorRecebido);

            var tarefasComPrevisao = tarefasConcluidas
                .Select(t => {
                    double previsao = TarefaController.CalcularMediaHorasPorStoryPoints(t.StoryPoints);
                    return $"{t.Id.ToString()} |Tarefa: {t.Descricao} | StoryPoints: {t.StoryPoints} | Previsão: {previsao:0.##} Horas";
                })
                .ToList();

            lstTarefasPrevisao.DataSource = null;
            lstTarefasPrevisao.DataSource = tarefasComPrevisao;


            lbTotalHoras.Text = $"Total Previsto para Conclusão:  {TarefaController.EstimarTempoTotalToDo():0.##}  Horas";

        }

        private void frmDetalhesPrevisao_Load(object sender, EventArgs e)
        {

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
