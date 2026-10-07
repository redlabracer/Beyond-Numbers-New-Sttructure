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

                { m_Setting.GetOptionGroupLocaleID(Setting.kPopTooltipGroup), "Detaillierter Einwohner-Tooltip" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnablePopTooltip)), "Detaillierten Einwohner-Tooltip aktivieren" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnablePopTooltip)), "Zeigt eine detaillierte Aufschlüsselung beim Überfahren der Einwohnerzahl. Wird ausgeblendet und deaktiviert, wenn City Watchdog installiert ist." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipMonthlyTrend)), "Tooltip: Monatlichen Trend anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipMonthlyTrend)), "Zeigt den aktuellen monatlichen Einwohnertrend im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipPopulation)), "Tooltip: Einwohner anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipPopulation)), "Zeigt die Gesamteinwohnerzahl im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipBirthRate)), "Tooltip: Geburtenrate anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipBirthRate)), "Zeigt die monatliche Geburtenrate im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipDeathRate)), "Tooltip: Sterberate anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipDeathRate)), "Zeigt die monatliche Sterberate im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipMovedIn)), "Tooltip: Zugezogen anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipMovedIn)), "Zeigt die pro Monat zugezogenen Einwohner im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipMovedAway)), "Tooltip: Weggezogen anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipMovedAway)), "Zeigt die pro Monat weggezogenen Einwohner im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipJobs)), "Tooltip: Arbeitsplätze anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipJobs)), "Zeigt die Gesamtzahl der Arbeitsplätze im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipEmployed)), "Tooltip: Beschäftigte anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipEmployed)), "Zeigt die Anzahl der Beschäftigten im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipUnemployment)), "Tooltip: Arbeitslosigkeit anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipUnemployment)), "Zeigt die Arbeitslosenquote im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipHomeless)), "Tooltip: Obdachlose anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipHomeless)), "Zeigt die Anzahl der Obdachlosen im Tooltip an." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PopTooltipHomelessRate)), "Tooltip: Obdachlosenquote anzeigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PopTooltipHomelessRate)), "Zeigt die Obdachlosenquote im Tooltip an." },
            };
        }

        public void Unload() { }
    }
}
