using BikeGround.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Web.Mvc;

namespace BikeGround.Web.Classes
{
    /// <summary>
    /// Helper klasa koja generira elemente sučelja
    /// </summary>
    public static class CustomHTMLHelper
    {
        /// <summary>
        /// Dohvati custom attribut
        /// </summary>
        /// <typeparam name="T">Type parametar</typeparam>
        /// <param name="instance">Extenzija</param>
        /// <param name="parentType">Parent type (promatrani objekt)</param>
        /// <param name="propertyName">Promatrani property objekta</param>
        /// <returns></returns>
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

        /// <summary>
        /// Generira textbox kontrolu
        /// </summary>
        /// <typeparam name="T">Tip promatranog objekta</typeparam>
        /// <typeparam name="U">Tip propertya</typeparam>
        /// <param name="htmlHelper">Ekstenzija ThmlHelpera</param>
        /// <param name="expr">Lambda izraz</param>
        /// <param name="t">Tip promatranog objekta</param>
        /// <returns>MvcHtmlString string</returns>
        /// <returns></returns>
        public static MvcHtmlString Editor<T, U>(this HtmlHelper htmlHelper, Expression<Func<T, U>> expr, Type t) where T : class
        {
            var e = expr.Body as MemberExpression;

            PropertyInfo i = t.GetProperty(e.Member.Name);
            StringLengthAttribute req = i.GetCustomAttribute<StringLengthAttribute>();
            DataTypeAttribute dat = i.GetCustomAttribute<DataTypeAttribute>();
            TextEditorAttribute tex = i.GetCustomAttribute<TextEditorAttribute>();

            if (req == null)
            {
                return new MvcHtmlString(String.Empty);
            }

            string required = i.GetCustomAttribute<RequiredAttribute>() == null ? "" : "required";
            string password = i.GetCustomAttribute<PasswordAttribute>() == null ? "type=\"text\"" : "type=\"password\"";
            string minLenght = req.MinimumLength == 0 ? "" : String.Format("ng-minlength=\"{0}\"", req.MinimumLength);
            string maxLenght = req.MaximumLength == 0 ? "" : String.Format("ng-maxlength=\"{0}\"", req.MaximumLength);

            if (tex != null)
            {
                return new MvcHtmlString(String.Format("<div id=\"{1}\" text-angular ng-model=\"{0}.{1}\" name=\"{1}\" ta-text-editor-class=\"border-around container\" ta-html-editor-class=\"border-around\" {2} {3}></div>",
                    t.Name,
                    e.Member.Name,
                    required,
                    String.Format("{0} {1} {2}", new string[] { required, minLenght, maxLenght })
                ));
            }
            else if (dat != null && dat.DataType == DataType.MultilineText)
            {
                return new MvcHtmlString(String.Format("<textarea class=\"form-control multi-line\" id=\"{0}\" name=\"{0}\" ng-model=\"{3}.{0}\" {1} {2}></textarea>",
                    e.Member.Name,
                    required,
                    String.Format("{0} {1} {2}", new string[] { required, minLenght, maxLenght }),
                    t.Name
                    ));
            }
            else
            {
                return new MvcHtmlString(String.Format("<input id=\"{0}\" {4} name=\"{0}\" class=\"form-control\" ng-model=\"{3}.{0}\" {1} {2} />",
                    e.Member.Name,
                    required,
                    String.Format("{0} {1} {2}", new string[] { required, minLenght, maxLenght }),
                    t.Name,
                    password
                    ));
            }
        }

        public static MvcHtmlString HiddenEditor<T, U>(this HtmlHelper htmlHelper, Expression<Func<T, U>> expr, Type t, string alias = "") where T : class
        {
            var e = expr.Body as MemberExpression;

            PropertyInfo i = t.GetProperty(e.Member.Name);
            DataTypeAttribute dat = i.GetCustomAttribute<DataTypeAttribute>();

            string required = i.GetCustomAttribute<RequiredAttribute>() == null ? "" : "required";

            if (String.IsNullOrEmpty(alias))
            {

                return new MvcHtmlString(String.Format("<input id=\"{0}\" type=\"hidden\" name=\"{0}\" class=\"form-control\" ng-model=\"{2}.{0}\" ng-value=\"{2}.{0}\" {1} />",
                    e.Member.Name,
                    required,
                    t.Name
                    ));
            }
            else
            {
                return new MvcHtmlString(String.Format("<input id=\"{0}\" type=\"hidden\" name=\"{0}\" class=\"form-control\" ng-model=\"{2}.{0}\" value=\"{{{{ {3}.{0} }}}}\"  {1} />",
                    e.Member.Name,
                    required,
                    t.Name,
                    alias
                    ));
            }
        }

