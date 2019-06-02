namespace SIS.MvcFramework.ViewEngine
{
    public class SisViewEngine : IViewEngine
    {
        public string GetHtml<T>(string viewContent, T model)
        {
            //TODO: https://youtu.be/F5kuK80hWUM?t=6175
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
        string GetHtml()
        {{
            var html = new StringBuilder();

            {{csharpHtmlCode}}

            return html.ToString();
        }}
    }}
}}";
            IView view = CompileAndInstance(code);
            string htmlResult = view?.GetHtml();

            return htmlResult;
        }

        private IView CompileAndInstance(string code)
        {
            throw new System.NotImplementedException();
        }

        private string GetCSharpCode(string viewContent)
        {
            throw new System.NotImplementedException();
        }
    }
}
