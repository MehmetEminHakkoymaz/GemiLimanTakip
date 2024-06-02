
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;

namespace WebApplication2.Helpers
{
    public static class HtmlHelpers
    {
        public static HtmlString TextBox(this IHtmlHelper html, string id, string name, string value = "", string cssClass = "form-control")
        {
            var textbox = string.Format("<input type='text' id='{0}' name='{1}' value='{2}' class='{3}' />", id, name, value, cssClass);
            return new HtmlString(textbox);
        }
    }
}