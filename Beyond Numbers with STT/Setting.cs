namespace Beyond_Numbers_with_STT
{
    using Beyond_Numbers_with_STT.Compatibility;
    using Colossal.IO.AssetDatabase;
    using Game.Modding;
    using Game.Settings;

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
            m_HidePopulation = false;
            m_HideDemand = false;
            m_HideDate = false;
            m_HideTime = false;

            m_ShowMoneyTrendHourly = true;
            m_ShowMoneyTrendMonthly = false;
            m_ShowPopTrendHourly = true;
            m_ShowPopTrendMonthly = false;

            // Default OFF so the extra tooltip rows are opt-in.
            m_EnableMoneyTooltip = false;
            m_ShowTooltipIncome = true;
            m_ShowTooltipExpense = true;
            m_ShowTooltipNet = true;
            m_ShowTooltipHourlyValues = true;
            m_ShowTooltipMonthlyValues = true;
        }

        // ----- City Watchdog compatibility -----
        internal bool CityWatchdogDetected => CwdCompatibility.IsCityWatchdogInstalled();

        // City Watchdog already owns this bottom-toolbar trend area.
        public bool ShouldHideMoneyPopulationTrendOptions()
        {
            return CityWatchdogDetected;
        }

        // Hide even the master toggle when City Watchdog is installed.
        public bool ShouldHideMoneyPopulationTooltipMasterOption()
        {
            return CityWatchdogDetected;
        }

        // Child options only show when Beyond's master tooltip toggle is enabled.
        public bool ShouldHideMoneyPopulationTooltipChildOptions()
        {
            return CityWatchdogDetected || !EnableMoneyTooltip;
        }

        // Effective values force Beyond's injected money/population UI off when needed.
        internal bool EffectiveShowMoneyTrendHourly => !CityWatchdogDetected && ShowMoneyTrendHourly;
        internal bool EffectiveShowMoneyTrendMonthly => !CityWatchdogDetected && ShowMoneyTrendMonthly;
        internal bool EffectiveShowPopTrendHourly => !CityWatchdogDetected && ShowPopTrendHourly;
        internal bool EffectiveShowPopTrendMonthly => !CityWatchdogDetected && ShowPopTrendMonthly;

        internal bool EffectiveEnableMoneyTooltip => !CityWatchdogDetected && EnableMoneyTooltip;
        internal bool EffectiveShowTooltipIncome => EffectiveEnableMoneyTooltip && ShowTooltipIncome;
        internal bool EffectiveShowTooltipExpense => EffectiveEnableMoneyTooltip && ShowTooltipExpense;
        internal bool EffectiveShowTooltipNet => EffectiveEnableMoneyTooltip && ShowTooltipNet;
        internal bool EffectiveShowTooltipHourlyValues => EffectiveEnableMoneyTooltip && ShowTooltipHourlyValues;
        internal bool EffectiveShowTooltipMonthlyValues => EffectiveEnableMoneyTooltip && ShowTooltipMonthlyValues;

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
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTrendOptions))]
        public bool ShowMoneyTrendHourly
        {
            get => m_ShowMoneyTrendHourly;
            set { m_ShowMoneyTrendHourly = value; Mod.PushBindings(); }
        }
        private bool m_ShowMoneyTrendHourly;

        [SettingsUISection(kSection, kTrendGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTrendOptions))]
        public bool ShowMoneyTrendMonthly
        {
            get => m_ShowMoneyTrendMonthly;
            set { m_ShowMoneyTrendMonthly = value; Mod.PushBindings(); }
        }
        private bool m_ShowMoneyTrendMonthly;

        [SettingsUISection(kSection, kTrendGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTrendOptions))]
        public bool ShowPopTrendHourly
        {
            get => m_ShowPopTrendHourly;
            set { m_ShowPopTrendHourly = value; Mod.PushBindings(); }
        }
        private bool m_ShowPopTrendHourly;

        [SettingsUISection(kSection, kTrendGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTrendOptions))]
        public bool ShowPopTrendMonthly
        {
            get => m_ShowPopTrendMonthly;
            set { m_ShowPopTrendMonthly = value; Mod.PushBindings(); }
        }
        private bool m_ShowPopTrendMonthly;

        // ----- Tooltip -----
        [SettingsUISection(kSection, kTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTooltipMasterOption))]
        public bool EnableMoneyTooltip
        {
            get => m_EnableMoneyTooltip;
            set { m_EnableMoneyTooltip = value; Mod.PushBindings(); }
        }
        private bool m_EnableMoneyTooltip;

        [SettingsUISection(kSection, kTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTooltipChildOptions))]
        public bool ShowTooltipIncome
        {
            get => m_ShowTooltipIncome;
            set { m_ShowTooltipIncome = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipIncome;

        [SettingsUISection(kSection, kTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTooltipChildOptions))]
        public bool ShowTooltipExpense
        {
            get => m_ShowTooltipExpense;
            set { m_ShowTooltipExpense = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipExpense;

        [SettingsUISection(kSection, kTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTooltipChildOptions))]
        public bool ShowTooltipNet
        {
            get => m_ShowTooltipNet;
            set { m_ShowTooltipNet = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipNet;

        [SettingsUISection(kSection, kTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTooltipChildOptions))]
        public bool ShowTooltipHourlyValues
        {
            get => m_ShowTooltipHourlyValues;
            set { m_ShowTooltipHourlyValues = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipHourlyValues;

        [SettingsUISection(kSection, kTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTooltipChildOptions))]
        public bool ShowTooltipMonthlyValues
        {
            get => m_ShowTooltipMonthlyValues;
            set { m_ShowTooltipMonthlyValues = value; Mod.PushBindings(); }
        }
        private bool m_ShowTooltipMonthlyValues;
    }
}
