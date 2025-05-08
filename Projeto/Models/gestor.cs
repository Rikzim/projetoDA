using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public enum Departamento{IT, Marketing, Administração}
    class gestor: utilizador
    {
        public Departamento departamento { get; set; }
        public bool gereUtilizadores { get; set; }

        public gestor(string nome, string username, string password, Departamento departamento, bool gereUtilizadores) : base(nome, username, password)
        {
            this.nome = nome;
            this.username = username;
            this.password = password;
            this.departamento = departamento;
            this.gereUtilizadores = gereUtilizadores;
        }

        public override string ToString()
        {
            return $"Gestor: {nome}, Username: {username}, Password: {password}, Departamento: {departamento}, Gere Utilizadores: {gereUtilizadores}";
        }
    }
}
