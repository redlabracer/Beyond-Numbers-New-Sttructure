using Colossal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Beyond_Numbers_with_STT
{
    /// <summary>
    /// Dictionary source backed by one translation file from the <c>lang</c> folder.
    /// Option keys are written in a packed, translator-friendly form (e.g. <c>Options.OPTION:HideDate</c>)
    /// and expanded here through the settings object, so translators never see the generated page id.
    /// </summary>
    public class JsonLocaleSource : IDictionarySource
    {
        private const string kSectionKey = "Options.SECTION";
        private const string kTabPrefix = "Options.TAB:";
        private const string kGroupPrefix = "Options.GROUP:";
        private const string kOptionPrefix = "Options.OPTION:";
        private const string kOptionDescPrefix = "Options.OPTION_DESCRIPTION:";
        private const string kEnumPrefix = "Options.ENUM:";
        private const string kPackedPrefix = "Options.";

        private static readonly MethodInfo s_GetEnumValueLocaleID =
            typeof(Setting).GetMethod(nameof(Setting.GetEnumValueLocaleID));

        private readonly string m_LocaleId;
        private readonly Dictionary<string, string> m_Entries;

        public JsonLocaleSource(string localeId, IDictionary<string, string> rawEntries, Setting setting)
        {
            m_LocaleId = localeId;
            m_Entries = new Dictionary<string, string>(rawEntries.Count);

            foreach (var entry in rawEntries)
            {
                if (string.IsNullOrEmpty(entry.Value))
                {
                    continue;
                }

                m_Entries[ExpandKey(entry.Key, setting)] = entry.Value;
            }
        }

        public int Count => m_Entries.Count;

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return m_Entries;
        }

        public void Unload() { }

        public override string ToString() => $"BeyondNumbers.Locale.{m_LocaleId}";

        private static string ExpandKey(string key, Setting setting)
        {
            if (key == kSectionKey)
            {
                return setting.GetSettingsLocaleID();
            }

            if (key.StartsWith(kTabPrefix, StringComparison.Ordinal))
            {
                return setting.GetOptionTabLocaleID(key.Substring(kTabPrefix.Length));
            }

            if (key.StartsWith(kGroupPrefix, StringComparison.Ordinal))
            {
                return setting.GetOptionGroupLocaleID(key.Substring(kGroupPrefix.Length));
            }

            if (key.StartsWith(kOptionDescPrefix, StringComparison.Ordinal))
            {
                return setting.GetOptionDescLocaleID(RequireProperty(key, kOptionDescPrefix));
            }

            if (key.StartsWith(kOptionPrefix, StringComparison.Ordinal))
            {
                return setting.GetOptionLabelLocaleID(RequireProperty(key, kOptionPrefix));
            }

            if (key.StartsWith(kEnumPrefix, StringComparison.Ordinal))
            {
                return ExpandEnumKey(key, setting);
            }

            // A packed "Options.*:" key that matched none of the arms above is a typo.
            // Failing here prevents it from silently overwriting an unrelated entry.
            if (key.StartsWith(kPackedPrefix, StringComparison.Ordinal) && key.IndexOf(':') >= 0)
            {
                throw new InvalidDataException($"Unknown packed locale key prefix in '{key}'.");
            }

            return key;
        }

        private static string RequireProperty(string key, string prefix)
        {
            var propertyName = key.Substring(prefix.Length);
            if (typeof(Setting).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance) == null)
            {
                throw new InvalidDataException($"Locale key '{key}' references unknown setting '{propertyName}'.");
            }

            return propertyName;
        }

        private static string ExpandEnumKey(string key, Setting setting)
        {
            var body = key.Substring(kEnumPrefix.Length);
            var dot = body.LastIndexOf('.');
            if (dot <= 0 || dot == body.Length - 1)
            {
                throw new InvalidDataException($"Locale key '{key}' must have the form '{kEnumPrefix}<EnumType>.<Value>'.");
            }

            var enumType = typeof(Setting).GetNestedType(body.Substring(0, dot), BindingFlags.Public);
            if (enumType == null || !enumType.IsEnum)
            {
                throw new InvalidDataException($"Locale key '{key}' references unknown enum '{body.Substring(0, dot)}'.");
            }

            var valueName = body.Substring(dot + 1);
            if (!Enum.IsDefined(enumType, valueName))
            {
                throw new InvalidDataException($"Locale key '{key}' references unknown value '{valueName}' of '{enumType.Name}'.");
            }

            var value = Enum.Parse(enumType, valueName);
            return (string)s_GetEnumValueLocaleID.MakeGenericMethod(enumType).Invoke(setting, new[] { value });
        }
    }
}
