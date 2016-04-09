using System;

namespace ContexGenerator
{
    public static class Predlosci
    {
        /// <summary>
        /// Predložak za klase
        /// </summary>
        public static string ClassTemplate
        {
            get
            {
                return "using MicroOrm.Pocos.SqlGenerator.Attributes;" + Environment.NewLine +
                        "using Newtonsoft.Json;" + Environment.NewLine +
                        "using System.Collections.Generic;" + Environment.NewLine +
                        "using System.ComponentModel.DataAnnotations;" + Environment.NewLine +
                        "using System;" + Environment.NewLine +
                        "" + Environment.NewLine +
                        "namespace BikeGround.Models" + Environment.NewLine +
                        "{{" + Environment.NewLine +
                        "    public partial class {0}" + Environment.NewLine +
                        "    {{" + Environment.NewLine +
                        "{1}" + Environment.NewLine +
                        "    }}" + Environment.NewLine +
                        "}}";
            }
        }

        /// <summary>
        /// Predložak za procedure
        /// </summary>
        public static string ProcedureTemplate
        {
            get
            {
                return "Ne sada";
            }
        }

        /// <summary>
        /// Predložak za angular js modul
        /// </summary>
        public static string AngularJSModul
        {
            get
            {
                return "\n(function () {{" +
                        "\n" +
                        "\n    var {0}Controller = function ($scope, ObjectFactory, Notification) {{" +
                        "\n" +
                        "\n        $scope.{0} = {{}};" +
                        "\n" +
                        "\n        function init() {{" +
                        "\n" +
                        "\n            $scope.{0} = ObjectFactory.getObject({0}ID, '{0}')" +
                        "\n            .success(function ({0}Data) {{" +
                        "\n                $scope.{0} = {0}Data;" +
                        "\n            }})" +
                        "\n            .error(function (data, status, headers, config) {{" +
                        "\n                Notification.error({{ message: 'Error: ' + status, delay: 2000 }});" +
                        "\n            }});" +
                        "\n        }}" +
                        "\n" +
                        "\n        init();" +
                        "\n" +
                        "\n        $scope.{0}FormValidate = function (isValid, {0}) {{" +
                        "\n" +
                        "\n            if (isValid) {{" +
                        "\n                if ({0}.ID === 0 || {0}.ID === undefined) {{" +
                        "\n                    ObjectFactory.postObject({0}, '{0}')" +
                        "\n                    .success(function (ID) {{" +
                        "\n" +
                        "\n                        $scope.{0}.ID = ID;" +
                        "\n" +
                        "\n                        Notification.success({{ message: 'Ok!', delay: 2000 }});" +
                        "\n                    }})" +
                        "\n                    .error(function (data, status, headers, config) {{" +
                        "\n                        Notification.error({{ message: 'Error: ' + status, delay: 2000 }});" +
                        "\n                    }});" +
                        "\n                }}" +
                        "\n                else if ({0}.ID > 0) {{" +
                        "\n                    ObjectFactory.putObject({0}, '{0}')" +
                        "\n                    .success(function () {{" +
                        "\n                        Notification.success({{ message: 'Ok!', delay: 2000 }});" +
                        "\n                    }})" +
                        "\n                    .error(function (data, status, headers, config) {{" +
                        "\n                        Notification.error({{ message: 'Error: ' + status, delay: 2000 }});" +
                        "\n                    }});" +
                        "\n                }}" +
                        "\n            }}" +
                        "\n        }};" +
                        "\n    }};" +
                        "\n" +
                        "\n    {0}Controller.$inject = ['$scope', 'ObjectFactory', 'Notification'];" +
                        "\n" +
                        "\n    angular.module('{0}Module').controller('{0}Controller', {0}Controller);" +
                        "\n" +
                        "\n}}());";
            }
        }

        /// <summary>
        /// Predložak za angular js controller
        /// </summary>
        public static string AngularJSFactory
        {
            get
            {
                return "\n(function () {{" +
                        "\n" +
                        "\n    var {0}Factory = function($http) {{" +
                        "\n" +
                        "\n        var factory = {{}};" +
                        "\n" +
                        "\n        factory.get{0} = function() {{" +
                        "\n            return $http.get('http://localhost:3668/api/{0}');" +
                        "\n        }}" +
                        "\n" +
                        "\n        return factory;" +
                        "\n    }};" +
                        "\n" +
                        "\n    {0}Factory.$inject = ['$http'];" +
                        "\n" +
                        "\n    angular.module('{0}Module').factory('{0}Factory', {0}Factory);" +
                        "\n" +
                        "\n}}());";
            }
        }

