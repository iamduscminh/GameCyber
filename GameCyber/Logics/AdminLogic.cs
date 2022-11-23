using GameCyber.Entities;
using GameCyber.Models;

namespace GameCyber.Logics
{
    public class AdminLogic
    {
        public List<UserChatModel> GetUserChatModel()
        {
            var listUserChatResult = new List<UserChatModel>();

            using (var context = new GameProjectContext())
            {
                var listUserChatFilter = from user in context.Users
                                         join user_chat in context.UserChats on user.Id equals user_chat.UserSentId
                                         join chat in context.Chats on user_chat.ChatId equals chat.Id
                                         where user.Role != "0"
                                         select new UserChatModel()
                                         {
                                             Id = user.Id,
                                             FullName = user.Fullname,
                                             LastMessage = chat.Message,
                                             SentDate = chat.SentDate
                                         };
                var listUserChatGroup = (from t in listUserChatFilter
                                         group t by new { t.Id, t.FullName }
                           into grp
                                         select new UserChatModel()
                                         {
                                             Id = grp.Key.Id,
                                             FullName = grp.Key.FullName,
                                             SentDate = grp.Max(t => t.SentDate)
                                         });

                listUserChatResult = (from l in listUserChatGroup
                                      join l1 in listUserChatFilter on l.SentDate equals l1.SentDate
                                      select new UserChatModel()
                                      {
                                          Id = l.Id,
                                          FullName = l1.FullName,
                                          LastMessage = l1.LastMessage,
                                          SentDate = l.SentDate
                                      }
                         ).OrderByDescending(x => x.SentDate).ToList();
            }
            return listUserChatResult;
        }

        public void GetAdminMessage(ChatViewModel chatViewModel, int userId = 0)
        {

            using (var context = new GameProjectContext())
            {
                int userSentId;
                UserChatModel userSent = new();
                if (userId == 0)
                {
                    userSent = chatViewModel.usersChats.FirstOrDefault();
                    userSentId = userSent.Id;
                }
                else
                {
                    userSentId = userId;
                    userSent.FullName = new CommonLogic().GetUser(userSentId).Fullname;
                }
                var userReceiveId = context.Users.FirstOrDefault(x => x.Role == "0").Id;
                chatViewModel.UserSentId = userReceiveId;
                chatViewModel.UserRecievedId = userSentId;
                chatViewModel.UserSentName = userSent.FullName;
                context.Chats.ToList();
                var messageModels = from user_chat in context.UserChats
                                    join chat in context.Chats on user_chat.ChatId equals chat.Id
                                    where (user_chat.UserSentId == userSentId && user_chat.UserRecievedId == userReceiveId)
                                    || (user_chat.UserSentId == userReceiveId && user_chat.UserRecievedId == userSentId)
                                    select new MessageModel()
                                    {
                                        UserSentId = user_chat.UserSentId,
                                        UserRecievedId = user_chat.UserRecievedId,
                                        Message = chat.Message,
                                        SentDate = chat.SentDate
                                    };
                chatViewModel.messages = messageModels.ToList();
            }
        }
    }
}
