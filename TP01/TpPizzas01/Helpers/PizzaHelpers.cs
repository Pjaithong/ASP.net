using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TpPizzas01.Helpers
{
    public static class PizzaHelpers
    {
        /// <summary>
        /// Creates simple button
        /// <div class="form-group">
        ///     <div class="col-md-offset-2 col-md-10">
        ///         <input type = "submit" value="@name" class="btn btn-default" />
        ///     </div>
        /// </div>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IHtmlString CostumSubmit(this HtmlHelper helper, string name)
        {  
            StringBuilder res = new StringBuilder();
            res.Append("<div class=\"form - group\">");
            res.Append("<div class=\"col - md - offset - 2 col - md - 10\">");
            res.Append($"<input type = \"submit\" value=\"{@name}\" class=\"btn btn - default\" />");
            res.Append("</div>");
            res.Append("</div>");

            return new MvcHtmlString(res.ToString());
        }
    }
}