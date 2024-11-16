using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bluesky.NET.Models;

[JsonSerializable(typeof(AuthResponse))]
[JsonSerializable(typeof(AuthRequestBody))]
[JsonSerializable(typeof(FeedItem[]), GenerationMode = JsonSourceGenerationMode.Metadata)] // Only used to deserialize
[JsonSerializable(typeof(FeedPost[]), GenerationMode = JsonSourceGenerationMode.Metadata)] // Only used to deserialize
[JsonSerializable(typeof(FeedRecord[]), GenerationMode = JsonSourceGenerationMode.Metadata)] // Only used to deserialize
[JsonSerializable(typeof(Notification[]), GenerationMode = JsonSourceGenerationMode.Metadata)] // Only used to deserialize
[JsonSerializable(typeof(FeedResponse), GenerationMode = JsonSourceGenerationMode.Metadata)] // Only used to deserialize
[JsonSerializable(typeof(Author))]
[JsonSerializable(typeof(Notification))]
[JsonSerializable(typeof(CreateRecordBody))]
[JsonSerializable(typeof(CreateRecordResponse))]
public sealed partial class ModelSerializerContext : JsonSerializerContext
{
    /// <summary>
    /// The lazily initialized backing field for the context to be used for case insensitive serialization (<see cref="CaseInsensitive"/>).
    /// </summary>
    private static ModelSerializerContext? _caseInsensitive;

    /// <summary>
    /// A case insensitive variant of <see cref="Default"/>.
    /// </summary>
    public static ModelSerializerContext CaseInsensitive => _caseInsensitive ??= new ModelSerializerContext(new JsonSerializerOptions(s_defaultOptions) 
    { 
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    });
}
