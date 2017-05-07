using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Problema3
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Explorador());

        }

        /// <summary>
        /// Se declara
        /// la lista de datos de vehículos
        /// </summary>        
        public static List<Vehiculo> vehiculos;

        /// <summary>
        /// Se declara 
        /// la lista de datos de vehículos (descargada de internet)
        /// </summary>
        public static List<Vehiculo> bbdd;
    }
}
