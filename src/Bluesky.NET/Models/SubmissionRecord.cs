using System;

namespace Bluesky.NET.Models;

public class SubmissionRecord
{
    public DateTime CreatedAt { get; init; }

    public string Text { get; init; } = string.Empty;

    public RecordSubject? Subject { get; init; }

    public ReplyRecord? Reply { get; init; }
}
