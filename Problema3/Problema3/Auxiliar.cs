using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

using System.Data;
using MySql.Data.MySqlClient;

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



        

        public static void loadDatabase(string conn, string database)
        {
            
            MySqlConnection conecta = new MySqlConnection(conn);
            Program.bbdd = new List<Vehiculo>();
            try
            {                
                conecta.Open();
                MySqlCommand com = conecta.CreateCommand();
                //com.CommandText = "update vehiculos set marca='lala' where matricula='123'";
                com.CommandText = "select * from " + database;
                IDataReader select = com.ExecuteReader();
                //com.ExecuteNonQuery();
                
                while (select.Read())
                {
                    Program.bbdd.Add(new Vehiculo((string)select["matricula"], (string)select["marca"], (string)select["modelo"], (string)select["color"]));
                }
                conecta.Close();

                /*
                string matriculas = "select matricula from vehiculos";
                MySqlDataAdapter adapt = new MySqlDataAdapter(matriculas, conecta);
                adapt.SelectCommand.CommandType = CommandType.Text;
                DataTable comparer = new DataTable();
                adapt.Fill(comparer);
                //conecta.Close();
                foreach (DataRow dr in comparer.Rows)
                {
                    Console.WriteLine(string.Format("user_id = {0}", dr["matricula"].ToString()));
                   
                }
                conecta.Open();
               
                conecta.Close();*/
                MessageBox.Show("SSSS");
            }
            catch (Exception)
            {
                MessageBox.Show("NNN");
                
            }
            finally
            {
                conecta.Close();
            }                  
        }
        
        public static void saveDatabase(string conn, string database)
        {
            bool existe = false;
            Wait barra = new Wait(Program.vehiculos.Count);
            
            
            if (Program.bbdd == null)
            {
                loadDatabase(conn, database);
            }

            MySqlConnection save = new MySqlConnection(conn);
            
            if (Program.bbdd.Count != 0)
            {
                for (int i = 0; i < Program.vehiculos.Count; i++)
                {
                    for (int j = 0; j < Program.bbdd.Count; j++) 
                    {
                        if (Program.vehiculos[i].Matricula == Program.bbdd[j].Matricula)
                        {
                            existe = true;
                            Query(save, i, j, existe);                        
                            break;
                        }

                    }
                    barra.backgroundWorker1_DoWork(barra, null);
                    if (!existe)
                    {
                        Query(save, i, 0, existe);
                    }
                }
            }
            else
            {
                barra.backgroundWorker1_DoWork(barra, null);
                for (int i = 0; i < Program.vehiculos.Count; i++)
                {
                    Query(save, i, 0, existe);
                }
            }

            MySqlConnection conecta = new MySqlConnection(conn);

        }

        public static void Query(MySqlConnection save, int i, int j, bool existe)
        {
            save.Open();
            MySqlCommand com = save.CreateCommand();
            string query;
            if (existe)
            {
                query = "";// update vehiculos set matricula='" + Program.vehiculos[i].Matricula + "','marca='" + Program.vehiculos[i].Marca + "', modelo='" + Program.vehiculos[i].Modelo + "', color='" + Program.vehiculos[i].Color + "' where matricula='" + Program.vehiculos[i].Matricula + "'";
            }
            else
            {
                query = "insert into vehiculos values('"+Program.vehiculos[i].Matricula+"','"+ Program.vehiculos[i].Marca + "','"+ Program.vehiculos[i].Modelo + "','"+ Program.vehiculos[i].Color+"');";
            }
            com.CommandText = query;
            com.ExecuteNonQuery();
            save.Close();
        }

        
        
    }
    

}