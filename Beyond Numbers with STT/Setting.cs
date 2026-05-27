using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;

namespace Beyond_Numbers_with_STT
{
    [FileLocation("ModsSettings/Beyond_Numbers/Beyond_Numbers")]
    [SettingsUIGroupOrder(kVisibilityGroup, kTrendGroup, kTooltipGroup)]
    [SettingsUIShowGroupName(kVisibilityGroup, kTrendGroup, kTooltipGroup)]
    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kVisibilityGroup = "Visibility";
        public const string kTrendGroup = "Trends";
        public const string kTooltipGroup = "MoneyTooltip";

        public Setting(IMod mod) : base(mod)
        {
            SetDefaults();
        }

        public override void SetDefaults()
        {
            HidePopulation = false;
            HideDemand = false;
            HideDate = false;
            HideTime = false;

            ShowMoneyTrendHourly = true;
            ShowMoneyTrendMonthly = false;
            ShowPopTrendHourly = true;
            ShowPopTrendMonthly = false;

            EnableMoneyTooltip = true;
            ShowTooltipIncome = true;
            ShowTooltipExpense = true;
            ShowTooltipNet = true;
            ShowTooltipHourlyValues = true;
            ShowTooltipMonthlyValues = true;
        }

        // ----- Visibility -----
        [SettingsUISection(kSection, kVisibilityGroup)]
        public bool HidePopulation
        {
            get => m_HidePopulation;
            set { m_HidePopulation = value; Mod.PushBindings(); }
        }
        private bool m_HidePopulation;

        [SettingsUISection(kSection, kVisibilityGroup)]
        public bool HideDemand
        {
            get => m_HideDemand;
            set { m_HideDemand = value; Mod.PushBindings(); }
        }
        private bool m_HideDemand;

        [SettingsUISection(kSection, kVisibilityGroup)]
        public bool HideDate
        {
            get => m_HideDate;
            set { m_HideDate = value; Mod.PushBindings(); }
        }
        private bool m_HideDate;

        [SettingsUISection(kSection, kVisibilityGroup)]
        public bool HideTime
        {
            get => m_HideTime;
            set { m_HideTime = value; Mod.PushBindings(); }
        }
        private bool m_HideTime;

        // ----- Trends -----
        [SettingsUISection(kSection, kTrendGroup)]
        public bool ShowMoneyTrendHourly
        {
            get => m_ShowMoneyTrendHourly;
            set { m_ShowMoneyTrendHourly = value; Mod.PushBindings(); }
        }
        private bool m_ShowMoneyTrendHourly;

        [SettingsUISection(kSection, kTrendGroup)]
        public bool ShowMoneyTrendMonthly
        {
            get => m_ShowMoneyTrendMonthly;
            set { m_ShowMoneyTrendMonthly = value; Mod.PushBindings(); }
        }
        private bool m_ShowMoneyTrendMonthly;

        [SettingsUISection(kSection, kTrendGroup)]
        public bool ShowPopTrendHourly
        {
            get => m_ShowPopTrendHourly;
            set { m_ShowPopTrendHourly = value; Mod.PushBindings(); }
        }
        private bool m_ShowPopTrendHourly;

        [SettingsUISection(kSection, kTrendGroup)]
        public bool ShowPopTrendMonthly
        {
            get => m_ShowPopTrendMonthly;
            set { m_ShowPopTrendMonthly = value; Mod.PushBindings(); }
        }
        private bool m_ShowPopTrendMonthly;

        // ----- Tooltip -----
        [SettingsUISection(kSection, kTooltipGroup)]
        public bool EnableMoneyTooltip
        {
            get => m_EnableMoneyTooltip;
            set { m_EnableMoneyTooltip = value; Mod.PushBindings(); }
        }
        private bool m_EnableMoneyTooltip;

        [SettingsUISection(kSection, kTooltipGroup)]
        public bool ShowTooltipIncome
        {
            get => m_ShowTooltipIncome;
            set { m_ShowTooltipIncome = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipIncome;

        [SettingsUISection(kSection, kTooltipGroup)]
        public bool ShowTooltipExpense
        {
            get => m_ShowTooltipExpense;
            set { m_ShowTooltipExpense = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipExpense;

        [SettingsUISection(kSection, kTooltipGroup)]
        public bool ShowTooltipNet
        {
            get => m_ShowTooltipNet;
            set { m_ShowTooltipNet = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipNet;

        [SettingsUISection(kSection, kTooltipGroup)]
        public bool ShowTooltipHourlyValues
        {
            get => m_ShowTooltipHourlyValues;
            set { m_ShowTooltipHourlyValues = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipHourlyValues;

        [SettingsUISection(kSection, kTooltipGroup)]
        public bool ShowTooltipMonthlyValues
        {
            get => m_ShowTooltipMonthlyValues;
            set { m_ShowTooltipMonthlyValues = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipMonthlyValues;
    }
}
