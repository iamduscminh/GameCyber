using GameCyber.Entities;
using GameCyber.Logics;
using GameCyber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameCyber.Pages
{
    public class DashboardModel : PageModel
    {
        public User User { get; set; }
        public UserViewModel userViewModel { get; set; }
        public IActionResult OnGet()
        {
            var commonLogic = new CommonLogic();
            var userLogic = new UserLogic();
            userViewModel = new UserViewModel();
            string userId = HttpContext.Session.GetString("userId");
            if (userId == null) return new RedirectToPageResult("Index");
            User = commonLogic.GetUser(Convert.ToInt32(userId));

            userViewModel.ListUsers = userLogic.GetUsers();

            return Page();
        }
        public void OnGetDelete(int id)
        {
            var userLogic = new UserLogic();
            userViewModel = new UserViewModel();
            userLogic.DeleteAccount(id);
            GetData(userViewModel);
        }
        public void OnGetUpdate(int id)
        {
            var userLogic = new UserLogic();
            userViewModel = new UserViewModel();
            userViewModel.UserEdit = new CommonLogic().GetUser(id);
            GetData(userViewModel);
        }
        public IActionResult OnPostCreateUser()
        {
            var userLogic = new UserLogic();
            userViewModel = new UserViewModel();
            string username = Request.Form["adUName"].ToString().Trim();
            if (userLogic.CheckUser(username))
            {
                ViewData["RegisterMsg"] = "username da ton tai!";
                GetData(userViewModel);
                return Page();
            }

            string password = Request.Form["adPass"].ToString().Trim();
            string fullname = Request.Form["adFName"].ToString().Trim();
            string cash = Request.Form["adCash"].ToString().Trim();

            User user = new User();
            user.Username = username;
            user.Password = password;
            user.Fullname = fullname;
            user.Role = "1";
            var cashCheck = int.TryParse(cash, out int cashValue);
            user.Cash = cashCheck ? cashValue : 0;

            userLogic.CreateAccount(user);
            GetData(userViewModel);
            return Page();
        }
        public IActionResult OnPostEditUser()
        {
            var userLogic = new UserLogic();
            userViewModel = new UserViewModel();
            string password = Request.Form["edPassword"].ToString().Trim();
            string cash = Request.Form["edCash"].ToString().Trim();
            string id = Request.Form["edId"].ToString().Trim();
            if (int.TryParse(cash, out int cashValue) && cashValue < 0)
            {
                ViewData["EditMsg"] = "Cash la so va phai lon hon 0!";
                userViewModel.UserEdit = new User();
                GetData(userViewModel);
                return Page();
            }
            userViewModel.UserEdit = new CommonLogic().GetUser(Convert.ToInt32(id));

            User user = new User();
            user.Id = Convert.ToInt32(id);
            user.Password = password;
            user.Cash = cashValue;

            userLogic.UpdateAccount(user);
            userViewModel.UserEdit = null;
            GetData(userViewModel);
            return Page();
        }
        public void OnPostSearchUser()
        {
            userViewModel = new UserViewModel();
            string userSearch = Request.Form["adUserSearch"].ToString().Trim();
            if (userSearch.Length == 0) GetData(userViewModel);
            else userViewModel.ListUsers = new UserLogic().SearchUsers(userSearch);
        }
        public void GetData(UserViewModel userViewModel)
        {
            userViewModel.ListUsers = new UserLogic().GetUsers();
            string userId = HttpContext.Session.GetString("userId");
            User = new CommonLogic().GetUser(Convert.ToInt32(userId));
        }
    }
}