        /// <summary>
        /// Predložak za html
        /// </summary>
        public static string HTMLTemplate
        {
            get
            {
                return "@model BikeGround.Models.{0}" +
                        "\n@{{" +
                        "\n    ViewBag.Title = \"Index\";" +
                        "\n" +
                        "\n    Type t = typeof({0});" +
                        "\n}}" +
                        "\n" +
                        "\n<div ng-controller=\"{0}Controller\" class=\"panel panel-default\">" +
                        "\n    <div class=\"panel-heading\">" +
                        "\n        <i class=\"fa fa-user\"></i> @BikeGround.Models.Resources.Global.{0}" +
                        "\n            <div class=\"pull-right\">" +
                        "\n                <button type=\"button\" class=\"btn btn-default btn-xs\">" +
                        "\n                    <i class=\"fa fa-refresh\" ng-click=\"init()\"></i>" +
                        "\n                </button>" +
                        "\n            </div>" +
                        "\n    </div>" +
                        "\n    <div class=\"panel-body\">" +
                        "\n        <form id=\"{0}Form\" name=\"{0}Form\" ng-submit=\"{0}FormValidate({0}Form.$valid)\" novalidate>" +
                        "\n            <div class=\"form-horizontal\">" +
                        "\n                <hr />" +
                        "\n                {1}" +
                        "\n" +
                        "\n                <div class=\"form-group\">" +
                        "\n                    <div class=\"col-md-offset-2 col-md-10\">" +
                        "\n                        <button type=\"submit\" class=\"btn btn-primary\">Submit</button>" +
                        "\n                        <input type=\"submit\" value=\"Create\" class=\"btn btn-default\" />" +
                        "\n                    </div>" +
                        "\n                </div>" +
                        "\n" +
                        "\n                <ul>" +
                        "\n                    <li ng-repeat=\"(key, errors) in {0}Form.$error track by $index\">" +
                        "\n                        <strong>XXX key YYY</strong> errors" +
                        "\n                        <ul>" +
                        "\n                            <li ng-repeat=\"e in errors\">XXX e.$name YYY has an error: <strong>XXX key YYY</strong>.</li>" +
                        "\n                        </ul>" +
                        "\n                    </li>" +
                        "\n                </ul>" +
                        "\n            </div>" +
                        "\n        </form>" +
                        "\n    </div>" +
                        "\n</div>";
            }
        }

        /// <summary>
        /// Predložak za pojedinačnu kontrolu
        /// </summary>
        public static string HTMLSingleTemplate
        {
            get
            {
                return "\n            <div class=\"form-group\">" +
                       "\n                @Html.LabelFor(model => model.{1}, htmlAttributes: new {{ @class = \"control-label col-md-2\" }})" +
                       "\n                <div class=\"col-md-10\" ng-class=\"{{ 'has-error' : {0}Form.{1}.$invalid && !{0}Form.{1}.$pristine }}\">" +
                       "\n                    @(Html.Editor<{0}, {2}>(x => x.{1}, t))" +
                        "\n                </div>" +
                       "\n            </div>";
            }
        }

        public static string HTMLSingleTemplateRequired
        {
            get
            {
                return "\n            <div class=\"form-group\">" +
                       "\n                @Html.LabelFor(model => model.{1}, htmlAttributes: new {{ @class = \"control-label col-md-2\" }})" +
                       "\n                <div class=\"col-md-10\" ng-class=\"{{ 'has-error' : {0}Form.{1}.$invalid && !{0}Form.{1}.$pristine }}\">" +
                       "\n                    @(Html.Editor<{0}, {2}>(x => x.{1}, t))" +
                       "\n                    @(Html.Required<{0}, {2}>(x => x.{1}, t))" +
                       "\n                </div>" +
                       "\n            </div>";
            }
        }

        public static string HTMLSingleTemplateRequiredLenght
        {
            get
            {
                return "\n            <div class=\"form-group\">" +
                       "\n                @Html.LabelFor(model => model.{1}, htmlAttributes: new {{ @class = \"control-label col-md-2\" }})" +
                       "\n                <div class=\"col-md-10\" ng-class=\"{{ 'has-error' : {0}Form.{1}.$invalid && !{0}Form.{1}.$pristine }}\">" +
                       "\n                    @(Html.Editor<{0}, {2}>(x => x.{1}, t))" +
                       "\n                    @(Html.Required<{0}, {2}>(x => x.{1}, t))" +
                       "\n                    @(Html.StringLength<{0}, {2}>(x => x.{1}, t))" +
                       "\n                </div>" +
                       "\n            </div>";
            }
        }

