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
    /// Formulario de información
    /// </summary>
    public partial class Acerca : Form
    {
        /// <summary>
        /// Inicializa el formulario
        /// </summary>
        public Acerca()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Abre al navegador al hacer click en el link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;

            System.Diagnostics.Process.Start("https://github.com/kiyokiyo2/programacion");
        }
    }
}
