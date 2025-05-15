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
            using (var db = new BasedeDados())
            {
                Utilizador user = db.Utilizador
                 .FirstOrDefault(u => u.username == username && u.password == password);

                return user;
            }
        }
    }
}
