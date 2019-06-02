namespace SIS.MvcFramework.ViewEngine
{
    class SisViewEngine : IViewEngine
    {
        public string GetHtml<T>(string viewContent, T model)
        {
            return viewContent;
        }
    }
}
