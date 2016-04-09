using BikeGround.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AttributesTester
{
    public static class HTMLHelper
    {
        private static T GetAttributeFrom<T>(this object instance, Type parentType, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = parentType.GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            return (T)property.GetCustomAttribute(attrType, false);
        }

        public static string GenerateHTMLTag_RequiredAttribute(object o, Type parentType, string propertyName)
        {
            var req = o.GetAttributeFrom<RequiredAttribute>(parentType, propertyName);

            if (req == null)
            {
                return String.Empty;
            }

            return String.Format("<p ng-show=\"{0}Form.{1}.$error.required && !{0}Form.{1}.$pristine\" class=\"text-danger\">{2}</p>",
                parentType.Name,
                propertyName,
                req.ErrorMessage);
        }

        // var req = tester.GetAttributeFrom<RequiredAttribute>(t, "Opis");
        // var maxLength = tester.GetAttributeFrom<StringLengthAttribute>(t, "Opis");
        // var name = tester.GetAttributeFrom<DataTypeAttribute>(t, "Opis");
        // var url = tester.GetAttributeFrom<TesterAttribute>(t, "Opis");

        public static string GenerateHTMLTag_StringLengthAttribute(object o, Type parentType, string propertyName)
        {
            var req = o.GetAttributeFrom<StringLengthAttribute>(parentType, propertyName);

            if (req == null)
            {
                return String.Empty;
            }

            //Formatiranje grešaka:

            //{0} - Name
            //{1} - Maximum Length
            //{2} - Minimum Length

            string error = String.Format(req.ErrorMessage, new object[] { propertyName, req.MinimumLength, req.MaximumLength });

            return String.Format("<p ng-show=\"{0}Form.{1}.$error.maxlength\" class=\"text-danger\">{2}</p>",
                parentType.Name,
                propertyName,
                error);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Profile profile = new Profile();

            Type t = profile.GetType();

            PropertyInfo[] p = t.GetProperties();

            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].Name != "FirstName")
                {
                    continue;
                }

                Console.WriteLine(String.Format("   {0} {1} {2} {3}",
                    p[i].Name,
                    p[i].Attributes,
                    p[i].DeclaringType,
                    p[i].CustomAttributes.Count()
                    ));

                // var req = tester.GetAttributeFrom<RequiredAttribute>(t, "Opis");
                // var maxLength = tester.GetAttributeFrom<StringLengthAttribute>(t, "Opis");
                // var name = tester.GetAttributeFrom<DataTypeAttribute>(t, "Opis");
                // var url = tester.GetAttributeFrom<TesterAttribute>(t, "Opis");

                Console.WriteLine(HTMLHelper.GenerateHTMLTag_RequiredAttribute(profile, t, p[i].Name));
                Console.WriteLine(HTMLHelper.GenerateHTMLTag_StringLengthAttribute(profile, t, p[i].Name));
            }

            //<p ng-show="ProfileForm.LastName.$error.minlength" class="text-danger">LastName is too short.</p>
            //<p ng-show="ProfileForm.LastName.$error.maxlength" class="text-danger">@BikeGround.Models.Resources.Profile.LastNameLong</p>

            var attr = t.GetCustomAttributes();

            Console.ReadLine();
        }

        [AttributeUsage(AttributeTargets.All)]
        public class TesterAttribute : System.Attribute
        {
            private string topic;
            public readonly string Url;

            public string Topic
            {
                get
                {
                    return topic;
                }
                set
                {
                    topic = value;
                }
            }

            public TesterAttribute(string url)
            {
                this.Url = url;
            }
        }
    }
}
