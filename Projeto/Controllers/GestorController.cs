using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Controllers
{
    class GestorController
    {
        
        public static void GravarGestor(string nome, string username, string password, Departamento departamento, bool GereUtilizadores)
        {
            BasedeDados db = BasedeDados.Instance;
            // Verifica se o gestor já existe
            db.Utilizador.Add(new Gestor(nome, username, password, departamento, GereUtilizadores));
            db.SaveChanges();
        }

        public static List<Gestor> ListarGestores()
        {
            BasedeDados db = BasedeDados.Instance;
            // Retorna uma lista de gestores filtrando a tabela de utilizadores
            return db.Utilizador.OfType<Gestor>().ToList();
        }

        public static int countGestor()
        {
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de gestores na base de dados e adiciona 1, começando em 1 se não houver nenhum
            int count = db.Gestor.Count();
            return count + 1;
        }
    }
}
