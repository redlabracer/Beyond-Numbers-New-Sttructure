using Beyond_Numbers_with_STT.Compatibility;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;

namespace Beyond_Numbers_with_STT
{
    [FileLocation("ModsSettings/Beyond_Numbers/Beyond_Numbers")]
    [SettingsUIGroupOrder(kVisibilityGroup, kTrendGroup, kTooltipGroup, kPopTooltipGroup)]
    [SettingsUIShowGroupName(kVisibilityGroup, kTrendGroup, kTooltipGroup, kPopTooltipGroup)]
    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kVisibilityGroup = "Visibility";
        public const string kTrendGroup = "Trends";
        public const string kTooltipGroup = "MoneyTooltip";
        public const string kPopTooltipGroup = "PopulationTooltip";

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

            EnablePopTooltip = true;
            PopTooltipMonthlyTrend = true;
            PopTooltipPopulation = true;
            PopTooltipBirthRate = true;
            PopTooltipDeathRate = true;
            PopTooltipMovedIn = true;
            PopTooltipMovedAway = true;
            PopTooltipJobs = true;
            PopTooltipEmployed = true;
            PopTooltipUnemployment = true;
            PopTooltipHomeless = true;
            PopTooltipHomelessRate = true;
        }

        // ----- City Watchdog compatibility -----
        internal bool CityWatchdogDetected => CwdCompatibility.IsCityWatchdogInstalled();

        // City Watchdog already owns this bottom-toolbar trend area.
        public bool ShouldHideMoneyPopulationTrendOptions()
        {
            return CityWatchdogDetected;
        }

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

        public bool ShouldHidePopTooltipChildOptions()
        {
            return CityWatchdogDetected || !EnablePopTooltip;
        }

        internal bool EffectiveEnablePopTooltip => !CityWatchdogDetected && EnablePopTooltip;
        internal bool EffectivePopTooltipMonthlyTrend => EffectiveEnablePopTooltip && PopTooltipMonthlyTrend;
        internal bool EffectivePopTooltipPopulation => EffectiveEnablePopTooltip && PopTooltipPopulation;
        internal bool EffectivePopTooltipBirthRate => EffectiveEnablePopTooltip && PopTooltipBirthRate;
        internal bool EffectivePopTooltipDeathRate => EffectiveEnablePopTooltip && PopTooltipDeathRate;
        internal bool EffectivePopTooltipMovedIn => EffectiveEnablePopTooltip && PopTooltipMovedIn;
        internal bool EffectivePopTooltipMovedAway => EffectiveEnablePopTooltip && PopTooltipMovedAway;
        internal bool EffectivePopTooltipJobs => EffectiveEnablePopTooltip && PopTooltipJobs;
        internal bool EffectivePopTooltipEmployed => EffectiveEnablePopTooltip && PopTooltipEmployed;
        internal bool EffectivePopTooltipUnemployment => EffectiveEnablePopTooltip && PopTooltipUnemployment;
        internal bool EffectivePopTooltipHomeless => EffectiveEnablePopTooltip && PopTooltipHomeless;
        internal bool EffectivePopTooltipHomelessRate => EffectiveEnablePopTooltip && PopTooltipHomelessRate;

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

        // ----- Money Tooltip -----
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

        // ----- Population Tooltip -----
        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHideMoneyPopulationTooltipMasterOption))]
        public bool EnablePopTooltip
        {
            get => m_EnablePopTooltip;
            set { m_EnablePopTooltip = value; Mod.PushBindings(); }
        }
        private bool m_EnablePopTooltip;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipMonthlyTrend
        {
            get => m_PopTooltipMonthlyTrend;
            set { m_PopTooltipMonthlyTrend = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipMonthlyTrend;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipPopulation
        {
            get => m_PopTooltipPopulation;
            set { m_PopTooltipPopulation = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipPopulation;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipBirthRate
        {
            get => m_PopTooltipBirthRate;
            set { m_PopTooltipBirthRate = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipBirthRate;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipDeathRate
        {
            get => m_PopTooltipDeathRate;
            set { m_PopTooltipDeathRate = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipDeathRate;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipMovedIn
        {
            get => m_PopTooltipMovedIn;
            set { m_PopTooltipMovedIn = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipMovedIn;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipMovedAway
        {
            get => m_PopTooltipMovedAway;
            set { m_PopTooltipMovedAway = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipMovedAway;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipJobs
        {
            get => m_PopTooltipJobs;
            set { m_PopTooltipJobs = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipJobs;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipEmployed
        {
            get => m_PopTooltipEmployed;
            set { m_PopTooltipEmployed = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipEmployed;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipUnemployment
        {
            get => m_PopTooltipUnemployment;
            set { m_PopTooltipUnemployment = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipUnemployment;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipHomeless
        {
            get => m_PopTooltipHomeless;
            set { m_PopTooltipHomeless = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipHomeless;

        [SettingsUISection(kSection, kPopTooltipGroup)]
        [SettingsUIHideByCondition(typeof(Setting), nameof(ShouldHidePopTooltipChildOptions))]
        public bool PopTooltipHomelessRate
        {
            get => m_PopTooltipHomelessRate;
            set { m_PopTooltipHomelessRate = value; Mod.PushBindings(); }
        }
        private bool m_PopTooltipHomelessRate;
    }
}