        public static MvcHtmlString NumericEditor<T, U>(this HtmlHelper htmlHelper, Expression<Func<T, U>> expr, Type t) where T : class
        {
            var e = expr.Body as MemberExpression;

            PropertyInfo i = t.GetProperty(e.Member.Name);
            DataTypeAttribute dat = i.GetCustomAttribute<DataTypeAttribute>();

            string required = i.GetCustomAttribute<RequiredAttribute>() == null ? "" : "required";

            return new MvcHtmlString(String.Format("<input id=\"{0}\" type=\"number\" name=\"{0}\" class=\"form-control\" ng-model=\"{2}.{0}\" {1} />",
                e.Member.Name,
                required,
                t.Name
                ));
        }

        /// <summary>
        /// Generira editor za vanjske ključeve (drow down list)
        /// </summary>
        /// <typeparam name="T">Tip promatranog objekta</typeparam>
        /// <typeparam name="U">Tip propertya</typeparam>
        /// <param name="htmlHelper">Ekstenzija ThmlHelpera</param>
        /// <param name="expr">Lambda izraz</param>
        /// <param name="t">Tip promatranog objekta</param>
        /// <param name="ngExpression">Angular.js ng-options izraz</param>
        /// <returns>MvcHtmlString string</returns>
        public static MvcHtmlString EditorDropDown<T, U>(this HtmlHelper htmlHelper, Expression<Func<T, U>> expr, Type t, string ngExpression) where T : class
        {
            var e = expr.Body as MemberExpression;

            PropertyInfo i = t.GetProperty(e.Member.Name);

            string required = i.GetCustomAttribute<RequiredAttribute>() == null ? "" : "required";

            return new MvcHtmlString(String.Format("<select id=\"{0}\" type=\"text\" name=\"{0}\" class=\"form-control\" ng-model=\"{2}.{0}\" {1} ng-options=\"{3}\"><option value=\"\">{4}</option></select>",
                e.Member.Name,
                required,
                t.Name,
                ngExpression,
                BikeGround.Models.Resources.Global.DefaultSelect
                ));
        }

        public static MvcHtmlString CheckEditor<T, U>(this HtmlHelper htmlHelper, Expression<Func<T, U>> expr, Type t) where T : class
        {
            var e = expr.Body as MemberExpression;

            PropertyInfo i = t.GetProperty(e.Member.Name);

            string required = i.GetCustomAttribute<RequiredAttribute>() == null ? "" : "required";

            return new MvcHtmlString(String.Format("<input id=\"{0}\" type=\"checkbox\" name=\"{0}\" ng-model=\"{1}.{0}\" />",
                e.Member.Name,
                t.Name
                ));
        }

        /// <summary>
        /// Editor za datumska polja
        /// </summary>
        /// <typeparam name="T">Tip promatranog objekta</typeparam>
        /// <typeparam name="U">Tip propertya</typeparam>
        /// <param name="htmlHelper">Ekstenzija ThmlHelpera</param>
        /// <param name="expr">Lambda izraz</param>
        /// <param name="t">Tip promatranog objekta</param>
        /// <returns>MvcHtmlString string</returns>
        public static MvcHtmlString DateEditor<T, U>(this HtmlHelper htmlHelper, Expression<Func<T, U>> expr, Type t) where T : class
        {
            var e = expr.Body as MemberExpression;

            PropertyInfo i = t.GetProperty(e.Member.Name);
            StringLengthAttribute req = i.GetCustomAttribute<StringLengthAttribute>();

            string required = i.GetCustomAttribute<RequiredAttribute>() == null ? "" : "required";

            
            // return new MvcHtmlString(String.Format("<input id=\"{0}\" type=\"text\" name=\"{0}\" class=\"form-control\" ng-model=\"{2}.{0}\" {1} placeholder=\"{3}\"  dateFormat=\"{3}\" datedirective />",
            return new MvcHtmlString(String.Format("<input id=\"{0}\" type=\"text\" name=\"{0}\" class=\"form-control\" ng-model=\"{2}.{0}\" {1} placeholder=\"{3}\" is-open=\"{2}{0}o\" ng-click=\"{2}{0}o = true\" datepicker-options=\"dateOptions\" {1} close-text=\"Close\" datepicker-popup=\"{3}\" />",
                e.Member.Name,
                required,
                t.Name,
                BikeGround.Models.Resources.Global.DateFormat
                ));
        }

