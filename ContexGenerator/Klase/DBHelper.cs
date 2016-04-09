﻿using System;

namespace ContexGenerator
{
    public static class DBHelper
    {
        /// <summary>
        /// Dohvaća SQL tip podataka ekvivalentan .NET tipu podataka
        /// </summary>
        /// <param name="tip">.NET tip podataka</param>
        public static string SqlTipPodataka(string tip)
        {
            switch (tip)
            {
                // Summary:
                //     System.Int64. A 64-bit signed integer.
                case "bigint": tip = "SqlDbType.BigInt"; break;
                //
                // Summary:
                //     System.Array of type System.Byte. A fixed-length stream of binary data ranging
                //     between 1 and 8,000 bytes.
                case "binary": tip = "SqlDbType.Binary"; break;
                //
                // Summary:
                //     System.Boolean. An unsigned numeric value that can be 0, 1, or null.
                case "bit": tip = "SqlDbType.Bit"; break;
                //
                // Summary:
                //     System.String. A fixed-length stream of non-Unicode characters ranging between
                //     1 and 8,000 characters.
                case "char": tip = "SqlDbType.Char"; break;
                //
                // Summary:
                //     System.DateTime. Date and time data ranging in value from January 1, 1753
                //     to December 31, 9999 to an accuracy of 3.33 milliseconds.
                case "datetime": tip = "SqlDbType.DateTime"; break;
                //
                // Summary:
                //     System.Decimal. A fixed precision and scale numeric value between -10 38
                //     -1 and 10 38 -1.
                case "decimal": tip = "SqlDbType.Decimal"; break;
                //
                // Summary:
                //     System.Double. A floating point number within the range of -1.79E +308 through
                //     1.79E +308.
                case "float": tip = "SqlDbType.Float"; break;
                //
                // Summary:
                //     System.Array of type System.Byte. A variable-length stream of binary data
                //     ranging from 0 to 2 31 -1 (or 2,147,483,647) bytes.
                case "image": tip = "SqlDbType.Image"; break;
                //
                // Summary:
                //     System.Int32. A 32-bit signed integer.
                case "int": tip = "SqlDbType.Int"; break;
                //
                // Summary:
                //     System.Decimal. A currency value ranging from -2 63 (or -9,223,372,036,854,775,808)
                //     to 2 63 -1 (or +9,223,372,036,854,775,807) with an accuracy to a ten-thousandth
                //     of a currency unit.
                case "money": tip = "SqlDbType.Money"; break;
                //
                // Summary:
                //     System.String. A fixed-length stream of Unicode characters ranging between
                //     1 and 4,000 characters.
                case "nchar": tip = "SqlDbType.NChar"; break;
                //
                // Summary:
                //     System.String. A variable-length stream of Unicode data with a maximum length
                //     of 2 30 - 1 (or 1,073,741,823) characters.
                case "ntext": tip = "SqlDbType.NText"; break;
                //
                // Summary:
                //     System.String. A variable-length stream of Unicode characters ranging between
                //     1 and 4,000 characters. Implicit conversion fails if the string is greater
                //     than 4,000 characters. Explicitly set the object when working with strings
                //     longer than 4,000 characters. Use System.Data.SqlDbType.NVarChar when the
                //     database column is nvarchar(max).
                case "nvarchar": tip = "SqlDbType.NVarChar"; break;
                //
                // Summary:
                //     System.Single. A floating point number within the range of -3.40E +38 through
                //     3.40E +38.
                case "real": tip = "SqlDbType.Real"; break;
                //
                // Summary:
                //     System.Guid. A globally unique identifier (or GUID).
                case "uniqueidentifier": tip = "SqlDbType.UniqueIdentifier"; break;
                //
                // Summary:
                //     System.DateTime. Date and time data ranging in value from January 1, 1900
                //     to June 6, 2079 to an accuracy of one minute.
                case "smalldatetime": tip = "SqlDbType.SmallDateTime"; break;
                //
                // Summary:
                //     System.Int16. A 16-bit signed integer.
                case "smallint": tip = "SqlDbType.SmallInt"; break;
                //
                // Summary:
                //     System.Decimal. A currency value ranging from -214,748.3648 to +214,748.3647
                //     with an accuracy to a ten-thousandth of a currency unit.
                case "smallmoney": tip = "SqlDbType.SmallMoney"; break;
                //
                // Summary:
                //     System.String. A variable-length stream of non-Unicode data with a maximum
                //     length of 2 31 -1 (or 2,147,483,647) characters.
                case "text": tip = "SqlDbType.Text"; break;
                //
                // Summary:
                //     System.Array of type System.Byte. Automatically generated binary numbers,
                //     which are guaranteed to be unique within a database. timestamp is used typically
                //     as a mechanism for version-stamping table rows. The storage size is 8 bytes.
                case "timestamp": tip = "SqlDbType.Timestamp"; break;
                //
                // Summary:
                //     System.Byte. An 8-bit unsigned integer.
                case "tinyint": tip = "SqlDbType.TinyInt"; break;
                //
                // Summary:
                //     System.Array of type System.Byte. A variable-length stream of binary data
                //     ranging between 1 and 8,000 bytes. Implicit conversion fails if the byte
                //     array is greater than 8,000 bytes. Explicitly set the object when working
                //     with byte arrays larger than 8,000 bytes.
                case "varbinary": tip = "SqlDbType.VarBinary"; break;
                //
                // Summary:
                //     System.String. A variable-length stream of non-Unicode characters ranging
                //     between 1 and 8,000 characters. Use System.Data.SqlDbType.VarChar when the
                //     database column is varchar(max).
                case "varchar": tip = "SqlDbType.VarChar"; break;
                //
                // Summary:
                //     System.Object. A special data type that can contain numeric, string, binary,
                //     or date data as well as the SQL Server values Empty and Null, which is assumed
                //     if no other type is declared.
                case "variant": tip = "SqlDbType.Variant"; break;
                //
                // Summary:
                //     An XML value. Obtain the XML as a string using the System.Data.SqlClient.SqlDataReader.GetValue(System.Int32)
                //     method or System.Data.SqlTypes.SqlXml.Value property, or as an System.Xml.XmlReader
                //     by calling the System.Data.SqlTypes.SqlXml.CreateReader() method.
                case "xml": tip = "SqlDbType.Xml"; break;
                //
                // Summary:
                //     A SQL Server 2005 user-defined type (UDT).
                case "udt": tip = "SqlDbType.Udt"; break;
                //
                // Summary:
                //     A special data type for specifying structured data contained in table-valued
                //     parameters.
                case "structured": tip = "SqlDbType.Structured"; break;
                //
                // Summary:
                //     Date data ranging in value from January 1,1 AD through December 31, 9999
                //     AD.
                case "date": tip = "SqlDbType.Date"; break;
                //
                // Summary:
                //     Time data based on a 24-hour clock. Time value range is 00:00:00 through
                //     23:59:59.9999999 with an accuracy of 100 nanoseconds. Corresponds to a SQL
                //     Server time value.
                case "time": tip = "SqlDbType.Time"; break;
                //
                // Summary:
                //     Date and time data. Date value range is from January 1,1 AD through December
                //     31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an
                //     accuracy of 100 nanoseconds.
                case "datetime2": tip = "SqlDbType.DateTime2"; break;
                //
                // Summary:
                //     Date and time data with time zone awareness. Date value range is from January
                //     1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through
                //     23:59:59.9999999 with an accuracy of 100 nanoseconds. Time zone value range
                //     is -14:00 through +14:00.
                case "datetimeoffset": tip = "DateTimeOffset"; break;
            }

            return tip;
        }