        public static string HTMLSingleTemplateLenght
        {
            get
            {
                return "\n            <div class=\"form-group\">" +
                       "\n                @Html.LabelFor(model => model.{1}, htmlAttributes: new {{ @class = \"control-label col-md-2\" }})" +
                       "\n                <div class=\"col-md-10\" ng-class=\"{{ 'has-error' : {0}Form.{1}.$invalid && !{0}Form.{1}.$pristine }}\">" +
                       "\n                    @(Html.Editor<{0}, {2}>(x => x.{1}, t))" +
                       "\n                    @(Html.StringLength<{0}, {2}>(x => x.{1}, t))" +
                       "\n                </div>" +
                       "\n            </div>";
            }
        }

        public static string HTMLHiddenField
        {
            get
            {
                return "@Html.HiddenFor(x => x.{0})";
            }
        }

        public static string HTMLTextBox
        {
            get
            {
                return "\n        <div class=\"form-group\">" +
                        "\n            @Html.LabelFor(model => model.{0}, htmlAttributes: new { @class = \"control-label col-md-2\" })" +
                        "\n            <div class=\"col-md-10\">" +
                        "\n                <input type=\"text\" name=\"{1}\" class=\"form-control\" ng-model=\"user.{1}\" required />" +
                        "\n                @Html.ValidationMessageFor(model => model.{0}, \"\", new { @class = \"text-danger\" })" +
                        "\n            </div>" +
                        "\n        </div>";
            }
        }

        /// <summary>
        /// Predložak za repozitorij
        /// </summary>
        public static string RepositoryTemplate
        {
            get
            {
                return "\nusing BikeGround.Models;" +
                        "\nusing Dapper;" +
                        "\nusing Dapper.DataRepositories;" +
                        "\nusing MicroOrm.Pocos.SqlGenerator;" +
                        "\nusing System.Collections.Generic;" +
                        "\nusing System.Data;" +
                        "\nusing System.Threading.Tasks;" +
                        "\n" +
                        "\nnamespace BikeGround.DataLayer.Repositories" +
                        "\n{{" +
                        "\n    interface I{0}Repository : IDataRepository<{0}>" +
                        "\n    {{" +
                        "\n        //I{0}Repository is inheriting all CRUD operations " +
                        "\n    }}" +
                        "\n" +
                        "\n    public class {0}Repository : DataRepository<{0}>, I{0}Repository" +
                        "\n    {{" +
                        "\n        public {0}Repository(IDbConnection connection, ISqlGenerator<{0}> sqlGenerator)" +
                        "\n            : base(connection, sqlGenerator)" +
                        "\n        {{" +
                        "\n        }}" +
                        "\n" +
                        "\n    }}" +
                        "\n}}";
            }
        }

        /// <summary>
        /// Predložak za interface repozitorij
        /// </summary>
        public static string IRepositoryTemplate
        {
            get
            {
                return "\ninterface I{0}Repository : IDataRepository<{0}>" +
                       "\n{{" +
                       "\n    //IProfileRepository is inheriting all CRUD operations " +
                       "\n}}";
            }
        }

