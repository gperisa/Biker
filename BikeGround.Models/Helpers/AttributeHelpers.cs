using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeGround.Models
{
    /// <summary>
    /// Atribut koji označava da se promatrani property prikazuje unutar WYSIWYG editora
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class TextEditorAttribute : Attribute
    {

    }

    /// <summary>
    /// Atribut koji označava da se promatrani property prikazuje kao password field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : Attribute
    {

    }

    /// <summary>
    /// Atribut koji označava iz kojeg tablice selektiramo podatke. Koristi se kada preko dapera izvodimo SQL 
    /// upite u kojima se nalaze JOIN klauzule i svaki atribut dolazi iz neke tablice.
    /// Koristi se i za resurse gdje označava resx file iz kojeg se čita resurs, overrida temeljne postavke (gleda objekt po nazivu klase)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SourceTableAttribute : Attribute
    {
        public string TableName { get; set; }
    }
}
