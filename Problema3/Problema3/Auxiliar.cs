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
        public static List<string> Convertir(List<Vehiculo> vehiculos)
        {
            List<string> data = new List<string>();
            foreach (Vehiculo veh in vehiculos)
            {
                data.Add(veh.Matricula);
                data.Add(veh.Marca);
                data.Add(veh.Modelo);
                data.Add(veh.Color);
                data.Add("|");
            }

            data.RemoveAt(data.Count - 1);
            data.Add("$");
            return data;
        }



        /// <summary>
        /// Comprueba la existencia de un archivo de datos. En caso de que no exista, lo crea.
        /// </summary>
        public static void File_Load()
        {
            if (File.Exists("data.bin"))
            {
                string mensaje = "Se ha encontrado un fichero de datos.\nSe utilizará dicho fichero.";
                string titulo = "Fichero encontrado";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Read_File();
            }
            else
            {
                string mensaje = "No se ha encontrado un fichero de datos.\nSe creará uno por defecto";
                string titulo = "Nuevo fichero";
                var result = MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                File.Create("data.bin");
            }

        }

        /// <summary>
        /// Lee el archivo y lo vuelca en un List<Vehiculo> en memoria para utilizar sus datos.
        /// </summary>
        public static void Read_File()
        {
            FileStream binario = new FileStream("data.bin", FileMode.Open, FileAccess.Read);
            BinaryReader leer = new BinaryReader(binario, Encoding.Unicode);
            
            
        }


        /// <summary>
        /// Guarda la información de nuevo en el fichero.
        /// </summary>
        /// <param name="save">Es la List<Vehiculo> donde se encuentran los datos del programa.</param>
        public static void Save_File(List<Vehiculo> save)
        {
            FileStream binario = new FileStream("data.bin", FileMode.Open, FileAccess.Write);
            BinaryWriter escribir = new BinaryWriter(binario, Encoding.Unicode);
            foreach (Vehiculo v in save)
            {
                v.Write(escribir);
            }
            binario.Close();
        }
    }
}