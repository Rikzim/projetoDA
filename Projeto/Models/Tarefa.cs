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
        public Gestor IdGestor { get; set; }
        public Programador IdProgramador { get; set; }
        public int OrdemExecucao { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPrevistaInicio { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public TipoTarefa TipoTarefa { get; set; }
        public int StoryPoints { get; set; }
        public DateTime DataRealInicio { get; set; }
        public DateTime DataRealFim { get; set; }
        public DateTime DataCriacao { get; set; }
        public Estado EstadoAtual { get; set; }

        public Tarefa(int id, Gestor idGestor, Programador idProgramador, int ordemExecucao, string descricao, 
            DateTime dataPrevistaInicio, DateTime dataPrevistaFim, TipoTarefa tipoTarefa, int storyPoints, DateTime dataRealInicio, 
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

        public override string ToString() 
        { 
            return $"Tarefa: {Descricao}, ID: {Id}, Gestor: {IdGestor.nome}, Programador: {IdProgramador.nome}, Ordem Execucao: {OrdemExecucao}, " +
                $"Data Prevista Inicio: {DataPrevistaInicio}, Data Prevista Fim: {DataPrevistaFim}, Tipo Tarefa: {TipoTarefa.Nome}, Story Points: {StoryPoints}, " +
                $"Data Real Inicio: {DataRealInicio}, Data Real Fim: {DataRealFim}, Data Criacao: {DataCriacao}, Estado Atual: {EstadoAtual}";
        }
    }
}
