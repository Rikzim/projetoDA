using iTasks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmLogin : Form
    {
        BasedeDados db;
        public frmLogin()
        {
            InitializeComponent();

            db = new BasedeDados();

            Gestor gestor = new Gestor("Jão","Jão19293","123456789", Departamento.Marketing, true);

            db.Gestor.Add(gestor);

            db.SaveChanges();

            Programador programador = new Programador("henrik","Rikzim", "987654321", NivelExperiencia.Junior, gestor);

            db.Programador.Add(programador);
            db.SaveChanges();

            MessageBox.Show($"Programador: {programador.nome} \n Gestor dele: {programador.idGestor.nome}");
        }
    }
}
