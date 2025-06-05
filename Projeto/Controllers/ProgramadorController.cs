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
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += "\n" + ex.InnerException.Message;
                if (ex.InnerException?.InnerException != null)
                    msg += "\n" + ex.InnerException.InnerException.Message;
                MessageBox.Show("Erro ao gravar gestor: " + msg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static void EditarProgramador(Programador programadorSelecionado, string nome, string username, string password, NivelExperiencia experiencia, Gestor gestorid)
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Encontra o programador pelo ID
            Programador programador = db.Programador.FirstOrDefault(p => p.id == programadorSelecionado.id);
            if (programador != null)
            {
                // Atualiza os dados do programador
                programador.nome = nome;
                programador.username = username;
                programador.password = password;
                programador.nivelExperiencia = experiencia;
                programador.idGestor = gestorid;
                // Salva as alterações na base de dados
                db.SaveChanges();
            }
        }
        public static void EliminarProgramador(Programador programadorSelecionado)
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Encontra o programador pelo ID
            Programador programador = db.Programador.FirstOrDefault(p => p.id == programadorSelecionado.id);
            if (programador != null)
            {
                // Remove o programador da base de dados
                db.Programador.Remove(programador);
                db.SaveChanges();
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
