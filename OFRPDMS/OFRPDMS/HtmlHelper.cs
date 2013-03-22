using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq.Expressions;


    public static class HtmlHelpers
    {

        public static IHtmlString LinkToRemoveNestedForm(this HtmlHelper htmlHelper, string linkText, string container, string deleteElement)
        {

            var js = string.Format("javascript:removeNestedForm(this,'{0}','{1}');return false;", container, deleteElement);

            TagBuilder tb = new TagBuilder("a");

            tb.Attributes.Add("href", "#");

            tb.Attributes.Add("onclick", js);

            tb.InnerHtml = linkText;

            var tag = tb.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(tag);

        }



        private static string JsEncode(this string s)
        {

            if (string.IsNullOrEmpty(s)) return "";

            int i;

            int len = s.Length;

            StringBuilder sb = new StringBuilder(len + 4);

            string t;



            for (i = 0; i < len; i += 1)
            {

                char c = s[i];

                switch (c)
                {

                    case '>':

                    case '"':

                    case '\\':

                        sb.Append('\\');

                        sb.Append(c);

                        break;

                    case '\b':

                        sb.Append("\\b");

                        break;

                    case '\t':

                        sb.Append("\\t");

                        break;

                    case '\n':

                        //sb.Append("\\n");

                        break;

                    case '\f':

                        sb.Append("\\f");

                        break;

                    case '\r':

                        //sb.Append("\\r");

                        break;

                    default:

                        if (c < ' ')
                        {

                            //t = "000" + Integer.toHexString(c); 

                            string tmp = new string(c, 1);

                            t = "000" + int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);

                            sb.Append("\\u" + t.Substring(t.Length - 4));

                        }

                        else
                        {

                            sb.Append(c);

                        }

                        break;

                }

            }

            return sb.ToString();

        }



        public static MvcHtmlString LinkToAddNestedForm<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string linkText, string containerElement, string counterElement, string cssClass = null) where TProperty : IEnumerable<object>
        {
            // a fake index to replace with a real index
            long ticks = DateTime.UtcNow.Ticks;
            // pull the name and type from the passed in expression
            string collectionProperty = ExpressionHelper.GetExpressionText(expression);
            var nestedObject = Activator.CreateInstance(typeof(TProperty).GetGenericArguments()[0]);

            // save the field prefix name so we can reset it when we're doing
            string oldPrefix = htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix;
            // if the prefix isn't empty, then prepare to append to it by appending another delimiter
            if (!string.IsNullOrEmpty(oldPrefix))
            {
                htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix += ".";
            }
            // append the collection name and our fake index to the prefix name before rendering
            htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix += string.Format("{0}[{1}]", collectionProperty, ticks);
            string partial = htmlHelper.EditorFor(x => nestedObject).ToHtmlString();


            // done rendering, reset prefix to old name
            htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix = oldPrefix;



            // strip out the fake name injected in (our name was all in the prefix)
            partial = Regex.Replace(partial, @"[\._]?nestedObject", "");



            // encode the output for javascript since we're dumping it in a JS string
            partial = HttpUtility.JavaScriptStringEncode(partial);



            // create the link to render
            var js = string.Format("javascript:addNestedForm('{0}','{1}','{2}','{3}');return false;", containerElement, counterElement, ticks, partial);
            TagBuilder a = new TagBuilder("a");
            a.Attributes.Add("href", "javascript:void(0)");
            a.Attributes.Add("onclick", js);
            if (cssClass != null)
            {
                a.AddCssClass(cssClass);
            }
            a.InnerHtml = linkText;



            return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
        }


    }
