using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DatabaseProject.TagHelpers
{
    public class ExampleTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // <i></i>
            output.PreContent.SetHtmlContent("<i>");
            output.PostContent.SetHtmlContent("</i>");
        }
    }
}
