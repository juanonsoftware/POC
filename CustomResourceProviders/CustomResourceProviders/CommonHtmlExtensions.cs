using System.Web.Mvc;

namespace CustomResourceProviders
{
    public static class CommonHtmlExtensions
    {
        public static object GetGlobalResource(this HtmlHelper htmlHelper, string classKey, string resourceKey)
        {
            return htmlHelper.ViewContext.HttpContext.GetGlobalResourceObject(classKey, resourceKey);
        }
    }
}