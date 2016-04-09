using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace BikeGround.API.Logger
{
    public class Tracing
    {
        /// <summary>
        /// Small tracing method
        /// </summary>
        /// <param name="message">message to write</param>
        /// 
        /// Using the #if, the evaluation of the conditional will be performed with regard to the library's compilation settings.
        /// Using the Conditional("DEBUG") attribute, the evaluation of the conditional will be performed with regard to the compilation settings of the invoker.
        //   [System.Diagnostics.Conditional("DEBUG")]
        public static void Trace(string message)
        {
            using (StreamWriter tracer = new StreamWriter(ConfigurationManager.AppSettings["tracePath"].ToString(), true))
            {
                tracer.WriteLine(message);
            }
        }
    }
}