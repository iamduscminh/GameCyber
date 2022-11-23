using System;
using System.Collections.Generic;

namespace GameCyber.Entities;

public partial class Chat
{
    public int Id { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? SentDate { get; set; }

    public virtual ICollection<UserChat> UserChats { get; } = new List<UserChat>();
}
