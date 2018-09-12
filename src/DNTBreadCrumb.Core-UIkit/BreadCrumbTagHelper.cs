using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DNTBreadCrumb.Core
{
    /// <summary>
    /// BreadCrumb TagHelper
    /// </summary>
    [HtmlTargetElement("breadcrumb")]
    public class BreadCrumbTagHelper : TagHelper
    {
        /// <summary>
        /// Such as 'Home'
        /// </summary>
        [HtmlAttributeName("asp-homepage-title")]
        public string HomePageTitle { set; get; }

        /// <summary>
        /// Such as @Url.Action("Index", "Home")
        /// </summary>
        [HtmlAttributeName("asp-homepage-url")]
        public string HomePageUrl { set; get; }

        /// <summary>
        /// such as 'heart'
        /// </summary>
        [HtmlAttributeName("asp-homepage-icon")]
        public string HomePageIcon { set; get; }


        /// <summary>
        ///
        /// </summary>
        protected HttpRequest Request => ViewContext.HttpContext.Request;

        /// <summary>
        ///
        /// </summary>
        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var breadCrumbs = ViewContext.HttpContext.Items[BreadCrumbExtentions.CurrentBreadCrumbKey] as List<BreadCrumb>;
            if (breadCrumbs == null || !breadCrumbs.Any())
            {
                return;
            }

            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "uk-breadcrumb");

            {
                var itemBuilder = new TagBuilder("li");

                itemBuilder.InnerHtml.AppendHtml($"<a href='{HomePageUrl}'>");
                if (!string.IsNullOrWhiteSpace(HomePageIcon))
                {
                    itemBuilder.InnerHtml.AppendHtml(
                        $"<span class='uk-margin-small-right' uk-icon='{HomePageIcon}'></span> ");
                }
                itemBuilder.InnerHtml.AppendHtml($"{HomePageTitle}");
                itemBuilder.InnerHtml.AppendHtml("</a>");

                output.Content.AppendHtml(itemBuilder);
            }


            foreach (var node in breadCrumbs.OrderBy(x => x.Order))
            {
                if (node.Url != null && node.Url.Equals(HomePageUrl, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                {
                    var itemBuilder = new TagBuilder("li");

                    itemBuilder.InnerHtml.AppendHtml($"<a href='{node.Url}'>");
                    if (!string.IsNullOrWhiteSpace(node.Icon))
                    {
                        itemBuilder.InnerHtml.AppendHtml(
                            $"<span class='uk-margin-small-right' uk-icon='{node.Icon}'></span> ");
                    }
                    itemBuilder.InnerHtml.AppendHtml($"{node.Title}");
                    itemBuilder.InnerHtml.AppendHtml("</a>");
                    output.Content.AppendHtml(itemBuilder);
                }
            }
        }

    }
}