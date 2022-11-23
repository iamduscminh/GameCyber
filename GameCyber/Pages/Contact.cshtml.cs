using GameCyber.Entities;
using GameCyber.Logics;
using GameCyber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameCyber.Pages
{
    public class ContactModel : PageModel
    {
        public User User { get; set; }
        public BaseViewModel BaseViewModel { get; set; }
        public ChatViewModel ChatViewModel { get; set; }
        public IActionResult OnGet()
        {
            string userId = HttpContext.Session.GetString("userId");
            if (userId == null) return new RedirectToPageResult("Index");
            GetData(userId);
            return Page();
        }
        void GetData(string userId)
        {
            var commonLogic = new CommonLogic();
            BaseViewModel = new();
            ChatViewModel = new();

            User = commonLogic.GetUser(Convert.ToInt32(userId));
            BaseViewModel.userTimeRemain = commonLogic.ConvertTimeRemain(User.Cash.HasValue ? User.Cash.Value : 0, Convert.ToInt32(HttpContext.Session.GetString("cash")));
            ChatViewModel = new UserLogic().GetUserMessage(Convert.ToInt32(userId));
        }
        public ActionResult OnGetUpdateCash()
        {
            string userId = HttpContext.Session.GetString("userId");
            if (userId == null) return new RedirectToPageResult("Index");

            int curCash = new UserLogic().UpdateCashUser(Convert.ToInt32(userId), Convert.ToInt32(HttpContext.Session.GetString("cash")));
            string timeRemain = new CommonLogic().ConvertTimeRemain(curCash, Convert.ToInt32(HttpContext.Session.GetString("cash")));
            return new JsonResult(new { curCash = curCash, timeRemain = timeRemain, isCanPlay = curCash > 0 });
        }
    }
}
