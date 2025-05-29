using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace iTasks.Models
{
    class BasedeDados: DbContext
    {
        public DbSet<Gestor> Gestor { get; set; }
        public DbSet<Programador> Programador { get; set; }
        public DbSet<TipoTarefa> TipoTarefa { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }

        private static BasedeDados _instance;

        public static BasedeDados Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BasedeDados();
                    _instance.Utilizador.Load();
                    _instance.Gestor.Load();
                    _instance.Programador.Load();
                    _instance.TipoTarefa.Load();
                    _instance.Tarefa.Load();
                }
                return _instance;
            }
        }
    }
}
