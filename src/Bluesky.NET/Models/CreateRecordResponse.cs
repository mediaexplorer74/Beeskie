using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesky.NET.Models;

public class CreateRecordResponse
{
    public string Uri { get; init; } = string.Empty;

    public string Cid { get; init; } = string.Empty;

    public string? ValidationStatus { get; init; }
}