        /// <summary>
        /// {0} - Assembly
        /// </summary>
        public static string ResxFileTemplate
        {
            get
            {
                return "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                        "<root>" + Environment.NewLine +
                        "  <!-- " + Environment.NewLine +
                        "    Microsoft ResX Schema " + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    Version 2.0" + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    The primary goals of this format is to allow a simple XML format " + Environment.NewLine +
                        "    that is mostly human readable. The generation and parsing of the " + Environment.NewLine +
                        "    various data types are done through the TypeConverter classes " + Environment.NewLine +
                        "    associated with the data types." + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    Example:" + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    ... ado.net/XML headers & schema ..." + Environment.NewLine +
                        "    <resheader name=\"resmimetype\">text/microsoft-resx</resheader>" + Environment.NewLine +
                        "    <resheader name=\"version\">2.0</resheader>" + Environment.NewLine +
                        "    <resheader name=\"reader\">System.Resources.ResXResourceReader, System.Windows.Forms, ...</resheader>" + Environment.NewLine +
                        "    <resheader name=\"writer\">System.Resources.ResXResourceWriter, System.Windows.Forms, ...</resheader>" + Environment.NewLine +
                        "    <data name=\"Name1\"><value>this is my long string</value><comment>this is a comment</comment></data>" + Environment.NewLine +
                        "    <data name=\"Color1\" type=\"System.Drawing.Color, System.Drawing\">Blue</data>" + Environment.NewLine +
                        "    <data name=\"Bitmap1\" mimetype=\"application/x-microsoft.net.object.binary.base64\">" + Environment.NewLine +
                        "        <value>[base64 mime encoded serialized .NET Framework object]</value>" + Environment.NewLine +
                        "    </data>" + Environment.NewLine +
                        "    <data name=\"Icon1\" type=\"System.Drawing.Icon, System.Drawing\" mimetype=\"application/x-microsoft.net.object.bytearray.base64\">" + Environment.NewLine +
                        "        <value>[base64 mime encoded string representing a byte array form of the .NET Framework object]</value>" + Environment.NewLine +
                        "        <comment>This is a comment</comment>" + Environment.NewLine +
                        "    </data>" + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    There are any number of \"resheader\" rows that contain simple " + Environment.NewLine +
                        "    name/value pairs." + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    Each data row contains a name, and value. The row also contains a " + Environment.NewLine +
                        "    type or mimetype. Type corresponds to a .NET class that support " + Environment.NewLine +
                        "    text/value conversion through the TypeConverter architecture. " + Environment.NewLine +
                        "    Classes that don't support this are serialized and stored with the " + Environment.NewLine +
                        "    mimetype set." + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    The mimetype is used for serialized objects, and tells the " + Environment.NewLine +
                        "    ResXResourceReader how to depersist the object. This is currently not " + Environment.NewLine +
                        "    extensible. For a given mimetype the value must be set accordingly:" + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    Note - application/x-microsoft.net.object.binary.base64 is the format " + Environment.NewLine +
                        "    that the ResXResourceWriter will generate, however the reader can " + Environment.NewLine +
                        "    read any of the formats listed below." + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    mimetype: application/x-microsoft.net.object.binary.base64" + Environment.NewLine +
                        "    value   : The object must be serialized with " + Environment.NewLine +
                        "            : System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" + Environment.NewLine +
                        "            : and then encoded with base64 encoding." + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    mimetype: application/x-microsoft.net.object.soap.base64" + Environment.NewLine +
                        "    value   : The object must be serialized with " + Environment.NewLine +
                        "            : System.Runtime.Serialization.Formatters.Soap.SoapFormatter" + Environment.NewLine +
                        "            : and then encoded with base64 encoding." + Environment.NewLine +
                        "" + Environment.NewLine +
                        "    mimetype: application/x-microsoft.net.object.bytearray.base64" + Environment.NewLine +
                        "    value   : The object must be serialized into a byte array " + Environment.NewLine +
                        "            : using a System.ComponentModel.TypeConverter" + Environment.NewLine +
                        "            : and then encoded with base64 encoding." + Environment.NewLine +
                        "    -->" + Environment.NewLine +
                        "  <xsd:schema id=\"root\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">" + Environment.NewLine +
                        "    <xsd:import namespace=\"http://www.w3.org/XML/1998/namespace\" />" + Environment.NewLine +
                        "    <xsd:element name=\"root\" msdata:IsDataSet=\"true\">" + Environment.NewLine +
                        "      <xsd:complexType>" + Environment.NewLine +
                        "        <xsd:choice maxOccurs=\"unbounded\">" + Environment.NewLine +
                        "          <xsd:element name=\"metadata\">" + Environment.NewLine +
                        "            <xsd:complexType>" + Environment.NewLine +
                        "              <xsd:sequence>" + Environment.NewLine +
                        "                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" />" + Environment.NewLine +
                        "              </xsd:sequence>" + Environment.NewLine +
                        "              <xsd:attribute name=\"name\" use=\"required\" type=\"xsd:string\" />" + Environment.NewLine +
                        "              <xsd:attribute name=\"type\" type=\"xsd:string\" />" + Environment.NewLine +
                        "              <xsd:attribute name=\"mimetype\" type=\"xsd:string\" />" + Environment.NewLine +
                        "              <xsd:attribute ref=\"xml:space\" />" + Environment.NewLine +
                        "            </xsd:complexType>" + Environment.NewLine +
                        "          </xsd:element>" + Environment.NewLine +
                        "          <xsd:element name=\"assembly\">" + Environment.NewLine +
                        "            <xsd:complexType>" + Environment.NewLine +
                        "              <xsd:attribute name=\"alias\" type=\"xsd:string\" />" + Environment.NewLine +
                        "              <xsd:attribute name=\"name\" type=\"xsd:string\" />" + Environment.NewLine +
                        "            </xsd:complexType>" + Environment.NewLine +
                        "          </xsd:element>" + Environment.NewLine +
                        "          <xsd:element name=\"data\">" + Environment.NewLine +
                        "            <xsd:complexType>" + Environment.NewLine +
                        "              <xsd:sequence>" + Environment.NewLine +
                        "                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />" + Environment.NewLine +
                        "                <xsd:element name=\"comment\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"2\" />" + Environment.NewLine +
                        "              </xsd:sequence>" + Environment.NewLine +
                        "              <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" msdata:Ordinal=\"1\" />" + Environment.NewLine +
                        "              <xsd:attribute name=\"type\" type=\"xsd:string\" msdata:Ordinal=\"3\" />" + Environment.NewLine +
                        "              <xsd:attribute name=\"mimetype\" type=\"xsd:string\" msdata:Ordinal=\"4\" />" + Environment.NewLine +
                        "              <xsd:attribute ref=\"xml:space\" />" + Environment.NewLine +
                        "            </xsd:complexType>" + Environment.NewLine +
                        "          </xsd:element>" + Environment.NewLine +
                        "          <xsd:element name=\"resheader\">" + Environment.NewLine +
                        "            <xsd:complexType>" + Environment.NewLine +
                        "              <xsd:sequence>" + Environment.NewLine +
                        "                <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />" + Environment.NewLine +
                        "              </xsd:sequence>" + Environment.NewLine +
                        "              <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" />" + Environment.NewLine +
                        "            </xsd:complexType>" + Environment.NewLine +
                        "          </xsd:element>" + Environment.NewLine +
                        "        </xsd:choice>" + Environment.NewLine +
                        "      </xsd:complexType>" + Environment.NewLine +
                        "    </xsd:element>" + Environment.NewLine +
                        "  </xsd:schema>" + Environment.NewLine +
                        "  <resheader name=\"resmimetype\">" + Environment.NewLine +
                        "    <value>text/microsoft-resx</value>" + Environment.NewLine +
                        "    </resheader>" + Environment.NewLine +
                        "    <resheader name=\"version\">" + Environment.NewLine +
                        "    <value>2.0</value>" + Environment.NewLine +
                        "  </resheader>" + Environment.NewLine +
                        "  <resheader name=\"reader\">" + Environment.NewLine +
                        "    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>" + Environment.NewLine +
                        "  </resheader>" + Environment.NewLine +
                        "  <resheader name=\"writer\">" + Environment.NewLine +
                        "    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>" + Environment.NewLine +
                        "  </resheader>" + Environment.NewLine +
                        "  {0}" + Environment.NewLine +
                        "</root>";
            }
        }

