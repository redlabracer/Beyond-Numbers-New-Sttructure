using Colossal;
using System.Collections.Generic;

namespace Beyond_Numbers_with_STT
{
    public class LocaleDE : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleDE(Setting setting)
        {
            m_Setting = setting;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Beyond Numbers" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Hauptmenü" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kVisibilityGroup), "Sichtbarkeit" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kTrendGroup), "Geld- & Bevölkerungs-Trends" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kTooltipGroup), "Detaillierter Geld-Tooltip" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HidePopulation)), "Bevölkerung verstecken" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HidePopulation)), "Versteckt die Einwohnerzahl, bis die Maus darauf zeigt." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HideDemand)), "Bedarfs-Balken verstecken" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HideDemand)), "Versteckt die Anzeige für Wohn- und Industriegebiet, bis die Maus darauf zeigt." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HideDate)), "Datum verstecken" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HideDate)), "Versteckt das Spieldatum / Jahr, bis die Maus darauf zeigt." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HideTime)), "Uhrzeit verstecken" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HideTime)), "Versteckt die Spieluhrzeit, bis die Maus darauf zeigt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowMoneyTrendHourly)), "Geld-Trend (stündlich) anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowMoneyTrendHourly)), "Zeigt die stündliche Geldveränderung neben dem Kontostand an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowMoneyTrendMonthly)), "Geld-Trend (monatlich) anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowMoneyTrendMonthly)), "Zeigt die monatliche Geldveränderung neben dem Kontostand an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowPopTrendHourly)), "Bevölkerungs-Trend (stündlich) anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowPopTrendHourly)), "Zeigt die stündliche Bevölkerungsveränderung neben der Einwohnerzahl an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowPopTrendMonthly)), "Bevölkerungs-Trend (monatlich) anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowPopTrendMonthly)), "Zeigt die monatliche Bevölkerungsveränderung neben der Einwohnerzahl an." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableMoneyTooltip)), "Detaillierten Geld-Tooltip aktivieren" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableMoneyTooltip)), "Zeigt eine detaillierte Aufschlüsselung beim Hover über den Geldwert." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipIncome)), "Tooltip: Einnahmen anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipIncome)), "Zeigt die Gesamteinnahmen im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipExpense)), "Tooltip: Ausgaben anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipExpense)), "Zeigt die Gesamtausgaben im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipNet)), "Tooltip: Netto anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipNet)), "Zeigt das Netto (Einnahmen minus Ausgaben) im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipHourlyValues)), "Tooltip: Stundenwerte anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipHourlyValues)), "Zeigt Werte pro Stunde im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShowTooltipMonthlyValues)), "Tooltip: Monatswerte anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShowTooltipMonthlyValues)), "Zeigt Werte pro Monat im Tooltip an." },
            };
        }

        public void Unload() { }
    }
}
