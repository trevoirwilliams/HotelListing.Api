using System;
using System.Collections.Generic;
using System.Text;

namespace HotelListing.Api.Common.Constants;

public static class CacheConstants
{
    // Output Cache Policy Names
    public const string AuthenticatedUserCachingPolicy = "AuthenticatedUserCachingPolicy";

    public const string AuthenticatedUserCachingPolicyTag = "authpolicy-";

    // Cache Durations (in seconds)
    public const int ShortDuration = 60; // 1 minute
    public const int MediumDuration = 300; // 5 minutes
    public const int LongDuration = 900; // 15 minutes
}