        public static string SingleRresxItem
        {
            get
            {
                return "  <data name=\"{0}\" xml:space=\"preserve\">" + Environment.NewLine +
                       "    <value>{0}</value>" + Environment.NewLine +
                       "  </data>" + Environment.NewLine;
            }
        }

        public static string ResxDesignerFileTemplate
        {
            get
            {
                return "//------------------------------------------------------------------------------" + Environment.NewLine +
                        "// <auto-generated>" + Environment.NewLine +
                        "//     This code was generated by a tool." + Environment.NewLine +
                        "//     Runtime Version:4.0.30319.18444" + Environment.NewLine +
                        "//" + Environment.NewLine +
                        "//     Changes to this file may cause incorrect behavior and will be lost if" + Environment.NewLine +
                        "//     the code is regenerated." + Environment.NewLine +
                        "// </auto-generated>" + Environment.NewLine +
                        "//------------------------------------------------------------------------------" + Environment.NewLine +
                        "" + Environment.NewLine +
                        "namespace {2} {{" + Environment.NewLine +
                        "    using System;" + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    " + Environment.NewLine +
                        "    /// <summary>" + Environment.NewLine +
                        "    ///   A strongly-typed resource class, for looking up localized strings, etc." + Environment.NewLine +
                        "    /// </summary>" + Environment.NewLine +
                        "    // This class was auto-generated by the StronglyTypedResourceBuilder" + Environment.NewLine +
                        "    // class via a tool like ResGen or Visual Studio." + Environment.NewLine +
                        "    // To add or remove a member, edit your .ResX file then rerun ResGen" + Environment.NewLine +
                        "    // with the /str option, or rebuild your VS project." + Environment.NewLine +
                        "    [global::System.CodeDom.Compiler.GeneratedCodeAttribute(\"System.Resources.Tools.StronglyTypedResourceBuilder\", \"4.0.0.0\")]" + Environment.NewLine +
                        "    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]" + Environment.NewLine +
                        "    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]" + Environment.NewLine +
                        "    public class {1} {{" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        private static global::System.Resources.ResourceManager resourceMan;" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        private static global::System.Globalization.CultureInfo resourceCulture;" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"Microsoft.Performance\", \"CA1811:AvoidUncalledPrivateCode\")]" + Environment.NewLine +
                        "        internal {1}() {{" + Environment.NewLine +
                        "        }}" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        /// <summary>" + Environment.NewLine +
                        "        ///   Returns the cached ResourceManager instance used by this class." + Environment.NewLine +
                        "        /// </summary>" + Environment.NewLine +
                        "        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]" + Environment.NewLine +
                        "        public static global::System.Resources.ResourceManager ResourceManager {{" + Environment.NewLine +
                        "            get {{" + Environment.NewLine +
                        "                if (object.ReferenceEquals(resourceMan, null)) {{" + Environment.NewLine +
                        "                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager(\"{2}.{1}\", typeof({1}).Assembly);" + Environment.NewLine +
                        "                    resourceMan = temp;" + Environment.NewLine +
                        "                }}" + Environment.NewLine +
                        "                return resourceMan;" + Environment.NewLine +
                        "            }}" + Environment.NewLine +
                        "        }}" + Environment.NewLine +
                        "        " + Environment.NewLine +
                        "        /// <summary>" + Environment.NewLine +
                        "        ///   Overrides the current thread's CurrentUICulture property for all" + Environment.NewLine +
                        "        ///   resource lookups using this strongly typed resource class." + Environment.NewLine +
                        "        /// </summary>" + Environment.NewLine +
                        "        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]" + Environment.NewLine +
                        "        public static global::System.Globalization.CultureInfo Culture {{" + Environment.NewLine +
                        "            get {{" + Environment.NewLine +
                        "                return resourceCulture;" + Environment.NewLine +
                        "            }}" + Environment.NewLine +
                        "            set {{" + Environment.NewLine +
                        "                resourceCulture = value;" + Environment.NewLine +
                        "            }}" + Environment.NewLine +
                        "        }}" + Environment.NewLine +
                        "        {0}" + Environment.NewLine +
                        "    }}" + Environment.NewLine +
                        "}}";
            }
        }

