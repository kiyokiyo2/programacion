using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Problema3
{
    public partial class MostrarDatos : Form
    {
        static string condata = "Server=82.223.113.38;Database=quo605;User ID=qxt173;Password=Vehiculos12;";

        public MostrarDatos()
        {
            InitializeComponent();
            
        }

        public void load()
        {
            dataGridView1.ColumnCount = 4;
            dataGridView1.ColumnHeadersVisible = true;

            dataGridView1.Columns[0].Name = "Matrícula";
            dataGridView1.Columns[1].Name = "Marca";
            dataGridView1.Columns[2].Name = "Modelo";
            dataGridView1.Columns[3].Name = "Color";

            Auxiliar.loadDatabase(condata,"vehiculos");

            foreach(Vehiculo veh in Program.bbdd)
            {
                string [] row = new string[] {veh.Matricula,veh.Marca,veh.Modelo,veh.Color};
                dataGridView1.Rows.Add(row);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        
    }
}
