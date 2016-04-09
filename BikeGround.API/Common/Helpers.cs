using BikeGround.DataLayer.Repositories;
using BikeGround.Models;
using MicroOrm.Pocos.SqlGenerator;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BikeGround.API.Common
{
    public static class Helpers
    {
        public static string GetClientIp(string x_forwarded_for, string userhostaddress)
        {
            if (String.IsNullOrEmpty(x_forwarded_for))
            {
                return userhostaddress;
            }

            return x_forwarded_for;
        }

        /// <summary>
        /// sets agreed datetime json format
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToJsonStringDate(DateTime date)
        {
            return date.ToString("yyyy-MM-ddT00:00:00Z");
        }

        /// <summary>
        /// gets date from params date format
        /// </summary>
        /// <param name="date">in format yyyy-MM-dd</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateFromParam(string date)
        {
            return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Get Hash
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        /// <summary>
        /// Get hash string
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        /// <summary>
        /// Dohvaća UserID za zadani username
        /// </summary>
        /// <returns></returns>
        public static long GetUserIDFromDB(string username)
        {
            SqlConnection sqlCon = new SqlConnection(AppInit.Instance.ConnectionString);
            ISqlGenerator<AspNetUsers> sqlGenerator = new SqlGenerator<AspNetUsers>();
            AspNetUsers item = new AspNetUsers();

            try
            {
                AspNetUsersRepository r = new AspNetUsersRepository(sqlCon, sqlGenerator);
                item = r.GetFirst(new { Email = username });

                if (item == null)
                {
                    throw new Exception("Invalid data");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid data");
            }

            return item.Id;
        }

        public static long GetUserIDFromClaims(ClaimsPrincipal principal)
        {
            try
            {
                return Convert.ToInt64(principal.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Single().Value);
            }
            catch (Exception ex)
            {
                // Ovo je napravljeno jer se exception ne može hvatati prije ili tijekom instanciranja contollera. Vratim 0 i u nastavku pipeline vodi u authorizaciju koja neće proći i vratit će Internal Server Error
                // Testirano sa lošim tokenom i radi
                return 0;
            }
        }
    }
}