using GameCyber.Entities;
using GameCyber.Logics;
using GameCyber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameCyber.Pages
{
    public class GameManagerModel : PageModel
    {
        public User User { get; set; }
        public GameViewModel gameViewModel { get; set; }
        public IActionResult OnGet()
        {
            var commonLogic = new CommonLogic();
            var userLogic = new UserLogic();
            gameViewModel = new GameViewModel();
            string userId = HttpContext.Session.GetString("userId");
            if (userId == null) return new RedirectToPageResult("Index");
            User = commonLogic.GetUser(Convert.ToInt32(userId));

            gameViewModel.ListGames= userLogic.GetGames();

            return Page();
        }
        public void OnGetDelete(int id)
        {
            var userLogic = new UserLogic();
            gameViewModel = new ();
            userLogic.DeleteGame(id);
            GetData(gameViewModel);
        }
        public void OnGetUpdate(int id)
        {
            var userLogic = new UserLogic();
            gameViewModel = new ();
            gameViewModel.GameEdit = new UserLogic().GetGame(id);
            GetData(gameViewModel);
        }
        public IActionResult OnPostCreateGame()
        {
            var userLogic = new UserLogic();
            gameViewModel = new ();
            string name = Request.Form["adGName"].ToString().Trim();
            string title = Request.Form["adGTitle"].ToString().Trim();
            string url = Request.Form["adGUrl"].ToString().Trim();
            string description = Request.Form["adGDes"].ToString().Trim();
            string author = Request.Form["adGAuthor"].ToString().Trim();
            string imgUrl = Request.Form["adGImg"].ToString().Trim();

            Game game = new();
            game.Name = name;
            game.Title = title;
            game.Description = description;
            game.Author = author;
            game.Rating = 1;
            game.Upload = DateTime.Now;
            game.Url = url;
            game.ImageUrl = imgUrl;

            userLogic.CreateGame(game);
            GetData(gameViewModel);
            return Page();
        }
        public IActionResult OnPostEditGame()
        {
            var userLogic = new UserLogic();
            gameViewModel = new ();
            string id = Request.Form["edId"].ToString().Trim();
            string name = Request.Form["edName"].ToString().Trim();
            string title = Request.Form["edTitle"].ToString().Trim();
            string url = Request.Form["edUrl"].ToString().Trim();
            string description = Request.Form["edDescription"].ToString().Trim();
            string author = Request.Form["edAuthor"].ToString().Trim();
            string imgUrl = Request.Form["edImageUrl"].ToString().Trim();

            Game game = new();
            game.Id = Convert.ToInt32(id);
            game.Name = name;
            game.Title = title;
            game.Description = description;
            game.Author = author;
            game.Url = url;
            game.ImageUrl = imgUrl;

            userLogic.UpdateGame(game);
            gameViewModel.GameEdit = null;
            GetData(gameViewModel);
            return Page();
        }
        public void OnPostSearchGame()
        {
            gameViewModel = new ();
            string userSearch = Request.Form["adGameSearch"].ToString().Trim();
            if (userSearch.Length == 0) GetData(gameViewModel);
            else gameViewModel.ListGames = new UserLogic().SearchGames(userSearch);
        }
        public void GetData(GameViewModel gameViewModel)
        {
            gameViewModel.ListGames = new UserLogic().GetGames();
            string userId = HttpContext.Session.GetString("userId");
            User = new CommonLogic().GetUser(Convert.ToInt32(userId));
        }
    }
}
