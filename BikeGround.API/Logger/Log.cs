using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BikeGround.API.Logger
{
    public class Log
    {
        /// <summary>
        ///     Logs the service request.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="function">The function.</param>
        /// <param name="status">The status.</param>
        /// <param name="MachineIPShort">Machine IP</param>
        public static void LogServiceRequest(string username, string function, int status, string ip,
            string MachineIPShort, string query, string exception)
        {
            using (SqlConnection conn = new SqlConnection(AppInit.Instance.ConnectionString))
            {
                string insertLog = "INSERT into Log (Date,UserName,UserIP,Status, Query, Message,Method) VALUES (@Date,@UserName,@UserIP,@Status, @Query, @Message, @Method)";

                using (SqlCommand command = new SqlCommand(insertLog))
                {
                    command.Connection = conn;
                    command.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Parse(DateTime.Now.ToString());
                    command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = username;
                    command.Parameters.Add("@UserIP", SqlDbType.VarChar).Value = ip;
                    command.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;
                    command.Parameters.Add("@Query", SqlDbType.VarChar).Value = query;
                    command.Parameters.Add("@Message", SqlDbType.VarChar).Value = exception;
                    command.Parameters.Add("@Method", SqlDbType.VarChar).Value = function;

                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
    }
}