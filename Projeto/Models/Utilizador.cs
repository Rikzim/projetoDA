using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class Utilizador
    {
        public int id { get; set; }
        public string nome { get; set; }
        [MaxLength(100)] [Index(IsUnique = true)] public string username { get; set; }
        public string password { get; set; }

        public Utilizador() { }
        public Utilizador(string nome, string username, string password)
        {
            this.nome = nome;
            this.username = username;
            this.password = password;
        }

        public override string ToString()
        {
            return $"Utilizador: {nome}, Username: {username}, Password: {password}";
        }
    }
}
