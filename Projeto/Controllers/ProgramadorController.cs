using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Controllers
{
    class ProgramadorController
    {
        public static void GravarProgramador(string nome, string username, string password, NivelExperiencia experiencia, Gestor gestorid)
        {
            BasedeDados db = BasedeDados.Instance;
            // Verifica se o gestor já existe
            var gestor = db.Gestor.Find(gestorid.id);
            db.Utilizador.Add(new Programador(nome, username, password, experiencia, gestor));
            db.SaveChanges();
        }

        public static List<Programador> ListarProgramadores()
        {
            BasedeDados db = BasedeDados.Instance;
            // Retorna uma lista de programadores filtrando a tabela de utilizadores
            return db.Programador.ToList();
        }

        public static int countProgramador()
        {
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de programadores na base de dados e adiciona 1, começando em 1 se não houver nenhum
            int count = db.Programador.Count();
            return count + 1;
        }
    }
}
