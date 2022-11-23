using System;
using System.Collections.Generic;

namespace GameCyber.Entities;

public partial class UserChat
{
    public int Id { get; set; }

    public int UserSentId { get; set; }

    public int UserRecievedId { get; set; }

    public int ChatId { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual User UserRecieved { get; set; } = null!;

    public virtual User UserSent { get; set; } = null!;
}
