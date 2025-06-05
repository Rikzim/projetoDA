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
        // Método para gravar um novo gestor na base de dados
        public static void GravarGestor(string nome, string username, string password, Departamento departamento, bool GereUtilizadores)
        {
            // Cria uma instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Verifica se o gestor já existe
            db.Utilizador.Add(new Gestor(nome, username, password, departamento, GereUtilizadores));
            // Adiciona o novo gestor à tabela de utilizadores
            db.SaveChanges();
        }
        public static void EditarGestor(Gestor gestorSelecionado, string nome, string username, string password, Departamento departamento, bool GereUtilizadores)
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;
            // Encontra o gestor pelo ID
            Gestor gestor = db.Gestor.FirstOrDefault(g => g.id == gestorSelecionado.id);
            if (gestor != null)
            {
                // Atualiza os dados do gestor
                gestor.nome = nome;
                gestor.username = username;
                gestor.password = password;
                gestor.departamento = departamento;
                gestor.gereUtilizadores = GereUtilizadores;
                // Salva as alterações na base de dados
                db.SaveChanges();
            }
        }
        public static void EliminarGestor(Gestor gestorSelecionado, Utilizador gestorLogado)
        {
            // Obtém a instância da base de dados
            BasedeDados db = BasedeDados.Instance;

            // Encontra o gestor pelo ID
            Gestor gestor = db.Gestor.FirstOrDefault(g => g.id == gestorSelecionado.id);

            // Verifica se o gestor a eliminar é o gestor logado
            if (gestorLogado != null && gestor.id == gestorLogado.id)
            {
                // Não permite eliminar o próprio gestor logado
                throw new InvalidOperationException("Não é possível eliminar o gestor logado.");
            }

            // Remove o gestor da base de dados
            db.Gestor.Remove(gestor);
            // Salva as alterações na base de dados
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
