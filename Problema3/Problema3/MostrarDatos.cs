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
    /// <summary>
    /// Formulario encargado de mostrar los datos descargados de una base de datos remota
    /// </summary>
    public partial class MostrarDatos : Form
    {
        static string condata = Auxiliar.connectionData();

        /// <summary>
        /// Inicializador del formulario MostrarDatos()
        /// </summary>
        public MostrarDatos()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Descarga los datos de la base de datos remota y los muestra en un DataGridView
        /// </summary>
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

        /// <summary>
        /// Ejecuta el método load()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            load();
        }

        /// <summary>
        /// Cierra el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        
    }
}
