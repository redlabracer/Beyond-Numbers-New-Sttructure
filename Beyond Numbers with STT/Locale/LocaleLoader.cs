using Colossal.Json;
using Game.SceneFlow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Beyond_Numbers_with_STT
{
    /// <summary>
    /// Loads every translation embedded from the <c>lang</c> folder
    /// (resource name <c>BeyondNumbersWithSTT.l10n.&lt;locale&gt;.json</c>) and registers it with the game.
    /// </summary>
    public static class LocaleLoader
    {
        private const string kResourcePrefix = "BeyondNumbersWithSTT.l10n.";
        private const string kResourceSuffix = ".json";
        private const string kFallbackLocale = "en-US";

        public static void Register(Setting setting)
        {
            var localizationManager = GameManager.instance.localizationManager;
            var assembly = Assembly.GetExecutingAssembly();
            var localeIds = new List<string>();

            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                if (resourceName.StartsWith(kResourcePrefix, StringComparison.Ordinal) &&
                    resourceName.EndsWith(kResourceSuffix, StringComparison.OrdinalIgnoreCase))
                {
                    localeIds.Add(resourceName.Substring(kResourcePrefix.Length, resourceName.Length - kResourcePrefix.Length - kResourceSuffix.Length));
                }
            }

            // English is the source language and the game's fallback, so register it first.
            localeIds.Sort((a, b) => a == kFallbackLocale ? -1 : b == kFallbackLocale ? 1 : string.CompareOrdinal(a, b));

            foreach (var localeId in localeIds)
            {
                if (!localizationManager.SupportsLocale(localeId))
                {
                    Mod.log.Warn($"Skipping translation '{localeId}': the game does not ship this locale.");
                    continue;
                }

                try
                {
                    var entries = ReadResource(assembly, kResourcePrefix + localeId + kResourceSuffix);
                    var source = new JsonLocaleSource(localeId, entries, setting);
                    localizationManager.AddSource(localeId, source);
                    Mod.log.Info($"Loaded translation '{localeId}' ({source.Count} entries).");
                }
                catch (Exception ex)
                {
                    Mod.log.Error(ex, $"Failed to load translation '{localeId}'.");
                }
            }
        }

        private static Dictionary<string, string> ReadResource(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");
                }

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return JSON.Load(reader.ReadToEnd()).Make<Dictionary<string, string>>();
                }
            }
        }
    }
}
