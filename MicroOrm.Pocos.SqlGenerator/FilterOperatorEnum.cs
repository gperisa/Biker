using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MicroOrm.Pocos.SqlGenerator
{
    public enum FilterOperatorEnum
    {
       [Description("0")]
       AND=1,
       OR=2
    }

    /// <summary>
    ///     Extension method for getting range enums
    /// </summary>
    public static class AttributesHelperExtension
    {
        public static string ToDescription(this Enum value)
        {
            var da =
                (DescriptionAttribute[])
                    (value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute),
                        false);
            return da.Length > 0 ? da[0].Description : value.ToString();
        }
    }
}
