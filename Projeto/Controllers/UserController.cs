using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTasks.Models;

namespace iTasks.Controllers
{
    public class UserController
    {
        // Método para contar o login de um utilizador
        public static Utilizador loginUtilizador(string username, string password)
        {
            // Se o utilizador existir e a password estiver correta, retorna true
            // Retorna um obj
            BasedeDados db = BasedeDados.Instance;
            Utilizador user = db.Utilizador.FirstOrDefault(u => u.username == username && u.password == password);
            return user;
        }
        // Método para adicionar um administrador com o username "admin" e a password "admin"
        public static void addAdmin()
        {
            // Cria uma instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Verifica se já existe um utilizador com o username "admin"
            if (!db.Utilizador.Any(u => u.username == "admin"))
            {
                // Se não existir, cria um novo utilizador com o username "admin" e a password "admin"
                Gestor admin = new Gestor("admin", "admin", "admin", Departamento.Administração, true);
                db.Utilizador.Add(admin);
                db.SaveChanges();
            }
        }

        public static int countId()
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de gestores na base de dados e adiciona 1, começando em 1 se não houver nenhum
            int maxId = db.Utilizador.Max(u => u.id);
            return maxId + 1;
        }
    }
}
