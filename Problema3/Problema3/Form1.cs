using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Problema3
{
    public partial class Form1 : Form
    {

        static List<Vehiculo> vehiculos;




        public Form1()
        {
            InitializeComponent();
            
        }


            
        private void Form1_Load(object sender, EventArgs e)
        {
            File_Load();
        }

        /// <summary>
        /// Comprueba la existencia de un archivo de datos y lo carga. En caso de que no exista, lo crea.
        /// </summary>
        public static void File_Load()
        {
            if (File.Exists("data.bin"))
            {
                string mensaje = "Se ha encontrado un fichero de datos.\nSe utilizará dicho fichero.";
                string titulo = "Fichero encontrado";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                vehiculos = Auxiliar.Read_File();
            }
            else
            {
                string mensaje = "No se ha encontrado un fichero de datos.\nSe creará uno por defecto";
                string titulo = "Nuevo fichero";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                File.Create("data.bin");
            }

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
