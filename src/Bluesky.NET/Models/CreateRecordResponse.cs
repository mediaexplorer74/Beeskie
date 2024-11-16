using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesky.NET.Models;

public class CreateRecordResponse
{
    public required string Uri { get; init; }

    public required string Cid { get; init; }

    public string? ValidationStatus { get; init; }
}
