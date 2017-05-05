﻿using System;
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

        static string lastItem = string.Empty;
        static string condata = "Server=82.223.113.38;Database=quo605;User ID=qxt173;Password=Vehiculos12;";



        public Form1()
        {
            InitializeComponent();
            Program.vehiculos = new List<Vehiculo>();
            
        }


        /// <summary>
        /// Se ejecuta al cargar el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    
        private void Form1_Load(object sender, EventArgs e)
        {
            //Auxiliar.loadDatabase(condata, "vehiculos");
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
                Program.vehiculos = Auxiliar.Read_File("data.bin");
                foreach (Vehiculo veh in Program.vehiculos)
                {
                    if (!comboBox1.Items.Contains(veh.Marca))
                    { 
                        comboBox1.Items.Add(veh.Marca);
                    }
                }
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
        public void initForm()
        {
            bindingNavigatorPositionItem.Text = "1";
            bindingNavigatorCountItem.Text = "de 1";
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        /// <summary>
        /// Actualiza el formulario con los datos contenidos en la lista.
        /// </summary>
        /// <param name="index">Índice para la selección del elemento de la lista.</param>
        public void initForm(int index)
        {
            try
            {
                if (index >= 0)
                {
                    bindingNavigatorCountItem.Text = "de " + Program.vehiculos.Count.ToString();
                    bindingNavigatorPositionItem.Text = (index + 1).ToString();
                    textBox1.Text = Program.vehiculos[index].Matricula;
                    comboBox1.Text = Program.vehiculos[index].Marca;
                    textBox2.Text = Program.vehiculos[index].Modelo;
                    textBox3.Text = Program.vehiculos[index].Color;
                }
                else
                {
                    initForm(0);
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                if (Program.vehiculos.Count > 0)
                {
                    initForm(Program.vehiculos.Count - 1);
                }
                else
                {
                    initForm();
                }
            }
            
            
        }


        // TODO: Unir las dos funciones en una sola
        private void modList()
        {
            Program.vehiculos[int.Parse(bindingNavigatorPositionItem.Text)-1] = new Vehiculo(textBox1.Text,comboBox1.Text,textBox2.Text,textBox3.Text);            
        }

        private void modList(int index)
        {
            Program.vehiculos[index - 1] = new Vehiculo(textBox1.Text, comboBox1.Text, textBox2.Text, textBox3.Text);
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
                Auxiliar.Save_File(Program.vehiculos, "data.bin");
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
                        Auxiliar.Save_File(Program.vehiculos, save.FileName);                        
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
        /// Añade un nuevo elemento a la lista de Program.vehiculos.
        /// </summary>
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                Vehiculo nuevo = new Vehiculo(textBox1.Text, comboBox1.Text, textBox2.Text, textBox3.Text);
                Program.vehiculos.Add(nuevo);
                initForm(Program.vehiculos.Count - 1);
                if (Program.vehiculos.Count > 0)
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
            initForm(Program.vehiculos.Count - 1);
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
                    Program.vehiculos.RemoveAt(int.Parse(bindingNavigatorPositionItem.Text) - 1);
                    initForm(int.Parse(bindingNavigatorPositionItem.Text) - 2);
                }     
                if (Program.vehiculos.Count == 0)
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

        /// <summary>
        /// Se ha añadido al bindingNavigation un botón para navegar por los elementos
        /// haciendo click en su icono. Esta función mueve al programa de registro. Cumple
        /// el mismo objetivo que el KeypPress cuando se pulsa 'intro'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ir_Click(object sender, EventArgs e)
        {            
            initForm(int.Parse(bindingNavigatorPositionItem.Text) - 1);
        }


        /// <summary>
        /// Función encargada de guardar el index elemento donde se encuentra para
        /// actualizarlo antes de que se cambie el index.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
        {
           
            lastItem = bindingNavigatorPositionItem.Text;
            modList(int.Parse(lastItem));
            lastItem = string.Empty;
        }

        /// <summary>
        /// Se repite el proceso de la funcion 'bindingNavigatorPositionItem_Click' ya que el 
        /// usuario puede hacer click y arrastrar para hacer selección del texto.
        /// Solo se ejecuta si la selección es de más de un carácter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingNavigatorPositionItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (bindingNavigatorPositionItem.SelectionLength > 0)
            {
                lastItem = bindingNavigatorPositionItem.Text;
                modList(int.Parse(lastItem));
                lastItem = string.Empty;
            }
        }


        /// <summary>
        /// Opción del menu contextual que se encarga de buscar el 
        /// vehículo cuya matrícula es indicada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void matrículaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = string.Empty;
            int index = 0;
            bool found = false;

            ShowInputDialog(ref input);
            if (input != string.Empty)
            {
                foreach (Vehiculo veh in Program.vehiculos)
                {
                    if (veh.Matricula == input)
                    {
                        found = true;
                        break;
                    }
                    index++;
                }

                if (found)
                {
                    MessageBox.Show("Se ha encontrado el vehículo con matrícula " + input, "Vehículo encontrado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    initForm(index);
                }
                else
                {
                    MessageBox.Show("No se ha encontrado el vehículo con matrícula " + input, "Vehículo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Función encargada de generar un cuadro de búsqueda para 
        /// la matrícula de un vehículo. 
        /// </summary>
        /// <param name="input">String pasado por referencia que almacena la cadena introducida</param>
        /// <returns>La ventana de busqueda</returns>
        private static DialogResult ShowInputDialog(ref string input)
        {
            Size size = new Size(300, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Introduzca matrícula";

            TextBox textBox = new TextBox();
            textBox.Size = new Size(size.Width - 10, 23);
            textBox.Location = new Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button buscarBoton = new Button();
            buscarBoton.DialogResult = DialogResult.OK;
            buscarBoton.Name = "buscarBoton";
            buscarBoton.Size = new Size(75, 23);
            buscarBoton.Text = "Buscar";
            buscarBoton.Location = new Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(buscarBoton);

            Button cancelBoton = new Button();
            cancelBoton.DialogResult = DialogResult.Cancel;
            cancelBoton.Name = "cancelBoton";
            cancelBoton.Size = new Size(75, 23);
            cancelBoton.Text = "Cancelar";
            cancelBoton.Location = new Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelBoton);

            inputBox.AcceptButton = buscarBoton;
            inputBox.CancelButton = cancelBoton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        /// <summary>
        /// Opción del menú contextual encargada de guardar el fichero.
        /// Se le ha añadido la opción de guardar el fichero como .bin
        /// para poder reabrirlo más tarde con el programa de nuevo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guardarInformeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog informe = new SaveFileDialog();
            informe.Filter = "Archivo de texto|*.txt|Archivo de datos|*.bin";
            informe.Title = "Guardar informe como...";
            informe.ShowDialog();
                if (informe.FileName != "")
                {
                    if (informe.FileName.Contains(".bin"))
                    {
                        Auxiliar.Save_File(Program.vehiculos, informe.FileName);
                    }
                    else
                    {
                        Auxiliar.generarInforme(informe.FileName);
                    }
                }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivo de datos|*.bin";
            abrir.Title = "Abrir informe";
            abrir.ShowDialog();
                if (abrir.FileName != "")
                {
                    Program.vehiculos = Auxiliar.Read_File(abrir.FileName);
                    initForm(0);
                }

        }

        private void baseDeDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Auxiliar.saveDatabase(condata, "vehiculos");
        }

        
    }
}
