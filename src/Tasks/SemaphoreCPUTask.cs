// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Build.Utilities;
using System;
using System.Threading;

namespace Microsoft.Build.Tasks
{
    class SemaphoreCPUTask : Task
    {
        public override bool Execute()
        {
            int initial = BuildEngine7.RequestCores(3123890);
            Log.LogMessageFromText($"Got {initial} cores from {System.Diagnostics.Process.GetCurrentProcess().Id}", Framework.MessageImportance.High);

            if (initial > 0)
            {
                while (initial > 0)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    BuildEngine7.ReleaseCores(1);
                    initial--;
                    Log.LogMessageFromText($"Released 1 core from {System.Diagnostics.Process.GetCurrentProcess().Id}", Framework.MessageImportance.High);
                }

                return !Log.HasLoggedErrors;
            }

            BuildEngine7.ReleaseCores(50);
            Log.LogMessageFromText($"Released ~50 cores  from {System.Diagnostics.Process.GetCurrentProcess().Id}", Framework.MessageImportance.High);

            Log.LogMessageFromText($"Got {BuildEngine7.RequestCores(10)} cores  from {System.Diagnostics.Process.GetCurrentProcess().Id}", Framework.MessageImportance.High);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Log.LogMessageFromText($"Got {BuildEngine7.RequestCores(30)} cores  from {System.Diagnostics.Process.GetCurrentProcess().Id}", Framework.MessageImportance.High);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            BuildEngine7.ReleaseCores(2);
            Log.LogMessageFromText($"Released some number of cores from {System.Diagnostics.Process.GetCurrentProcess().Id}", Framework.MessageImportance.High);

            Log.LogMessageFromText($"Got {BuildEngine7.RequestCores(12)} cores  from {System.Diagnostics.Process.GetCurrentProcess().Id}", Framework.MessageImportance.High);

            return !Log.HasLoggedErrors;
        }


    }
}
