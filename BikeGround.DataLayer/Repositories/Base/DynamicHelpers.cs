using BikeGround.Interfaces;
using BikeGround.Models;
using Dapper;
using System;
using System.Reflection;

namespace BikeGround.DataLayer.Repositories.Base
{
    /// <summary>
    /// Klasa koja definira dynamic helpere za kreiranje atributa, parametara i dbArgumenata SQL
    /// upita na temelju IDynamicQuery objekta
    /// </summary>
    public static class DynamicHelpers
    {
        /// <summary>
        /// Na temelju odabrane klase koja implementira IDynamicQuery generiraju se elementi pomoću
        /// kojih se slaže SQL upit
        /// </summary>
        /// <param name="instance">IDynamicQuery instanca objekta koju extenda</param>
        /// <returns>DynamicQueryHelper objekt</returns>
        public static DynamicQueryHelper GetDynamicQueryData(this IDynamicQuery instance)
        {
            Type t = instance.GetType();

            PropertyInfo[] p = t.GetProperties();

            DynamicQueryHelper q = new DynamicQueryHelper();
            DynamicParameters dbArgs = new DynamicParameters();

            string s;
            object o;
            foreach (PropertyInfo pi in p)
            {
                SourceTableAttribute sta = pi.GetCustomAttribute<SourceTableAttribute>();

                q.atributes.Add(String.Format("[dbo].[{0}].[{1}]", sta.TableName, pi.Name));

                o = pi.GetValue(instance);
                s = String.Format("{0}", o).Trim();

                if (s != "" && s != "0" && s != DateTime.MinValue.ToString())
                {
                    switch (pi.PropertyType.ToString())
                    {
                        case "System.String":
                            q.parameters.Add(String.Format("[dbo].[{0}].[{1}] LIKE '%' + @{1} + '%'", sta.TableName, pi.Name));
                            break;
                        case "System.DateTime":
                            q.parameters.Add(String.Format("[dbo].[{0}].[{1}] LIKE '%' + @{1} + '%'", sta.TableName, pi.Name));
                            break;
                        default:
                            q.parameters.Add(String.Format("[dbo].[{0}].[{1}] = @{1}", sta.TableName, pi.Name));
                            break;
                    }

                    q.dbArgs.Add(pi.Name, o);
                }
            }

            return q;
        }
    }
}
