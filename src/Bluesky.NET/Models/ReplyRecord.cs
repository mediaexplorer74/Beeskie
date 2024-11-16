using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesky.NET.Models;

public class ReplyRecord
{
    public RecordSubject? Parent { get; init; }

    public RecordSubject? Root { get; init; }
}
