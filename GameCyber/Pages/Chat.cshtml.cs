using GameCyber.Entities;
using GameCyber.Logics;
using GameCyber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameCyber.Pages
{
    public class ChatModel : PageModel
    {
        public User User { get; set; }
        public ChatViewModel ChatViewModel { get; set; }
        public IActionResult OnGet()
        {
            var commonLogic = new CommonLogic();
            var adminLogic = new AdminLogic();
            ChatViewModel = new();
            string userId = HttpContext.Session.GetString("userId");
            if (userId == null) return new RedirectToPageResult("Index");
            User = commonLogic.GetUser(Convert.ToInt32(userId));
            ChatViewModel = new();
            ChatViewModel.usersChats = adminLogic.GetUserChatModel();
            adminLogic.GetAdminMessage(ChatViewModel);
            return Page();
        }
        public IActionResult OnGetLoadChat(int userReceiveId)
        {
            var commonLogic = new CommonLogic();
            var adminLogic = new AdminLogic();
            ChatViewModel = new();
            string userId = HttpContext.Session.GetString("userId");
            if (userId == null) return new RedirectToPageResult("Index");
            User = commonLogic.GetUser(Convert.ToInt32(userId));
            ChatViewModel = new();
            ChatViewModel.usersChats = adminLogic.GetUserChatModel();
            adminLogic.GetAdminMessage(ChatViewModel, userReceiveId);
            return Page();
        }
    }
}
