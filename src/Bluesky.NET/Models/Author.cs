using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesky.NET.Models;

public class Author
{
    public required string Did { get; init; }

    public required string Handle { get; init; }

    public required string DisplayName { get; init; }

    public required string Avatar { get; init; }
}
