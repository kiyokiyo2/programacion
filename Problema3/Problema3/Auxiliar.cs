using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;


using System.Data;
using MySql.Data.MySqlClient;

namespace Problema3
{
    
    class Auxiliar
    {

        #region(Ficheros)

        /// <summary>
        /// Lee el archivo y lo vuelca en una lista del tipo Vehiculo en memoria para utilizar sus datos.
        /// </summary>
        /// <param name="name">Ruta del fichero</param>
        /// <returns>List con todos los vehículos que se encontraban en el fichero</returns>
        public static List<Vehiculo> Read_File(string name)
        {
            FileStream binario = new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader leer = new BinaryReader(binario, Encoding.Unicode);
            List<Vehiculo> result = new List<Vehiculo>();
            bool lectura = true;
            while (lectura)
            {
                try
                {
                    Vehiculo aux = new Vehiculo(leer);
                    result.Add(aux);
                    lectura = true;
                }
                catch (System.IO.EndOfStreamException)
                {
                   // MessageBox.Show("FIN");
                    lectura = false;
                }
            }
            binario.Close();
            return result;
        }


        /// <summary>
        /// Guarda la información de nuevo en el fichero.
        /// </summary>
        /// <param name="save">Es la lista de tipo Vehiculo donde se encuentran los datos del programa.</param>
        /// <param name="nombre">Nombre con el que se desea guardar el fichero. Incluye la ruta absoluta</param>
        public static void Save_File(List<Vehiculo> save, string nombre)
        {
            FileStream binario = new FileStream(nombre, FileMode.Create, FileAccess.Write);
            BinaryWriter escribir = new BinaryWriter(binario, Encoding.Unicode);
            foreach (Vehiculo v in save)
            {
                v.Write(escribir);
            }
            binario.Close();
        }

        /// <summary>
        /// Guarda los datos del programa en un archivo .txt legible 
        /// </summary>
        /// <param name="name">Nombre con el que se desea guardar el fichero. Incluye la ruta absoluta</param>
        public static void generarInforme(string name)
        {
            int count = 1;
            StreamWriter salida = new StreamWriter(name);

            salida.WriteLine("--- LISTADO DE VEHÍCULOS ---\n");
            salida.WriteLine("-- NÚMERO TOTAL DE VEHÍCULOS: {0} --\n", Program.vehiculos.Count);
            salida.WriteLine(" ");
            foreach (Vehiculo veh in Program.vehiculos)
            {
                salida.WriteLine("-- Vehículo nº {0} --", count);
                salida.WriteLine("Matrícula: {0} ", veh.Matricula);
                salida.WriteLine("Marca: {0} ", veh.Marca);
                salida.WriteLine("Modelo: {0} ", veh.Modelo);
                salida.WriteLine("Color: {0} ", veh.Color);
                salida.WriteLine("-------------------\n");
                count++;
            }
            salida.Dispose();
        }



        #endregion

        /// <summary>
        /// Se encarga de conectar a la base de datos y descargar la información de la tabla indicada
        /// </summary>
        /// <param name="conn">Parámetros de la conexión</param>
        /// <param name="database">Tabla de la base de datos a la que se va a conectar</param>
        /// <returns>True si la conexión ha sido exitosa.</returns>
        public static bool loadDatabase(string conn, string database)
        {
            
            MySqlConnection conecta = new MySqlConnection(conn);
            Program.bbdd = new List<Vehiculo>();
            try
            {                
                conecta.Open();
                MySqlCommand com = conecta.CreateCommand();
                com.CommandText = "select * from " + database;
                IDataReader select = com.ExecuteReader();
                
                while (select.Read())
                {
                    Program.bbdd.Add(new Vehiculo((string)select["matricula"], (string)select["marca"], (string)select["modelo"], (string)select["color"]));
                }
                conecta.Close();
                MessageBox.Show("Conexión realizada con éxito.");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Conexión fallida. Compruebe su conexión a internet y vuelva a intentarlo.");
                return false;
                
            }                         
        }
        
        
        /// <summary>
        /// Actualiza o añade un registro a la base de datos
        /// </summary>
        /// <param name="save">Conexión a la base de datos</param>
        /// <param name="i">Contador de la lista de vehículos. Indica qué miembro se va a leer</param>
        /// <param name="existe">Es verdadero si el elemento existe y hay que actualizarlo</param>
        public static void Query(MySqlConnection save, int i, bool existe)
        {
            save.Open();
            MySqlCommand com = save.CreateCommand();
            string query;
            if (existe)
            {
                query = "update vehiculos set matricula = '" + Program.vehiculos[i].Matricula + "', marca = '" + Program.vehiculos[i].Marca + "', modelo = '" + Program.vehiculos[i].Modelo + "', color = '" + Program.vehiculos[i].Color + "' where matricula = '" + Program.vehiculos[i].Matricula + "';";
            }
            else
            {
                query = "insert into vehiculos values('" + Program.vehiculos[i].Matricula + "','" + Program.vehiculos[i].Marca + "','" + Program.vehiculos[i].Modelo + "','" + Program.vehiculos[i].Color + "');";
            }
            com.CommandText = query;
            com.ExecuteNonQuery();
            save.Close();
        }

        
        
    }
    

}