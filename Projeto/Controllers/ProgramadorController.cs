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
    class ProgramadorController
    {
        // Método para gravar um novo programador na base de dados
        public static void GravarProgramador(string nome, string username, string password, NivelExperiencia experiencia, Gestor gestorid)
        {
            try
            {
                // Cria uma instância da base de dados
                BasedeDados db = BasedeDados.Instance;
                // Verifica se o gestor já existe
                var gestor = db.Gestor.Find(gestorid.id);
                //Adiciona o novo programador à tabela de utilizadores
                db.Utilizador.Add(new Programador(nome, username, password, experiencia, gestor));
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Verifica se a exceção é por violação de restrição única
                if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("IX_username")) // ou o nome do índice criado
                {
                    MessageBox.Show("Já existe um utilizador com esse username.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Erro ao salvar utilizador: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Lança uma exceção se ocorrer um erro ao gravar o programador
                throw new Exception("Erro ao gravar programador: " + ex.Message);
            }
        }

        public static List<Programador> ListarProgramadores()
        {
            BasedeDados db = BasedeDados.Instance;
            // Retorna uma lista de programadores filtrando a tabela de utilizadores
            return db.Programador.ToList();
        }
        public static List<Programador> ListarProgramadoresPorGestor(Utilizador GestorRecebido)
        {
            try
            {
                BasedeDados db = BasedeDados.Instance;
                // Retorna uma lista de programadores filtrando a tabela de utilizadores
                return db.Programador.Where(t => t.idGestor.id == GestorRecebido.id).ToList();
            }
            catch (Exception ex)
            {
                // Lança uma exceção se ocorrer um erro ao listar os programadores
                throw new Exception("Erro ao listar programadores: " + ex.Message);
            }
        }

        public static int countProgramador()
        {

            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Conta o número de programadores na base de dados e adiciona 1, começando em 1 se não houver nenhum
            int count = db.Programador.Count();
            return count + 1;
        }
    }
}
