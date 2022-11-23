using GameCyber.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameCyber.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return Page();
        }
        public IActionResult OnPost()
        {
            var username = Request.Form["username"].ToString();
            var password = Request.Form["password"].ToString();

            if (username == null || password == null || username.Trim().Length == 0 || password.Trim().Length == 0)
            {
                ViewData["LoginMsg"] = "Nhap day du user va password!";
                return Page();
            }

            var user = new CommonLogic().Login(username, password);
            if (user == null)
            {
                ViewData["LoginMsg"] = "Kiem tra lai user password!";
                return Page();
            }

            if (user.Cash == 0)
            {
                ViewData["LoginMsg"] = "Tai khoan khong du de su dung!";
                return Page();
            }
            HttpContext.Session.SetString("userId", user.Id.ToString());
            if (user.Role == "0") return new RedirectToPageResult("Dashboard");
            else return new RedirectToPageResult("Home");
        }
    }
}