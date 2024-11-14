using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesky.NET.Models;

public class FeedRecord
{
    public DateTime CreatedAt { get; init; }

    public required string Text { get; init; }
}
