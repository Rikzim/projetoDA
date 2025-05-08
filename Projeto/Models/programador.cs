using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public enum NivelExperiencia
    {
        Junior,
        Senior
    }
    class programador: utilizador
    {
        public NivelExperiencia nivelExperiencia { get; set; }
        public int idGestor { get; set; }
        public programador(string nome, string username, string password, NivelExperiencia nivelExperiencia, int idGestor) : base(nome, username, password)
        {
            this.nome = nome;
            this.username = username;
            this.password = password;
            this.nivelExperiencia = nivelExperiencia;
            this.idGestor = idGestor;
        }

        public override string ToString()
        {
            return $"Programador: {nome}, Username: {username}, Password: {password}, Nivel de Experiencia: {nivelExperiencia}";
        }
    }
}
