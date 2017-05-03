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

            List<Vehiculo> t;
        private void Form1_Load(object sender, EventArgs e)
        {
            Auxiliar.File_Load();
            t = new List<Vehiculo>();
            Vehiculo lala = new Vehiculo("1111 BBB", "Ford", "Escort", "Burdeo");
            Vehiculo lele = new Vehiculo("1112 BBB", "Ford", "Escort", "Burdeo");
            Vehiculo lili = new Vehiculo("1113 BBB", "Ford", "Escort", "Burdeo");
            Vehiculo lolo = new Vehiculo("1114 BBB", "Ford", "Escort", "Burdeo");
            t.Add(lala);
            t.Add(lele);
            t.Add(lili);
            t.Add(lolo);
            Auxiliar.Save_File(t);

            Auxiliar.Read_File();
        }



        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
