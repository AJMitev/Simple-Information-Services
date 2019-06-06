namespace SIS.MvcFramework.ViewEngine
{
    using Identity;

    public interface IViewEngine
    {
        string GetHtml<T>(string viewContent, T model, Principal user = null);
    }
}