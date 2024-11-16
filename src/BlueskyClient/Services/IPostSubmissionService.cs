using Bluesky.NET.Models;
using System;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public interface IPostSubmissionService
{
    event EventHandler? NewPostSubmitted;
    event EventHandler? LikeSubmitted;

    Task<bool> LikeAsync(string targetUri, string targetCid);
    Task<string?> SubmitPostAsync(string text);
}