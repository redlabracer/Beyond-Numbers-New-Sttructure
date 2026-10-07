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

        private static bool s_Detected;

        internal static bool IsCityWatchdogInstalled()
        {
            if (s_Detected)
            {
                return true;
            }

            try
            {
                s_Detected = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Any(IsCityWatchdogAssembly);

                return s_Detected;
            }
            catch
            {
                return false;
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
