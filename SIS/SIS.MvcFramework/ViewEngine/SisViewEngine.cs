namespace SIS.MvcFramework.ViewEngine
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class SisViewEngine : IViewEngine
    {
        public string GetHtml<T>(string viewContent, T model)
        {
            string csharpHtmlCode = GetCSharpCode(viewContent);
            string code = $@"
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using SIS.MvcFramework.ViewEngine;
namespace AppViewCodeNamespace
{{
    public class AppViewCode : IView
    {{
       public string GetHtml(object model)
        {{
            var Model = model as {model.GetType().FullName};
            var html = new StringBuilder();

            {csharpHtmlCode}

            return html.ToString();
        }}
    }}
}}";
            IView view = CompileAndInstance(code,model.GetType().Assembly);
            string htmlResult = view?.GetHtml(model);

            return htmlResult;
        }

        private IView CompileAndInstance(string code, Assembly modelAssembly)
        {
            var compilation = CSharpCompilation.Create("AppViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(modelAssembly.Location));

            var netStandardAssembly = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();
            foreach (AssemblyName assemblyName in netStandardAssembly)
            {
                compilation =
                    compilation.AddReferences(MetadataReference.CreateFromFile(Assembly.Load(assemblyName).Location));
            }

            compilation = compilation.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(code));

            using (var memoryStream = new MemoryStream())
            {
                var compilationResult = compilation.Emit(memoryStream);

                if (!compilationResult.Success)
                {
                    foreach (var error in compilationResult.Diagnostics) //TODO: .Where(x=>x.Severity == DiagnosticSeverity.Error)
                    {
                        Console.WriteLine(error.GetMessage());
                    }


                    return null;
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                var assemblyBytes = memoryStream.ToArray();
                var assembly = Assembly.Load(assemblyBytes);

                var type = assembly.GetType("AppViewCodeNamespace.AppViewCode");
                if (type == null)
                {
                    Console.WriteLine("AppViewCode not found.");
                    return null;
                }

                var instance = Activator.CreateInstance(type);
                if (instance == null)
                {
                    Console.WriteLine("AppViewCode cannot be instantiated.");
                    return null;
                }


                return instance as IView;
            }
        }

        private string GetCSharpCode(string viewContent)
        {
            var lines = viewContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            var cSharpCode = new StringBuilder();
            var supportedOperators = new[] { "for", "if", "else", "foreach" };
            foreach (var line in lines)
            {
                if (line.TrimStart().StartsWith("{") || line.TrimStart().StartsWith("}"))
                {
                    // { || }
                    cSharpCode.AppendLine(line);
                }
                else if (supportedOperators.Any(x => line.TrimStart().StartsWith("@" + x)))
                {
                    // C# Code
                    var atSignLocation = line.IndexOf("@", StringComparison.Ordinal);
                    var csharpLine = line.Remove(atSignLocation, 1);
                    cSharpCode.AppendLine(csharpLine);
                }
                else
                {
                    // HTML
                    if (!line.Contains("@"))
                    {
                        var csharpLine = $"html.AppendLine(@\"{line.Replace("\"", "\"\"")}\");";
                        cSharpCode.AppendLine(csharpLine);
                    }
                    else
                    {
                        var csharpStringToAppend = "html.AppendLine(@\"";
                        var restOfLine = line;
                        while (restOfLine.Contains("@"))
                        {
                            var indexOfAtSign = restOfLine.IndexOf("@", StringComparison.Ordinal);
                            var plainText = restOfLine.Substring(0, indexOfAtSign);
                            Regex csharpCodeRegex = new Regex(@"[^\s<""]+", RegexOptions.Compiled);
                            var csharpExpression = csharpCodeRegex.Match(restOfLine.Substring(indexOfAtSign + 1))?.Value;
                            csharpStringToAppend += plainText + "\" + " + csharpExpression + " + @\"";


                            if (restOfLine.Length <= indexOfAtSign + csharpExpression.Length + 1)
                            {
                                restOfLine = string.Empty;
                            }
                            else
                            {
                                restOfLine = restOfLine.Substring(indexOfAtSign + csharpExpression.Length + 1);
                            }
                        }

                        csharpStringToAppend += $"{restOfLine}\");";
                        cSharpCode.AppendLine(csharpStringToAppend);
                    }

                }
            }


            return cSharpCode.ToString();
        }
    }
}