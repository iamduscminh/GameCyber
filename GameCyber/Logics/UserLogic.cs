using GameCyber.Entities;
using GameCyber.Models;

namespace GameCyber.Logics
{
    public class UserLogic
    {
        public int UpdateCashUser(int userId, int unit)
        {
            int curCash;
            using (var context = new GameProjectContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                if (user == null || !user.Cash.HasValue) return 0;

                user.Cash -= (unit / 60);
                if (user.Cash < 0) user.Cash = 0;
                curCash = user.Cash.Value;
                context.SaveChanges();
            }
            return curCash;
        }
        public List<User> GetUsers()
        {
            using (var context = new GameProjectContext())
            {
                return context.Users.Where(x => x.Role == "1").ToList();
            }
        }
        public List<User> SearchUsers(string txt)
        {
            using (var context = new GameProjectContext())
            {
                return context.Users.Where(x => x.Username.Contains(txt) && x.Role == "1").ToList();
            }
        }
        public void CreateAccount(User user)
        {
            using (var context = new GameProjectContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public void UpdateAccount(User user)
        {
            using (var context = new GameProjectContext())
            {
                var userEntity = context.Users.FirstOrDefault(x => x.Id == user.Id);
                userEntity.Password = user.Password;
                userEntity.Cash += user.Cash;
                context.SaveChanges();
            }
        }
        public void DeleteAccount(int Id)
        {
            using (var context = new GameProjectContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == Id);
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
        public bool CheckUser(string username)
        {
            using (var context = new GameProjectContext())
            {
                return context.Users.FirstOrDefault(x => x.Username == username) != null;
            }
        }
        public ChatViewModel GetUserMessage(int userSentId)
        {
            var chatModel = new ChatViewModel();
            using (var context = new GameProjectContext())
            {
                context.Chats.ToList();
                var userReceiveId = context.Users.FirstOrDefault(x => x.Role == "0").Id;
                chatModel.UserRecievedId = userReceiveId;
                chatModel.UserSentId = userSentId;
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

                chatModel.messages = messageModels.ToList();
            }
            return chatModel;
        }

        public List<Game> GetGames()
        {
            using (var context = new GameProjectContext())
            {
                return context.Games.ToList();
            }
        }
        public List<Game> GetNewGames()
        {
            using (var context = new GameProjectContext())
            {
                return context.Games.OrderByDescending(x=>x.Upload).Take(6).ToList();
            }
        }
        public List<Game> GetTopGames()
        {
            using (var context = new GameProjectContext())
            {
                return context.Games.OrderByDescending(x => x.Rating).Take(7).ToList();
            }
        }

        public List<Game> GetRelatedGames(int id)
        {
            using (var context = new GameProjectContext())
            {
                return context.Games.Where(g=>g.Id != id).OrderByDescending(x => x.Upload).Take(3).ToList();
            }
        }
        public List<Game> GetRelatedDetailGames(int id)
        {
            using (var context = new GameProjectContext())
            {
                return context.Games.Where(g => g.Id != id).OrderByDescending(x => x.Upload).Take(2).ToList();
            }
        }

        public List<Game> GetGames(int Offset, int Size)
        {
            using (var context = new GameProjectContext())
            {
                return context.Games.Skip(Offset - 1).Take(Size).ToList();
            }
        }

        public List<Game> GetGamesSearch(string txt)
        {
            using (var context = new GameProjectContext())
            {
                if(txt=="") return context.Games.ToList();
                return context.Games.Where(x=>x.Name.Contains(txt)).ToList();
            }
        }

        public List<Game> GetGamesSearch(int Offset, int Size, string txt)
        {
            using (var context = new GameProjectContext())
            {   if(txt=="") return context.Games.Skip(Offset - 1).Take(Size).ToList();
                return context.Games.Where(x => x.Name.Contains(txt)).Skip(Offset - 1).Take(Size).ToList();
            }
        }

        public List<Game> SearchGames(string txt)
        {
            using (var context = new GameProjectContext())
            {
                return context.Games.Where(x=>x.Name.Contains(txt)).ToList();
            }
        }
        public Game GetGame(int Id) 
        {
            using (var context = new GameProjectContext())
            {
                var game = context.Games.FirstOrDefault(x => x.Id == Id);
                game.Rating += 1;
                context.SaveChanges();
                return game;
            }
        }


        public void CreateGame(Game game)
        {
            using (var context = new GameProjectContext())
            {
                context.Games.Add(game);
                context.SaveChanges();
            }
        }
        public void UpdateGame(Game game)
        {
            using (var context = new GameProjectContext())
            {
                var entity = context.Games.FirstOrDefault(x => x.Id == game.Id);
                entity.Title = game.Title;
                entity.Name = game.Name;
                entity.Url = game.Url;
                entity.Description = game.Description;
                entity.Author = game.Author;
                entity.ImageUrl = game.ImageUrl;

                context.SaveChanges();
            }
        }
        public void DeleteGame(int Id)
        {
            using (var context = new GameProjectContext())
            {
                var game = context.Games.FirstOrDefault(x => x.Id == Id);
                context.Games.Remove(game);
                context.SaveChanges();
            }
        }
    }
}
