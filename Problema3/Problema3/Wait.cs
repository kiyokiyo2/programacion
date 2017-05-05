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
    public partial class Wait : Form
    {
        int total;
        int actual;
        public Wait(int cuenta)
        {
            InitializeComponent();
            total = cuenta;
        }

        public void step()
        {
            progressBar1.Increment(100 / total);
        }

        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Show();
            step();
        }
    }
}
