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

    }
}
