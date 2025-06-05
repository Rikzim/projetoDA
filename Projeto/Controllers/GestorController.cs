using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    class GestorController
    {
        // Método para gravar um novo gestor na base de dados
        public static void GravarGestor(string nome, string username, string password, Departamento departamento, bool GereUtilizadores)
        {
            // Cria uma instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            if (db.Utilizador.Any(u => u.username == username))
            {
                throw new Exception("Já existe um utilizador com esse username. Por favor, escolha outro.");
            }
            // Verifica se o gestor já existe
            db.Utilizador.Add(new Gestor(nome, username, password, departamento, GereUtilizadores));
                // Adiciona o novo gestor à tabela de utilizadores
            db.SaveChanges();
        }
        // Método para listar todos os gestores na base de dados
        public static List<Gestor> ListarGestores()
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Retorna uma lista de gestores filtrando a tabela de utilizadores
            return db.Utilizador.OfType<Gestor>().ToList();
        }
        // Método para contar o número de gestores na base de dados
        public static int countGestor()
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de gestores na base de dados e adiciona 1, começando em 1 se não houver nenhum
            int count = db.Gestor.Count();
            return count + 1;
        }
    }
}
