// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if NETCOREAPP

using System;
using System.Runtime.InteropServices;
using NuGet.Frameworks;

namespace Microsoft.NET.TestFramework
{
    public static class EnvironmentInfo
    {
        public static string ExecutableExtension => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : string.Empty;

        public static string GetCompatibleRid(string targetFramework = null) => RuntimeInformation.RuntimeIdentifier;

        public static bool SupportsTargetFramework(string targetFramework) => true;
    }
}

#endif