        /// <summary>
        /// Generira validacijski blok za required attribute
        /// </summary>
        /// <typeparam name="T">Tip promatranog objekta</typeparam>
        /// <typeparam name="U">Tip propertya</typeparam>
        /// <param name="htmlHelper">Ekstenzija ThmlHelpera</param>
        /// <param name="expr">Lambda izraz</param>
        /// <param name="t">Tip promatranog objekta</param>
        /// <returns>MvcHtmlString string</returns>
        public static MvcHtmlString Required<T, U>(this HtmlHelper htmlHelper, Expression<Func<T, U>> expr, Type t) where T : class
        {
            var e = expr.Body as MemberExpression;

            PropertyInfo i = t.GetProperty(e.Member.Name);
            RequiredAttribute req = i.GetCustomAttribute<RequiredAttribute>();
            SourceTableAttribute sta = i.GetCustomAttribute<SourceTableAttribute>();

            if (req == null)
            {
                return new MvcHtmlString(String.Empty);
            }

            ResourceManager r = new ResourceManager("BikeGround.Models.Resources." + (sta == null ? t.Name : sta.TableName), t.Assembly);

            string required;

            try
            {
                required = r.GetString(e.Member.Name + "Required");
            }
            catch
            {
                throw new Exception("Inavalid resource key [Missing resource data]");;
            }

            return new MvcHtmlString(String.Format("<span ng-show=\"{0}Form.{1}.$error.required && (!{0}Form.{1}.$pristine || submitted)\" class=\"text-danger\">{2}</span>",
                t.Name,
                e.Member.Name,
                required));
        }

        /// <summary>
        /// Generiraj validacijski blok za StringLenghtAttribute određenog propertija ({0} - Name, {1} - Maximum Length, {2} - Minimum Length)
        /// </summary>
        /// <typeparam name="T">Tip promatranog objekta</typeparam>
        /// <typeparam name="U">Tip propertya</typeparam>
        /// <param name="htmlHelper">Ekstenzija ThmlHelpera</param>
        /// <param name="expr">Lambda izraz</param>
        /// <param name="t">Tip promatranog objekta</param>
        /// <returns>MvcHtmlString string</returns>
        public static MvcHtmlString StringLength<T, U>(this HtmlHelper htmlHelper, Expression<Func<T, U>> expr, Type t) where T : class
        {
            var e = expr.Body as MemberExpression;

            PropertyInfo i = t.GetProperty(e.Member.Name);
            StringLengthAttribute req = i.GetCustomAttribute<StringLengthAttribute>();
            SourceTableAttribute sta = i.GetCustomAttribute<SourceTableAttribute>();

            if (req == null)
            {
                return new MvcHtmlString(String.Empty);
            }

            ResourceManager r = new ResourceManager("BikeGround.Models.Resources." + (sta == null ? t.Name : sta.TableName), t.Assembly);

            string lenght;

            try
            {
                lenght = r.GetString(e.Member.Name + "Long");
            }
            catch
            {
                throw new Exception("Inavalid resource key [Missing resource data]");
            }

            string error = String.Format(lenght, new object[] { t.Name, req.MaximumLength, req.MinimumLength });

            return new MvcHtmlString(String.Format("<span ng-show=\"{0}Form.{1}.$error.maxlength\" class=\"text-danger\">{2}</span>",
                t.Name,
                e.Member.Name,
                error));
        }

        /// <summary>
        /// Generiraj submit button za klasičnu formu
        /// </summary>
        /// <param name="htmlHelper">Ekstenzija ThmlHelpera</param>
        /// <param name="label">Label za gumb</param>
        /// <returns>MvcHtmlString string</returns>
        public static MvcHtmlString Submit(this HtmlHelper htmlHelper, string label)
        {
            //var e = expr.Body as MemberExpression;

            //PropertyInfo i = t.GetProperty(e.Member.Name);
            //ResourceManager r = new ResourceManager("BikeGround.Models.Resources." + t.Name, t.Assembly);
            return new MvcHtmlString(String.Format("<button type=\"submit\" class=\"btn btn-primary\" ng-click=\"submitted=true\">{0}</button>",
                label));
        }
    }
}