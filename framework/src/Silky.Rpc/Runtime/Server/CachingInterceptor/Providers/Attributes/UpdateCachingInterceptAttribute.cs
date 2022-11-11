﻿using System;
using JetBrains.Annotations;
using Silky.Core;

namespace Silky.Rpc.Runtime.Server;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class UpdateCachingInterceptAttribute : Attribute, IUpdateCachingInterceptProvider
{
    public UpdateCachingInterceptAttribute([NotNull] string keyTemplate)
    {
        Check.NotNullOrEmpty(keyTemplate, nameof(keyTemplate));
        KeyTemplate = keyTemplate;
        OnlyCurrentUserData = false;
        IgnoreMultiTenancy = false;
        CachingMethod = CachingMethod.Remove;
    }

    public string KeyTemplate { get; }

    public bool OnlyCurrentUserData { get; set; }
    public bool IgnoreMultiTenancy { get; set; }
    public CachingMethod CachingMethod { get; }
}