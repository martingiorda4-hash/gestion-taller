using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestion_taller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void verTrabajosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trabajos trabajos = new Trabajos();
            trabajos.Show();
        }

        private void agregarTrabajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarClientes agregarClientes = new AgregarClientes();
            agregarClientes.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
