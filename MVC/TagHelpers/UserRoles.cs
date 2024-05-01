using Business.Services.Abstracts;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MVC.TagHelpers
{
    [HtmlTargetElement("td",Attributes="asp-users-roles")]
    public class UserRoles : TagHelper
    {
        private readonly IUserService _userService;

        public UserRoles(IUserService userService)
        {
            _userService = userService;
        }

        [HtmlAttributeName("asp-users-roles")]
        public string UserId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> roles = await _userService.GetUserRole(UserId);

            output.Content.SetContent(roles.Count == 0 ? "no role for this user" : string.Join(", ", roles));
        }
    }
}
