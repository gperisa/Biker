using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;

namespace ContexGenerator
{
    public class General
    {
        private static string CS
        {
            get
            {
                return "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Integrated Security=True";
            }
        }

        /// <summary>
        /// Dohvati podatke iz baze na temelju upita
        /// </summary>
        /// <param name="SQL">SQL upit</param>
        /// <param name="server">Server</param>
        /// <param name="baza">Baza</param>
        /// <param name="user">User</param>
        /// <param name="pass">Password</param>
        /// <returns></returns>
        public static DataTable runSQL(string SQL, string server, string baza, string user, string pass)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(String.Format(CS, server, baza, user, pass));
            SqlCommand comm = new SqlCommand(SQL, conn);
            SqlDataAdapter da = new SqlDataAdapter(comm);

            try
            {
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Greška prilikom dohvata podataka: {0}", ex.Message), "Greška!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return dt;
        }

        public static void primjeniRegex(Color boja, RichTextBox rtb, string[] uzorak)
        {
            string pattern = string.Join("|", uzorak.Select(w => Regex.Escape(w)));
            Regex r = new Regex(pattern);

            MatchCollection coll = r.Matches(rtb.Text);

            foreach (Match m in coll)
            {
                rtb.SelectionStart = m.Index;
                rtb.SelectionLength = m.Length;
                rtb.SelectionColor = boja;
            }
        }
    }

    public static class RegexUzorci
    {
        public static string[] zeleno
        {
            get
            {
                return new string[] { "Display", "Required", "StringLength", "String", "Resources\\.\\w+", "\"\\w+\"", "DataType", "KeyProperty" };
            }
        }

        public static string[] plavo
        {
            get
            {
                return new string[] { "public", "partial", "string", "int", "long", "get", "set", "typeof", "class", "interface", "using", "namespace", "override", "return" };
            }
        }

        public static string[] crveno
        {
            get
            {
                return new string[] { "\"\\w+\"" };
            }
        }
    }

    public static class Putanje
    {
        public static string logedUser { get; set; }

        public static string pathKlase
        {
            get
            {
                if (logedUser == "petar")
                {
                    return @"E:\Projekti\Biker\BikeGround.Models\Classes";
                }
                else if (logedUser == "goran")
                {
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }

        public static string pathModul
        {
            get
            {
                if (logedUser == "petar")
                {
                    return @"E:\Projekti\Biker\BikeGround.Web\App";
                }
                else if (logedUser == "goran")
                {
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }

        public static string pathFactory
        {
            get
            {
                if (logedUser == "petar")
                {
                    return @"E:\Projekti\Biker\BikeGround.Web\App\Services";
                }
                else if (logedUser == "goran")
                {
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }

        public static string pathRepository
        {
            get
            {
                if (logedUser == "petar")
                {
                    return @"E:\Projekti\Biker\BikeGround.DataLayer\Repositories";
                }
                else if (logedUser == "goran")
                {
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }

        public static string pathResources
        {
            get
            {
                if (logedUser == "petar")
                {
                    return @"E:\Projekti\Biker\BikeGround.Models\Resources";
                }
                else if (logedUser == "goran")
                {
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }

        public static string pathMVCController
        {
            get
            {
                if (logedUser == "petar")
                {
                    return @"E:\Projekti\Biker\BikeGround.Web\Controllers";
                }
                else if (logedUser == "goran")
                {
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }

        public static string pathAPIController
        {
            get
            {
                if (logedUser == "petar")
                {
                    return @"E:\Projekti\Biker\BikeGround.API\Controllers";
                }
                else if (logedUser == "goran")
                {
                    return "";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}