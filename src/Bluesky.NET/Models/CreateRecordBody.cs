using System;
using System.Collections.Generic;
using System.Text;

namespace Bluesky.NET.Models;

public class CreateRecordBody
{
    public required string Repo { get; init; }

    public required string Collection { get; init; }

    public required SubmissionRecord Record { get; init; }
}
