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
        public static BasedeDados db = new BasedeDados();
        
        public static void GravarProgramador(string nome, string username, string password, NivelExperiencia experiencia, Gestor gestorid)
        {
            var gestor = db.Gestor.Find(gestorid.id);
            db.Utilizador.Add(new Programador(nome, username, password, experiencia, gestor));
            db.SaveChanges();
        }

        public static List<Programador> ListarProgramadores()
        {
            return db.Utilizador.OfType<Programador>().ToList();
        }

        public static int countProgramador()
        {
            int count = db.Programador.Count();
            return count + 1;
        }
    }
}
