namespace GameCyber.Models
{
    public class ChatViewModel
    {
        public int UserSentId { get; set; }
        public int UserRecievedId { get; set; }
        public string UserSentName { get; set; }
        public string UserRecievedName { get; set; }
        public List<UserChatModel> usersChats { get; set; }
        public List<MessageModel> messages { get; set; }
    }
    public class MessageModel
    {
        public int Id { get; set; }
        public int UserSentId { get; set; }
        public int UserRecievedId { get; set; }
        public string Message { get; set; }
        public DateTime? SentDate { get; set; }
    }
    public class UserChatModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string LastMessage { get; set; }
        public DateTime? SentDate { get; set; }
    }
}
