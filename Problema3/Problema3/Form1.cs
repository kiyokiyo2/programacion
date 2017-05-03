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
        public Form1()
        {
            InitializeComponent();
        }     
              
        private void Form1_Load(object sender, EventArgs e)
        {
            File_Load();
        }

        private void File_Load()
        {
            if (File.Exists("data.bin"))
            {
                string mensaje = "Se ha encontrado un fichero de datos.\nSe utilizará dicho fichero.";
                string titulo = "Fichero encontrado";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                string mensaje ="No se ha encontrado un fichero de datos.\nSe creará uno por defecto";
                string titulo ="Nuevo fichero";
                var result = MessageBox.Show(mensaje, titulo,MessageBoxButtons.OK,MessageBoxIcon.Hand);
                File.Create("data.bin");
            }

        }


        private void Read_File()
        {
            FileStream binario = new FileStream("data.bin", FileMode.Open, FileAccess.Read);
            BinaryReader leer = new BinaryReader(binario, Encoding.Unicode);

        }

        private void Save_File(List<Vehiculo> save)
        {
            FileStream binario = new FileStream("data.bin", FileMode.Open, FileAccess.Write);
            BinaryWriter escribir = new BinaryWriter(binario, Encoding.Unicode);            
        }


    }
}