        public static string SingleDesignerItem
        {
            get
            {
                return "" + Environment.NewLine +
                        "        /// <summary>" + Environment.NewLine +
                        "        ///   Looks up a localized string similar to {0} {1}." + Environment.NewLine +
                        "        /// </summary>" + Environment.NewLine +
                        "        public static string {0} {{" + Environment.NewLine +
                        "            get {{" + Environment.NewLine +
                        "                return ResourceManager.GetString(\"{0}\", resourceCulture);" + Environment.NewLine +
                        "            }}" + Environment.NewLine +
                        "        }}" + Environment.NewLine;
            }
        }

        public static string MVCControler
        {
            get
            {
                return "using System.Web.Mvc;" + Environment.NewLine +
                        "" + Environment.NewLine +
                        "namespace BikeGround.Web.Controllers" + Environment.NewLine +
                        "{{" + Environment.NewLine +
                        "    [Authorize]" + Environment.NewLine +
                        "    public class {0}Controller : Controller" + Environment.NewLine +
                        "    {{" + Environment.NewLine +
                        "        // GET: {0}" + Environment.NewLine +
                        "        public ActionResult Index()" + Environment.NewLine +
                        "        {{" + Environment.NewLine +
                        "            return PartialView();" + Environment.NewLine +
                        "        }}" + Environment.NewLine +
                        "    }}" + Environment.NewLine +
                        "}}";
            }
        }

