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
            try
            {
                // Se o utilizador existir e a password estiver correta, retorna true
                // Retorna um obj
                BasedeDados db = BasedeDados.Instance;
                Utilizador user = db.Utilizador.FirstOrDefault(u => u.username == username && u.password == password);
                return user;
            }
            catch (Exception ex)
            {
                // Lança uma exceção se ocorrer um erro ao verificar o utilizador
                throw new Exception("Erro ao verificar utilizador: " + ex.Message);
            }
        }
        // Método para adicionar um administrador com o username "admin" e a password "admin"
        public static void addAdmin()
        {
            try
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
            catch (Exception ex)
            {
                // Lança uma excecao se ocorrer um erro ao adicionar o administrador
                throw new Exception("Erro ao adicionar administrador: " + ex.Message);
            }
        }
    }
}
