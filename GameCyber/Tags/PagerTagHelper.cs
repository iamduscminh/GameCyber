using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GameCyber.Tags
{
    [HtmlTargetElement("pager")]
    public class PagerTagHelper : TagHelper
    {
        public int TotalPage { get; set; }
        public string Url { get; set; }
        public int CurrentPage { get; set; }
        public string ActiveClass { get; set; }

        //public override void Process(TagHelperContext context, TagHelperOutput output)
        //{
        //    output.TagMode = TagMode.StartTagAndEndTag;
        //    output.TagName = "div";
        //    for (int i = 1; i <= TotalPage; i++)
        //        if (i == CurrentPage)
        //            output.Content.AppendHtml($"<a style='margin:4px;' class={ActiveClass} href='{Url}/{i}'>{i}</a>");
        //        else
        //            output.Content.AppendHtml($"<a style='margin:4px;' href='{Url}/{i}'>{i}</a>");
        //}
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            int pagel = (CurrentPage - 1) < 0 ? 0 : CurrentPage - 1;
            int pager = (CurrentPage + 1);
            output.TagName = "ul";
            output.Attributes.SetAttribute("class", "page-pagination");
            output.Content.AppendHtml($"<li><a href=\"{Url}/{pagel}\"><i class=\"icofont-long-arrow-left\"></i></a></li>");
            for (int i = 1; i <= TotalPage; i++)
            {
                if (i == CurrentPage)
                    output.Content.AppendHtml($"<a style='margin:4px;' class={ActiveClass} href='{Url}/{i}'>{i}</a>");
                else
                    output.Content.AppendHtml($"<a style='margin:4px;' href='{Url}/{i}'>{i}</a>");
            }
            output.Content.AppendHtml($"<li><a href=\"{Url}/{pager}\"><i class=\"icofont-long-arrow-right\"></i></a></li>");
        }
    }
}
