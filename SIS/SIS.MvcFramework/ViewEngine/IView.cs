namespace SIS.MvcFramework.ViewEngine
{
    using Identity;

    public interface IView
    {
        string GetHtml(object model, Principal user );
    }
}