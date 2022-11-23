using System;
using System.Collections.Generic;

namespace GameCyber.Entities;

public partial class Game
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Upload { get; set; }

    public int? Rating { get; set; }

    public string? Author { get; set; }

    public string? ImageUrl { get; set; }
}
