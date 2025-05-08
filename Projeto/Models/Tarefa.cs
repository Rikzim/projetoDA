using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Models
{
    class Tarefa
    {

        public enum Estado { 
            ToDo,
            Doing,
            Done
        }

        public int Id { get; set; }
        public int IdGestor { get; set; }
        public int IdProgramador { get; set; }
        public int OrdemExecucao { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPrevistaInicio { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public int TipoTarefa { get; set; }
        public int StoryPoints { get; set; }
        public DateTime DataRealInicio { get; set; }
        public DateTime DataRealFim { get; set; }
        public DateTime DataCriacao { get; set; }
        public Estado EstadoAtual { get; set; }

        public Tarefa(int id, int idGestor, int idProgramador, int ordemExecucao, string descricao, 
            DateTime dataPrevistaInicio, DateTime dataPrevistaFim, int tipoTarefa, int storyPoints, DateTime dataRealInicio, 
            DateTime dataRealFim, DateTime dataCriacao, Estado estadoAtual)
        {
            this.IdGestor = idGestor;
            this.IdProgramador = idProgramador;
            this.OrdemExecucao = ordemExecucao;
            this.Descricao = descricao;
            this.DataPrevistaInicio = dataPrevistaInicio;
            this.DataPrevistaFim = dataPrevistaFim;
            this.TipoTarefa = tipoTarefa;
            this.StoryPoints = storyPoints;
            this.DataRealInicio = dataRealInicio;
            this.DataRealFim = dataRealFim;
            this.DataCriacao = dataCriacao;
            this.EstadoAtual = estadoAtual;
        }
    }
}
