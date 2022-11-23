using System;
using System.Collections.Generic;

namespace GameCyber.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int? Cash { get; set; }

    public string? Connection { get; set; }

    public virtual ICollection<UserChat> UserChatUserRecieveds { get; } = new List<UserChat>();

    public virtual ICollection<UserChat> UserChatUserSents { get; } = new List<UserChat>();
}
