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
            vehiculos = new List<Vehiculo>();     
        }


            
        private void Form1_Load(object sender, EventArgs e)
        {
            File_Load();

            
           /* 
            *   Código utilizado para rellenar el archivo con datos.
            * 
            
            List<Vehiculo> t = new List<Vehiculo>();
            Vehiculo lala = new Vehiculo("1111 BBB", "Ford", "Escort", "Burdeo");
            Vehiculo lele = new Vehiculo("1112 BBB", "Ford", "Escort", "Burdeo");
            Vehiculo lili = new Vehiculo("1113 BBB", "Ford", "Escort", "Burdeo");
            Vehiculo lolo = new Vehiculo("1114 BBB", "Ford", "Escort", "Burdeo");
            t.Add(lala);
            t.Add(lele);
            t.Add(lili);
            t.Add(lolo);
            Auxiliar.Save_File(t,"data.bin");
            
            * 
            * 
            */


            
        }

        /// <summary>
        /// Comprueba la existencia de un archivo de datos y lo carga. En caso de que no exista, lo crea.
        /// </summary>
        public void File_Load()
        {
            if (File.Exists("data.bin"))
            {
                string mensaje = "Se ha encontrado un fichero de datos.\nSe utilizará dicho fichero.";
                string titulo = "Fichero encontrado";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                vehiculos = Auxiliar.Read_File();
                initForm(0);
                
            }
            else
            {
                string mensaje = "No se ha encontrado un fichero de datos.\nSe creará uno por defecto";
                string titulo = "Nuevo fichero";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                File.Create("data.bin");
                initForm();
                
                
            }

        }


        /// <summary>
        /// Inicializa el formulario cuando no hay datos que mostrar
        /// </summary>
        private void initForm()
        {
            bindingNavigatorPositionItem.Text = "1";
            bindingNavigatorCountItem.Text = "de 1";
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        /// <summary>
        /// Rellena el formulario con los datos contenidos en la lista.
        /// </summary>
        /// <param name="index">Índice para la selección del elemento de la lista.</param>
        private void initForm(int index)
        {
            try
            {
                if (index >= 0)
                {
                    bindingNavigatorCountItem.Text = "de " + vehiculos.Count.ToString();
                    bindingNavigatorPositionItem.Text = (index + 1).ToString();
                    textBox1.Text = vehiculos[index].Matricula;
                    comboBox1.Text = vehiculos[index].Marca;
                    textBox2.Text = vehiculos[index].Modelo;
                    textBox3.Text = vehiculos[index].Color;
                }
                else
                {
                    initForm(0);
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                if (vehiculos.Count > 0)
                {
                    initForm(vehiculos.Count - 1);
                }
                else
                {
                    initForm();
                }
            }
            
            
        }


        
        //TODO ERROR PQ COGE EL NUMERO DEL CUADRO Y AL 
        //TODO CAMBIAR DE CUADRO SE CAMBIA OTRO REGISTRO DISTINTO.
        private void modList()
        {
            vehiculos[int.Parse(bindingNavigatorPositionItem.Text)-1] = new Vehiculo(textBox1.Text,comboBox1.Text,textBox2.Text,textBox3.Text);            
        }







        // MENÚ CONTEXTUAL


        /// <summary>
        /// Se encarga del control de salida del programa. Pregunta si desea guardar los cambios realizados
        /// y cierra el programa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mensaje = "¿Desea guardar los cambios al fichero?\nSe sobreescribirán los datos existentes.";
            string titulo = "Salir";
            var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo);
            SaveFileDialog save = new SaveFileDialog();            
            if (result == DialogResult.Yes)
            {
                Auxiliar.Save_File(vehiculos, "data.bin");
            }
            else
            {
                mensaje = "¿Desea guardar los cambios en un fichero nuevo?";
                titulo = "Salir";
                result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    save.Filter = "Archivo de datos|*.bin";
                    save.Title = "Guardar archivo como...";
                    save.ShowDialog();
                    if (save.FileName != "")
                    {
                        Auxiliar.Save_File(vehiculos, save.FileName);                        
                    }
                }                
            }
            Application.Exit();
        }



        // 
        // // //        CONTROLES DE LA
        // // //        BARRA DE NAVEGACIÓN
        //
        

        /// <summary>
        /// Responde al click para ir al siguiente elemento del la lista.
        /// </summary>
        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            modList();
            initForm(int.Parse(bindingNavigatorPositionItem.Text));
        }


        /// <summary>
        /// Añade un nuevo elemento a la lista de vehiculos.
        /// </summary>
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                Vehiculo nuevo = new Vehiculo(textBox1.Text, comboBox1.Text, textBox2.Text, textBox3.Text);
                vehiculos.Add(nuevo);
                initForm(vehiculos.Count - 1);
                if (vehiculos.Count > 0)
                {
                    bindingNavigatorDeleteItem.Enabled = true;
                }
            }
            else
            {
                string mensaje = "Rellene todos los campos.";
                string titulo = "Añadir registro.";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// Responde al click para ir al elemento anterior del la lista.
        /// </summary>
        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            modList();
            initForm(int.Parse(bindingNavigatorPositionItem.Text) -2);
        }


        /// <summary>
        /// Navega hasta el primer elemento
        /// </summary>
        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            modList();
            initForm(0);
        }


        /// <summary>
        /// Navega hasta el último elemento
        /// </summary>
        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            modList();
            initForm(vehiculos.Count - 1);
        }

        /// <summary>
        /// Control para que no se puedan introducir más que números en el cuadro de búsqueda.
        /// Se incluye el evento 'intro' para que pueda acceder al registro indicado.
        /// </summary>
        /// <param name="e">Tecla que se ha pulsado para ser introducida en el campo</param>
        private void bindingNavigatorPositionItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                initForm(int.Parse(bindingNavigatorPositionItem.Text) - 1);

            } else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }            
        }

        /// <summary>
        /// Se encarga de eliminar el elemento que se está visualizando de la lista.
        /// </summary>
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                string mensaje = "¿Desea eliminar el registro actual?\nPara recuperarlo, tendrá que recargar el archivo.";
                string titulo = "Borrado";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                SaveFileDialog save = new SaveFileDialog();
                if (result == DialogResult.Yes)
                {
                    vehiculos.RemoveAt(int.Parse(bindingNavigatorPositionItem.Text) - 1);
                    initForm(int.Parse(bindingNavigatorPositionItem.Text) - 2);
                }     
                if (vehiculos.Count == 0)
                {
                    bindingNavigatorDeleteItem.Enabled = false;
                }
            }
            catch (Exception)
            {
                string mensaje = "No hay registros";
                string titulo = "Borrado";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

        private void Ir_Click(object sender, EventArgs e)
        {
            modList();
            initForm(int.Parse(bindingNavigatorPositionItem.Text) - 1);
        }



    }
}
