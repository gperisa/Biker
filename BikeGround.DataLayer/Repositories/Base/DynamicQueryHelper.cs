using Dapper;
using System;
using System.Collections.Generic;

namespace BikeGround.DataLayer.Repositories.Base
{
    /// <summary>
    /// Pomoćna klasa koja služi prilikom kreiranja dinamičkih upita koji se
    /// inače ne mogu raditi preko dappera (podrazumjevaju složene join operacije)
    /// </summary>
    public class DynamicQueryHelper
    {
        /// <summary>
        /// Atributi
        /// </summary>
        public List<string> atributes;

        /// <summary>
        /// Parametri
        /// </summary>
        public List<string> parameters;

        /// <summary>
        /// DynamicParameters, SQL injecion safe vrijednosti za prosljeđivanje bazi
        /// </summary>
        public DynamicParameters dbArgs;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public DynamicQueryHelper()
        {
            atributes = new List<string>();
            parameters = new List<string>();
            dbArgs = new DynamicParameters();
        }

        /// <summary>
        /// Vraća popis atributa koje dohvaćamo iz tablica. Atributi se formatiraju
        /// kao [dbo].[Tablica].[Atribut]
        /// </summary>
        public string getAtributes
        {
            get
            {
                return String.Join(",", atributes);
            }
        }

        /// <summary>
        /// Parametri koji će biti u WHERE uvjetu. Formatirani kao: [dbo].[Tablica].[Atribut] = @Atribut
        /// </summary>
        public string getParametars
        {
            get
            {
                return String.Join(" AND ", parameters);
            }
        }
    }
}
