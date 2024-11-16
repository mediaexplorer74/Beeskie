using Bluesky.NET.Models;
using System;
using System.Threading.Tasks;

namespace BlueskyClient.Services;

public interface IPostSubmissionService
{
    event EventHandler? NewPostSubmitted;

    Task<string?> SubmitPostAsync(string text);
}