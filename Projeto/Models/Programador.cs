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
    class Programador: Utilizador
    {
        public NivelExperiencia nivelExperiencia { get; set; }
        public Gestor idGestor { get; set; }
        public Programador(string nome, string username, string password, NivelExperiencia nivelExperiencia, Gestor idGestor) : base(nome, username, password)
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
