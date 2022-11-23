using GameCyber.Entities;

namespace GameCyber.Logics
{
    public class CommonLogic
    {
        public User Login(string username, string password)
        {
            using (var context = new GameProjectContext())
            {
                return context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            }
        }

        public User GetUser(int id)
        {
            using (var context = new GameProjectContext())
            {
                return context.Users.FirstOrDefault(x => x.Id == id);
            }
        }
        public string ConvertTimeRemain(int cash, int unit)
        {
            string hour, minute, result = String.Empty;
            if (cash < unit)
            {
                minute = (((float)cash / (float)unit) * 60).ToString();
                result = $"{minute} minutes";
            }
            else
            {
                hour = (cash / unit).ToString();
                minute = (((float)(cash % unit) / (float)unit) * 60).ToString();
                result = $"{hour} hour : {minute} minute";
            }
            return result;
        }
        public void SendMessage(int userSentId, int userReceiveId, string msg)
        {
            using (var context = new GameProjectContext())
            {
                Chat sysChat = new();
                sysChat.Message = msg;
                sysChat.SentDate = DateTime.Now;

                context.Chats.Add(sysChat);
                context.SaveChanges();

                UserChat sysUserChat = new();
                sysUserChat.UserSentId = userSentId;
                sysUserChat.UserRecievedId = userReceiveId;
                sysUserChat.ChatId = sysChat.Id;

                context.UserChats.Add(sysUserChat);
                context.SaveChanges();
            }

        }
        public void SetUserConnection(int userId, string connectionId)
        {
            using (var context = new GameProjectContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                user.Connection = connectionId;
                context.SaveChanges();
            }
        }
    }
}
