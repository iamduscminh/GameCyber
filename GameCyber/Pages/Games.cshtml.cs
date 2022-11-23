using GameCyber.Entities;
using GameCyber.Logics;
using GameCyber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameCyber.Pages
{
    public class GamesModel : PageModel
    {
        public User User { get; set; }
        public BaseViewModel BaseViewModel { get; set; }
        public ChatViewModel ChatViewModel { get; set; }
        public List<Game> ListGame { get; set; }
        public int TotalGame { get; set; } = -1;
        public IActionResult OnGet(int PageIndex)
        {
            string userId = HttpContext.Session.GetString("userId");
            if (userId == null) return new RedirectToPageResult("Index");
            GetData(userId);

            //Paging
            var userLogic = new UserLogic();
            if (PageIndex <= 0) PageIndex = 1;
            int PageSize = Convert.ToInt32(HttpContext.Session.GetString("pSize"));

            if(ListGame==null)
                ListGame = userLogic.GetGames((PageIndex - 1) * PageSize + 1, PageSize);

            //Lay thong tin de hien thi thanh Pager.
            
            if(TotalGame==-1)
                TotalGame = userLogic.GetGames().Count;
            int TotalPage = (TotalGame / PageSize);
            if (TotalGame % PageSize != 0) TotalPage++;
            ViewData["TotalPage"] = TotalPage;
            ViewData["CurrentPage"] = PageIndex;
            ViewData["PageSize"] = PageSize;
            return Page();
        }

      

        public IActionResult OnPostSearchGame()
        {
            string userId = HttpContext.Session.GetString("userId");
            if (userId == null) return new RedirectToPageResult("Index");
            GetData(userId);
            var txt = Request.Form["game_search"].ToString();
            int PageIndex = 0;
            //Paging
            var userLogic = new UserLogic();
            if (PageIndex <= 0) PageIndex = 1;
            int PageSize = Convert.ToInt32(HttpContext.Session.GetString("pSize"));

            ListGame = userLogic.GetGamesSearch((PageIndex - 1) * PageSize + 1, PageSize, txt);

            //Lay thong tin de hien thi thanh Pager.
            TotalGame = userLogic.GetGamesSearch(txt).Count;
            int TotalPage = (TotalGame / PageSize);
            if (TotalGame % PageSize != 0) TotalPage++;
            ViewData["TotalPage"] = TotalPage;
            ViewData["CurrentPage"] = PageIndex;
            ViewData["PageSize"] = PageSize;
            return Page();
        }

        void GetData(string userId)
        {
            var commonLogic = new CommonLogic();
            var userLogic = new UserLogic();
            BaseViewModel = new();
            ChatViewModel = new();

            User = commonLogic.GetUser(Convert.ToInt32(userId));
            BaseViewModel.userTimeRemain = commonLogic.ConvertTimeRemain(User.Cash.HasValue ? User.Cash.Value : 0, Convert.ToInt32(HttpContext.Session.GetString("cash")));
            ChatViewModel = userLogic.GetUserMessage(Convert.ToInt32(userId));           
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
