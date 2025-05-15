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
        public static BasedeDados db = new BasedeDados();
        public static void GravarGestor(string nome, string username, string password, Departamento departamento, bool GereUtilizadores)
        {
            db.Utilizador.Add(new Gestor(nome, username, password, departamento, GereUtilizadores));
            db.SaveChanges();
        }

        public static List<Gestor> ListarGestores()
        {
            return db.Utilizador.OfType<Gestor>().ToList();
        }

        public static int countGestor()
        {
            int count = db.Gestor.Count();
            return count + 1;
        }
    }
}
