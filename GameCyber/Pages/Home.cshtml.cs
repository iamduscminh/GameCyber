using GameCyber.Entities;
using GameCyber.Logics;
using GameCyber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameCyber.Pages
{
    public class HomeModel : PageModel
    {
        public User User { get; set; }
        public BaseViewModel BaseViewModel { get; set; }
        public ChatViewModel ChatViewModel { get; set; }
        public GameViewModel GameViewModel { get; set; }
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
            GameViewModel = new();
            var userLogic = new UserLogic();
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var pSize = MyConfig.GetValue<string>("AppSettings:PageSize");
            var cash = MyConfig.GetValue<string>("AppSettings:CashPerHour");
            HttpContext.Session.SetString("pSize", pSize);
            HttpContext.Session.SetString("cash", cash);

            User = commonLogic.GetUser(Convert.ToInt32(userId));
            BaseViewModel.userTimeRemain = commonLogic.ConvertTimeRemain(User.Cash.HasValue ? User.Cash.Value : 0, Convert.ToInt32(cash));
            ChatViewModel = userLogic.GetUserMessage(Convert.ToInt32(userId));
            GameViewModel.ListTopGames = userLogic.GetTopGames();
            GameViewModel.ListNewGames = userLogic.GetNewGames();

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