        /// <summary>
        /// Dohvaća .NET ekvivalent SQL tipu podataka
        /// </summary>
        /// <param name="tip">Sql tip podataka</param>
        public static string TipPodataka(string tip)
        {
            switch (tip)
            {
                // Summary:
                //     System.Int64. A 64-bit signed integer.
                case "bigint": tip = "long"; break;
                //
                // Summary:
                //     System.Array of type System.Byte. A fixed-length stream of binary data ranging
                //     between 1 and 8,000 bytes.
                case "binary": tip = "SqlDbType.Binary"; break;
                //
                // Summary:
                //     System.Boolean. An unsigned numeric value that can be 0, 1, or null.
                case "bit": tip = "bool"; break;
                //
                // Summary:
                //     System.String. A fixed-length stream of non-Unicode characters ranging between
                //     1 and 8,000 characters.
                case "char": tip = "string"; break;
                //
                // Summary:
                //     System.DateTime. Date and time data ranging in value from January 1, 1753
                //     to December 31, 9999 to an accuracy of 3.33 milliseconds.
                case "datetime": tip = "DateTime"; break;
                //
                // Summary:
                //     System.Decimal. A fixed precision and scale numeric value between -10 38
                //     -1 and 10 38 -1.
                case "decimal": tip = "decimal"; break;
                //
                // Summary:
                //     System.Double. A floating point number within the range of -1.79E +308 through
                //     1.79E +308.
                case "float": tip = "float"; break;
                //
                // Summary:
                //     System.Array of type System.Byte. A variable-length stream of binary data
                //     ranging from 0 to 2 31 -1 (or 2,147,483,647) bytes.
                case "image": tip = "Image"; break;
                //
                // Summary:
                //     System.Int32. A 32-bit signed integer.
                case "int": tip = "int"; break;
                //
                // Summary:
                //     System.Decimal. A currency value ranging from -2 63 (or -9,223,372,036,854,775,808)
                //     to 2 63 -1 (or +9,223,372,036,854,775,807) with an accuracy to a ten-thousandth
                //     of a currency unit.
                case "money": tip = "Money"; break;
                //
                // Summary:
                //     System.String. A fixed-length stream of Unicode characters ranging between
                //     1 and 4,000 characters.
                case "nchar": tip = "string"; break;
                //
                // Summary:
                //     System.String. A variable-length stream of Unicode data with a maximum length
                //     of 2 30 - 1 (or 1,073,741,823) characters.
                case "ntext": tip = "string"; break;
                //
                // Summary:
                //     System.String. A variable-length stream of Unicode characters ranging between
                //     1 and 4,000 characters. Implicit conversion fails if the string is greater
                //     than 4,000 characters. Explicitly set the object when working with strings
                //     longer than 4,000 characters. Use System.Data.SqlDbType.NVarChar when the
                //     database column is nvarchar(max).
                case "nvarchar": tip = "string"; break;
                //
                // Summary:
                //     System.Single. A floating point number within the range of -3.40E +38 through
                //     3.40E +38.
                case "real": tip = "SqlDbType.Real"; break;
                //
                // Summary:
                //     System.Guid. A globally unique identifier (or GUID).
                case "uniqueidentifier": tip = "GUID"; break;
                //
                // Summary:
                //     System.DateTime. Date and time data ranging in value from January 1, 1900
                //     to June 6, 2079 to an accuracy of one minute.
                case "smalldatetime": tip = "SmallDateTime"; break;
                //
                // Summary:
                //     System.Int16. A 16-bit signed integer.
                case "smallint": tip = "short"; break;
                //
                // Summary:
                //     System.Decimal. A currency value ranging from -214,748.3648 to +214,748.3647
                //     with an accuracy to a ten-thousandth of a currency unit.
                case "smallmoney": tip = "SmallMoney"; break;
                //
                // Summary:
                //     System.String. A variable-length stream of non-Unicode data with a maximum
                //     length of 2 31 -1 (or 2,147,483,647) characters.
                case "text": tip = "string"; break;
                //
                // Summary:
                //     System.Array of type System.Byte. Automatically generated binary numbers,
                //     which are guaranteed to be unique within a database. timestamp is used typically
                //     as a mechanism for version-stamping table rows. The storage size is 8 bytes.
                case "timestamp": tip = "Timestamp"; break;
                //
                // Summary:
                //     System.Byte. An 8-bit unsigned integer.
                case "tinyint": tip = "TinyInt"; break;
                //
                // Summary:
                //     System.Array of type System.Byte. A variable-length stream of binary data
                //     ranging between 1 and 8,000 bytes. Implicit conversion fails if the byte
                //     array is greater than 8,000 bytes. Explicitly set the object when working
                //     with byte arrays larger than 8,000 bytes.
                case "varbinary": tip = "VarBinary"; break;
                //
                // Summary:
                //     System.String. A variable-length stream of non-Unicode characters ranging
                //     between 1 and 8,000 characters. Use System.Data.SqlDbType.VarChar when the
                //     database column is varchar(max).
                case "varchar": tip = "string"; break;
                //
                // Summary:
                //     System.Object. A special data type that can contain numeric, string, binary,
                //     or date data as well as the SQL Server values Empty and Null, which is assumed
                //     if no other type is declared.
                case "variant": tip = "Variant"; break;
                //
                // Summary:
                //     An XML value. Obtain the XML as a string using the System.Data.SqlClient.SqlDataReader.GetValue(System.Int32)
                //     method or System.Data.SqlTypes.SqlXml.Value property, or as an System.Xml.XmlReader
                //     by calling the System.Data.SqlTypes.SqlXml.CreateReader() method.
                case "xml": tip = "Xml"; break;
                //
                // Summary:
                //     A SQL Server 2005 user-defined type (UDT).
                case "udt": tip = "Udt"; break;
                //
                // Summary:
                //     A special data type for specifying structured data contained in table-valued
                //     parameters.
                case "structured": tip = "Structured"; break;
                //
                // Summary:
                //     Date data ranging in value from January 1,1 AD through December 31, 9999
                //     AD.
                case "date": tip = "Date"; break;
                //
                // Summary:
                //     Time data based on a 24-hour clock. Time value range is 00:00:00 through
                //     23:59:59.9999999 with an accuracy of 100 nanoseconds. Corresponds to a SQL
                //     Server time value.
                case "time": tip = "Time"; break;
                //
                // Summary:
                //     Date and time data. Date value range is from January 1,1 AD through December
                //     31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an
                //     accuracy of 100 nanoseconds.
                case "datetime2": tip = "DateTime"; break;
                //
                // Summary:
                //     Date and time data with time zone awareness. Date value range is from January
                //     1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through
                //     23:59:59.9999999 with an accuracy of 100 nanoseconds. Time zone value range
                //     is -14:00 through +14:00.
                case "datetimeoffset": tip = "DateTimeOffset"; break;
            }

            return tip;
        }
    }
}
