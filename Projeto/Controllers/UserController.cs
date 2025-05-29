using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTasks.Models;

namespace iTasks.Controllers
{
    public class UserController
    {
        public static Utilizador loginUtilizador(string username, string password)
        {

            // Se o utilizador existir e a password estiver correta, retorna true
            // Retorna um obj
            BasedeDados db = BasedeDados.Instance;
            Utilizador user = db.Utilizador.FirstOrDefault(u => u.username == username && u.password == password);
            return user;
        }

        public static void addAdmin()
        {
            BasedeDados db = BasedeDados.Instance;
            // Verifica se já existe um utilizador com o username "admin"
            if (!db.Utilizador.Any(u => u.username == "admin"))
            {
                // Se não existir, cria um novo utilizador com o username "admin" e a password "admin"
                Gestor admin = new Gestor("admin", "admin", "admin",Departamento.Administração, true);
                db.Utilizador.Add(admin);
                db.SaveChanges();
            }
        }
    }
}
