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
    public class Programador: Utilizador
    {
        public NivelExperiencia nivelExperiencia { get; set; }
        public Gestor idGestor { get; set; }
        public Programador() { }
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
            return $"ID: {id} | Username: {username} | Nivel de Experiencia: {idGestor.nome}";
        }
    }
}
