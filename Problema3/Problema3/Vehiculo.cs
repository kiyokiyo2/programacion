using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Problema3
{
    /// <summary>
    /// Almacena los datos de vehículos y los métodos relacionados
    /// </summary>
    class Vehiculo
    {
        string matricula;
        string marca;
        string modelo;
        string color;
    

        const int TAM_MATRICULA = 15;
        const int TAM_MARCA = 20;
        const int TAM_MODELO = 40;
        const int TAM_COLOR = 15;

                #region(Constructores)

        public string Color
        {
            get
            {
                return color;
            }

        }

        public string Matricula
        {
            get
            {
                return matricula;
            }
        }

        public string Marca
        {
            get
            {
                return marca;
            }
        }

        public string Modelo
        {
            get
            {
                return modelo;
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="matricula">Identificación del vehículo</param>
        /// <param name="marca">Marca del vehículo</param>
        /// <param name="modelo">Modelo del vehículo</param>
        /// <param name="color">Color del vehículo</param>
        public Vehiculo(string matricula, string marca, string modelo, string color)
        {            
            this.matricula = matricula;
            this.marca = marca;
            this.modelo = modelo;
            this.color = color;
        }

      
       
        /// <summary>
        /// Lee los datos de un vehiculo a traves de un BinaryReader
        /// </summary>
        /// <param name="reader">Origen de los datos del vehículo</param>
        public Vehiculo(BinaryReader reader)
        {
            Read(reader);
        }





        public static int TamanyoRegistro
        {
            get
            {
                return
                    /* matricula */        TAM_MATRICULA * 2 + 4 + /* 4 = longitud de la cadena, 2 bytes por caracter */
                    /* marca */            TAM_MARCA * 2 + 4 +
                    /* modelo */           TAM_MODELO * 2 + 4 +
                    /* color */            TAM_COLOR * 2 + 4;/* tamaño de un double */
            }
        }

        #endregion

                #region(Funciones de lectura y escritura de vehículos en un fichero)
        /// <summary>
        /// Guarda el vehículo en un stream de tipo BinaryWriter con un registro de tamaño fijo
        /// </summary>
        /// <param name="writer">BinaryWriter en el que guardamos el vehículo</param>

        public void Write(BinaryWriter writer)
        {
            // Escribimos los datos
            // Como todos los registros deben tener el mismo tamaño
            // calculamos el espacio en el que escribimos para rellenar
            // hasta cubrir el ancho deseado
            long posInicio = writer.BaseStream.Position;
            writer.Write(matricula);
            writer.Write(marca);
            writer.Write(modelo);
            writer.Write(color);
            long posFin = writer.BaseStream.Position;

            // Rellenamos al final para que el registro tenga siempre el mismo tamaño
            int nBytes = TamanyoRegistro - (int)(posFin - posInicio);
            byte[] buffer = new byte[nBytes];

            // Rellenamos con puntos para facilitar análisis del fichero
            // En la versión definitiva del programa esto no es necesario
            for (int i = 0; i < buffer.Length; i++) buffer[i] = (byte)'.';

            writer.BaseStream.Write(buffer, 0, nBytes);
        }

        /// <summary>
        /// Lee un objeto del stream BinaryReader. Este objeto debe haber sido guardado
        /// con el método Write del objeto Vehículo.
        /// 
        /// Si el formato es incorrecto o se ha llegado al final del stream
        /// el metodo lanzará la excepción apropiada
        /// </summary>
        /// <param name="reader">BinaryReader del que leemos el objeto</param>
        public void Read(BinaryReader reader)
        {
            // Leemos los datos en el mismo orden que los hemos escrito

            long posInicio = reader.BaseStream.Position;

            matricula = reader.ReadString();
            marca = reader.ReadString();
            modelo = reader.ReadString();            
            color = reader.ReadString();

            long posFin = reader.BaseStream.Position;
            // Avanzamos el puntero de lectura para saltar el relleno, escrito anteriormente
            // y que la siguiente operación de lectura este sobre un registro valido
            reader.BaseStream.Position += TamanyoRegistro - (int)(posFin - posInicio);

        }

        /// <summary>
        /// Situa el puntero de lectura/escritura de un flujo en la posición
        /// en la que empieza un registro
        /// </summary>
        /// <param name="st">Stream en el que movemos el puntero</param>
        /// <param name="nReg">Nº de registro al que vamos. 0 el primero, 1 el segundo, ...</param>
        public static void SituaPunteroEnRegistro(Stream st, int nReg)
        {
            st.Seek(nReg * TamanyoRegistro, SeekOrigin.Begin);
        }

        #endregion
    }
}
