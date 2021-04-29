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

        //  Encode relevant information from https://github.com/dotnet/core/blob/main/os-lifecycle-policy.md
        //  so that we can check if a test targeting a particular version of .NET Core should be
        //  able to run on the current OS
        public static bool SupportsTargetFramework(string targetFramework)
        {
            var nugetFramework = NuGetFramework.Parse(targetFramework);
            string currentRid = RuntimeInformation.RuntimeIdentifier;

            string ridOS = currentRid.Split('.')[0];
            if (ridOS.Equals("alpine", StringComparison.OrdinalIgnoreCase))
            {
                if (nugetFramework.Version < new Version(2, 1, 0, 0))
                {
                    return false;
                }
            }
            else if (ridOS.Equals("fedora", StringComparison.OrdinalIgnoreCase))
            {
                string restOfRid = currentRid.Substring(ridOS.Length + 1);
                string fedoraVersionString = restOfRid.Split('-')[0];
                if (int.TryParse(fedoraVersionString, out int fedoraVersion))
                {
                    else if (fedoraVersion >= 29)
                    {
                        if (nugetFramework.Version < new Version(2, 2, 0, 0))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }

            return true;
        }
    }
}

#endif
