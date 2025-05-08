using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    class utilizador
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public utilizador(string nome, string username, string password)
        {
            this.nome = nome;
            this.username = username;
            this.password = password;
        }
    }
}
