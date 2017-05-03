using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Problema3
{
    class Vehiculo
    {
        string matricula;
        string marca;
        string modelo;
        string color;
    

        const int TAM_MATRICULA = 20;
        const int TAM_MARCA = 30;
        const int TAM_MODELO = 30;
        const int TAM_COLOR = 20;
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


        //
        // CONSTRUCTOR
        //
        public Vehiculo(string matricula, string marca, string modelo, string color)
        {            
            this.matricula = matricula;
            this.marca = marca;
            this.modelo = modelo;
            this.color = color;
        }

      
       
        // Constructor que lee los datos de un BinaryReader
        public Vehiculo(BinaryReader reader)
        {
            Read(reader);
        }

        //
        // METODOS
        //



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

        

        /// <summary>
        /// Guarda el objeto en un stream de tipo BinaryWriter con un registro de tamaño fijo
        /// </summary>
        /// <param name="writer">BinaryWriter en el que guardamos el objeto</param>

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
        /// con el método Write del objeto Persona.
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
            // Aunque el tipo es float, en el BinaryWriter solo escribe 'double'
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

    }
}
