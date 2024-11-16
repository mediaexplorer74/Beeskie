using Bluesky.NET.Constants;
using Bluesky.NET.Models;
using System;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public interface IPostSubmissionService
{
    event EventHandler<(SubmissionRecord, CreateRecordResponse)>? RecordCreated;

    Task<bool> LikeOrRepostAsync(RecordType recordType, string targetUri, string targetCid);
    
    Task<string?> SubmitPostAsync(string text);
}