using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Problema3
{
    class Auxiliar
    {
        

        /// <summary>
        /// Lee el archivo y lo vuelca en un List<Vehiculo> en memoria para utilizar sus datos.
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
        /// <param name="save">Es la List<Vehiculo> donde se encuentran los datos del programa.</param>
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
        
    }
}