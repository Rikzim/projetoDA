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
            try
            {
                // Cria uma instância da base de dados
                BasedeDados db = BasedeDados.Instance;
                // Verifica se o gestor já existe
                db.Utilizador.Add(new Gestor(nome, username, password, departamento, GereUtilizadores));
                // Adiciona o novo gestor à tabela de utilizadores
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Verifica se a exceção é por violação de restrição única
                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.Contains("IX_username")) // ou o nome do índice criado
                    throw new Exception("Já existe um utilizador com esse username.");
                else
                    throw new Exception("Erro ao salvar gestor: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Lança uma exceção se ocorrer um erro ao gravar o gestor
                throw new Exception("Erro ao gravar gestor: " + ex.Message);
            }
        }
        // Método para listar todos os gestores na base de dados
        public static List<Gestor> ListarGestores()
        {
            try
            {
                // Obtém a instância da base de dados
                BasedeDados db = BasedeDados.Instance;
                // Retorna uma lista de gestores filtrando a tabela de utilizadores
                return db.Utilizador.OfType<Gestor>().ToList();
            }
            catch (Exception ex)
            {
                // Lança uma exceção se ocorrer um erro ao listar os gestores
                throw new Exception("Erro ao listar gestores: " + ex.Message);
            }
        }
        // Método para contar o número de gestores na base de dados
        public static int countGestor()
        {
            try
            {
                // Obtém a instância da base de dados
                BasedeDados db = BasedeDados.Instance;
                // Conta o número de gestores na base de dados e adiciona 1, começando em 1 se não houver nenhum
                int count = db.Gestor.Count();
                return count + 1;
            }
            catch (Exception ex) 
            {
                // Lança uma exceção se ocorrer um erro ao contar os gestores
                throw new Exception("Erro ao contar gestores" + ex.Message);
            }
        }
    }
}
