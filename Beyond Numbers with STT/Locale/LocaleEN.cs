using Colossal;
using System.Collections.Generic;

namespace Beyond_Numbers_with_STT
{
    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Beyond Numbers" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Main" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kVisibilityGroup), "Visibility Settings" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kTrendGroup), "Money & Population Trends" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kTooltipGroup), "Detailed Money Tooltip" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HidePopulation)), "Hide Population" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HidePopulation)), "Hides the population number until hovered." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HideDemand)), "Hide Demand Bars" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HideDemand)), "Hides the city demand until hovered." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HideDate)), "Hide Date" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HideDate)), "Hides the in-game date / year until hovered." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HideTime)), "Hide Time" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HideTime)), "Hides the in-game clock time until hovered." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowMoneyTrendHourly)), "Show Money Trend (Hourly)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowMoneyTrendHourly)), "Displays the hourly money change next to the cash value." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowMoneyTrendMonthly)), "Show Money Trend (Monthly)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowMoneyTrendMonthly)), "Displays the monthly money change next to the cash value." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowPopTrendHourly)), "Show Population Trend (Hourly)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowPopTrendHourly)), "Displays the hourly population change next to the population value." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowPopTrendMonthly)), "Show Population Trend (Monthly)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowPopTrendMonthly)), "Displays the monthly population change next to the population value." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableMoneyTooltip)), "Enable Detailed Money Tooltip" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableMoneyTooltip)), "Shows a detailed breakdown when hovering the money value." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipIncome)), "Tooltip: Show Income" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipIncome)), "Includes total income in the money tooltip." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipExpense)), "Tooltip: Show Expense" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipExpense)), "Includes total expense in the money tooltip." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipNet)), "Tooltip: Show Net" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipNet)), "Includes net (income minus expense) in the money tooltip." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipHourlyValues)), "Tooltip: Show Hourly Values" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipHourlyValues)), "Displays values per hour in the tooltip." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipMonthlyValues)), "Tooltip: Show Monthly Values" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipMonthlyValues)), "Displays values per month in the tooltip." },
            };
        }

        public void Unload() { }
    }
}
