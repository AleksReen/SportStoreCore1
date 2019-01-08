using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SportStoreCore1.Infrastructure
{
    [HtmlTargetElement("messanger", Attributes = "page-message")]
    public class TempDataMessageTagHelper: TagHelper
    {
        public string PageMessage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {          
            var div = new TagBuilder("div");
            div.InnerHtml.Append($"{PageMessage} has been saved");
            div.AddCssClass("alert alert-success messangerBody");
          
            output.Content.AppendHtml(div);
        }

    }
}
