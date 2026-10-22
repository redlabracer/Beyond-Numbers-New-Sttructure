// File: Compatibility/CwdCompatibility.cs
// Purpose: Detect City Watchdog mod without adding a compile-time dependency.

namespace Beyond_Numbers_with_STT.Compatibility
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal static class CwdCompatibility
    {
        private const string CityWatchdogAssemblyName = "CityWatchdog";
        private const string CityWatchdogModTypeName = "CityWatchdog.Mod";

        private static readonly object s_InitLock = new object();
        private static volatile bool s_Initialized;
        private static volatile bool s_Detected;

        // Called from hot paths (settings getters, UI update), so after the one-time
        // scan this must stay a plain field read. Assemblies loaded later are picked
        // up via the AssemblyLoad event instead of rescanning the whole AppDomain.
        internal static bool IsCityWatchdogInstalled()
        {
            if (!s_Initialized)
            {
                Initialize();
            }

            return s_Detected;
        }

        private static void Initialize()
        {
            lock (s_InitLock)
            {
                if (s_Initialized)
                {
                    return;
                }

                try
                {
                    AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
                }
                catch
                {
                }

                try
                {
                    if (AppDomain.CurrentDomain.GetAssemblies().Any(IsCityWatchdogAssembly))
                    {
                        s_Detected = true;
                    }
                }
                catch
                {
                }

                s_Initialized = true;
            }
        }

        private static void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            if (s_Detected || args?.LoadedAssembly == null)
            {
                return;
            }

            if (IsCityWatchdogAssembly(args.LoadedAssembly))
            {
                s_Detected = true;
            }
        }

        internal static void Shutdown()
        {
            lock (s_InitLock)
            {
                try
                {
                    AppDomain.CurrentDomain.AssemblyLoad -= OnAssemblyLoad;
                }
                catch
                {
                }

                s_Initialized = false;
                s_Detected = false;
            }
        }

        private static bool IsCityWatchdogAssembly(Assembly assembly)
        {
            try
            {
                string assemblyName = assembly.GetName().Name ?? string.Empty;

                if (string.Equals(assemblyName, CityWatchdogAssemblyName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                return assembly.GetType(CityWatchdogModTypeName, throwOnError: false) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