        public static string APIController
        {
            get
            {
                return "\nusing BikeGround.API.Common;" +
                        "\nusing BikeGround.DataLayer;" +
                        "\nusing BikeGround.DataLayer.Repositories;" +
                        "\nusing BikeGround.Models;" +
                        "\nusing MicroOrm.Pocos.SqlGenerator;" +
                        "\nusing System;" +
                        "\nusing System.Collections.Generic;" +
                        "\nusing System.Data.SqlClient;" +
                        "\nusing System.Diagnostics;" +
                        "\nusing System.Net;" +
                        "\nusing System.Net.Http;" +
                        "\nusing System.Security.Claims;" +
                        "\nusing System.Threading;" +
                        "\nusing System.Threading.Tasks;" +
                        "\nusing System.Web.Http;" +
                        "\nusing System.Web.Http.Cors;" +
                        "\n" +
                        "\nnamespace BikeGround.API.Controllers" +
                        "\n{{" +
                        "\n    [Authorize]" +
                        "\n    [EnableCors(\"http://localhost:3668\", \"*\", \"*\")]" +
                        "\n    public class {0}Controller : ApiController" +
                        "\n    {{" +
                        "\n        private readonly SqlConnection _sqlCon = new SqlConnection(ConfigurationSettings.GetConnectionString());" +
                        "\n        private readonly ISqlGenerator<{0}> _sqlGenerator = new SqlGenerator<{0}>();" +
                        "\n        private long LogedUserID {{ get; set; }}" +
                        "\n" +
                        "\n        /// <summary>" +
                        "\n        ///     Konstruktor, inicijalizira UserID" +
                        "\n        /// </summary>" +
                        "\n        public {0}Controller()" +
                        "\n        {{" +
                        "\n            this.LogedUserID = Helpers.GetUserIDFromClaims((ClaimsPrincipal)Thread.CurrentPrincipal);" +
                        "\n        }}" +
                        "\n" +
                        "\n        #region Logirani user" +
                        "\n" +
                        "\n        /// <summary>" +
                        "\n        /// Dohvaća listu podataka isključivo preko UserID-a" +
                        "\n        /// </summary>" +
                        "\n        /// <param name=\"sinceId\">Od podatak</param>" +
                        "\n        /// <param name=\"count\">Veličina stranice</param>" +
                        "\n        /// <returns>Listu objekata</returns>" +
                        "\n        [Route(\"api/{1}/multiple\"), HttpGet]" +
                        "\n        public async Task<HttpResponseMessage> GetUserAll(string sinceId = null, string count = null)" +
                        "\n        {{" +
                        "\n            IEnumerable<{0}> items;" +
                        "\n" +
                        "\n            var _{1}Repository = new {0}Repository(_sqlCon, _sqlGenerator);" +
                        "\n" +
                        "\n            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))" +
                        "\n            {{" +
                        "\n                items = await _{1}Repository.GetWhereAsyncPaged(new {{ UserID = this.LogedUserID }}, sinceId, count);" +
                        "\n            }}" +
                        "\n            else" +
                        "\n            {{" +
                        "\n                items = await _{1}Repository.GetWhereAsync(new {{ UserID = this.LogedUserID }});" +
                        "\n            }}" +
                        "\n" +
                        "\n            if (items == null)" +
                        "\n            {{" +
                        "\n                throw new HttpResponseException(HttpStatusCode.NotFound);" +
                        "\n            }}" +
                        "\n" +
                        "\n            Debug.WriteLine(\"api/{1}/multiple\");" +
                        "\n            return Request.CreateResponse(HttpStatusCode.OK, items);" +
                        "\n        }}" +
                        "\n" +
                        "\n        /// <summary>" +
                        "\n        /// Dohvaća jedan jedini podatak preko UserID-a" +
                        "\n        /// </summary>" +
                        "\n        /// <returns>Objekt</returns>" +
                        "\n        [AcceptVerbs(\"GET\")]" +
                        "\n        [Route(\"api/{1}/single\"), HttpGet]" +
                        "\n        public async Task<HttpResponseMessage> GetUserSingle()" +
                        "\n        {{" +
                        "\n            var item = new {0}();" +
                        "\n" +
                        "\n            var _{1}Repository = new {0}Repository(_sqlCon, _sqlGenerator);" +
                        "\n" +
                        "\n            item = await _{1}Repository.GetFirstAsync(new {{ UserID = this.LogedUserID }});" +
                        "\n" +
                        "\n            Debug.WriteLine(\"api/{1}/single\");" +
                        "\n            return Request.CreateResponse(HttpStatusCode.OK, item);" +
                        "\n        }}" +
                        "\n" +
                        "\n        [Route(\"api/{1}\"), HttpPost]" +
                        "\n        public async Task<HttpResponseMessage> Post([FromBody] {0} obj)" +
                        "\n        {{" +
                        "\n            obj.UserID = this.LogedUserID;" +
                        "\n" +
                        "\n            if (ModelState.IsValid)" +
                        "\n            {{" +
                        "\n                var _{1}Repository = new {0}Repository(_sqlCon, _sqlGenerator);" +
                        "\n                var ID = await _{1}Repository.InsertAsync(obj);" +
                        "\n" +
                        "\n                if (ID > 0)" +
                        "\n                {{" +
                        "\n                    return Request.CreateResponse(HttpStatusCode.Created, ID);" +
                        "\n                }}" +
                        "\n" +
                        "\n                throw new HttpResponseException(HttpStatusCode.Conflict);" +
                        "\n            }}" +
                        "\n" +
                        "\n            throw new HttpResponseException(HttpStatusCode.BadRequest);" +
                        "\n        }}" +
                        "\n" +
                        "\n        [Route(\"api/{1}/{{id}}\"), HttpPut]" +
                        "\n        public async Task<HttpResponseMessage> Put(long Id, [FromBody] {0} obj)" +
                        "\n        {{" +
                        "\n            obj.UserID = this.LogedUserID;" +
                        "\n" +
                        "\n            if (ModelState.IsValid)" +
                        "\n            {{" +
                        "\n                var _{1}Repository = new {0}Repository(_sqlCon, _sqlGenerator);" +
                        "\n" +
                        "\n                obj.ID = Id;" +
                        "\n" +
                        "\n                var item = await _{1}Repository.UpdateAsync(obj);" +
                        "\n" +
                        "\n                if (item)" +
                        "\n                {{" +
                        "\n                    var msg = new HttpResponseMessage(HttpStatusCode.OK);" +
                        "\n                    return msg;" +
                        "\n                }}" +
                        "\n            }}" +
                        "\n" +
                        "\n            throw new HttpResponseException(HttpStatusCode.NotFound);" +
                        "\n        }}" +
                        "\n" +
                        "\n        public async Task<HttpResponseMessage> Delete(long Id)" +
                        "\n        {{" +
                        "\n            var _{1}Repository = new {0}Repository(_sqlCon, _sqlGenerator);" +
                        "\n            var status = await _{1}Repository.DeleteAsync(new {{ ID = Id, UserID = this.LogedUserID }});" +
                        "\n" +
                        "\n            if (status)" +
                        "\n            {{" +
                        "\n                return new HttpResponseMessage(HttpStatusCode.OK);" +
                        "\n            }}" +
                        "\n" +
                        "\n            throw new HttpResponseException(HttpStatusCode.NotFound);" +
                        "\n        }}" +
                        "\n" +
                        "\n        #endregion" +
                        "\n" +
                        "\n        #region Logirani user tudi podaci" +
                        "\n" +
                        "\n        [Route(\"api/{1}\"), HttpGet]" +
                        "\n        public async Task<HttpResponseMessage> GetAll(string sinceId = null, string count = null)" +
                        "\n        {{" +
                        "\n            IEnumerable<{0}> items;" +
                        "\n" +
                        "\n            var _{1}Repository = new {0}Repository(_sqlCon, _sqlGenerator);" +
                        "\n" +
                        "\n            if (!String.IsNullOrEmpty(sinceId) && !String.IsNullOrEmpty(count))" +
                        "\n            {{" +
                        "\n                items = await _{1}Repository.GetWhereAsyncPaged(new {{ UserID = this.LogedUserID }}, sinceId, count);" +
                        "\n            }}" +
                        "\n            else" +
                        "\n            {{" +
                        "\n                items = await _{1}Repository.GetWhereAsync(new {{ UserID = this.LogedUserID }});" +
                        "\n            }}" +
                        "\n" +
                        "\n            if (items == null)" +
                        "\n            {{" +
                        "\n                throw new HttpResponseException(HttpStatusCode.NotFound);" +
                        "\n            }}" +
                        "\n" +
                        "\n            Debug.WriteLine(\"api/{1}\");" +
                        "\n            return Request.CreateResponse(HttpStatusCode.OK, items);" +
                        "\n        }}" +
                        "\n" +
                        "\n        [AcceptVerbs(\"GET\")]" +
                        "\n        [Route(\"api/{1}/{{id}}\"), HttpGet]" +
                        "\n        public async Task<HttpResponseMessage> GetSingle(long Id)" +
                        "\n        {{" +
                        "\n            var item = new {0}();" +
                        "\n" +
                        "\n            var _{1}Repository = new {0}Repository(_sqlCon, _sqlGenerator);" +
                        "\n" +
                        "\n            item = await _{1}Repository.GetFirstAsync(new {{ ID = Id }});" +
                        "\n" +
                        "\n            if (item == null)" +
                        "\n            {{" +
                        "\n                item = new {0}();" +
                        "\n            }}" +
                        "\n" +
                        "\n            Debug.WriteLine(\"api/{1}/{{id}}\");" +
                        "\n            return Request.CreateResponse(HttpStatusCode.OK, item);" +
                        "\n        }}" +
                        "\n" +
                        "\n        #endregion" +
                        "\n    }}" +
                        "\n}}";
            }
        }
    }
}