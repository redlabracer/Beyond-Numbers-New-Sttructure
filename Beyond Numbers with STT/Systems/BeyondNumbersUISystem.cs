namespace Beyond_Numbers_with_STT.Systems
{
    using Beyond_Numbers_with_STT.Compatibility;
    using Colossal.UI.Binding;
    using Game.Simulation;
    using Game.UI;

    public partial class BeyondNumbersUISystem : UISystemBase
    {
        public const string GroupName = "BeyondNumbers";

        private TimeSystem m_TimeSystem;

        private ValueBinding<bool> b_hidePopulation;
        private ValueBinding<bool> b_hideDemand;
        private ValueBinding<bool> b_hideDate;
        private ValueBinding<bool> b_hideTime;

        private ValueBinding<bool> b_showMoneyTrendHourly;
        private ValueBinding<bool> b_showMoneyTrendMonthly;
        private ValueBinding<bool> b_showPopTrendHourly;
        private ValueBinding<bool> b_showPopTrendMonthly;

        private ValueBinding<bool> b_enableMoneyTooltip;
        private ValueBinding<bool> b_showTooltipIncome;
        private ValueBinding<bool> b_showTooltipExpense;
        private ValueBinding<bool> b_showTooltipNet;
        private ValueBinding<bool> b_showTooltipHourlyValues;
        private ValueBinding<bool> b_showTooltipMonthlyValues;

        private ValueBinding<bool> b_cityWatchdogInstalled;
        private ValueBinding<int> b_daysPerYear;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_TimeSystem = World.GetOrCreateSystemManaged<TimeSystem>();

            AddBinding(b_hidePopulation = new ValueBinding<bool>(GroupName, "hidePopulation", Mod.m_Setting?.HidePopulation ?? false));
            AddBinding(b_hideDemand     = new ValueBinding<bool>(GroupName, "hideDemand",     Mod.m_Setting?.HideDemand     ?? false));
            AddBinding(b_hideDate       = new ValueBinding<bool>(GroupName, "hideDate",       Mod.m_Setting?.HideDate       ?? false));
            AddBinding(b_hideTime       = new ValueBinding<bool>(GroupName, "hideTime",       Mod.m_Setting?.HideTime       ?? false));

            // Effective values force these UI injections OFF when City Watchdog is detected.
            AddBinding(b_showMoneyTrendHourly  = new ValueBinding<bool>(GroupName, "showMoneyTrendHourly",  Mod.m_Setting?.EffectiveShowMoneyTrendHourly  ?? false));
            AddBinding(b_showMoneyTrendMonthly = new ValueBinding<bool>(GroupName, "showMoneyTrendMonthly", Mod.m_Setting?.EffectiveShowMoneyTrendMonthly ?? false));
            AddBinding(b_showPopTrendHourly    = new ValueBinding<bool>(GroupName, "showPopTrendHourly",    Mod.m_Setting?.EffectiveShowPopTrendHourly    ?? false));
            AddBinding(b_showPopTrendMonthly   = new ValueBinding<bool>(GroupName, "showPopTrendMonthly",   Mod.m_Setting?.EffectiveShowPopTrendMonthly   ?? false));

            // Master OFF also forces all child tooltip bindings OFF.
            AddBinding(b_enableMoneyTooltip       = new ValueBinding<bool>(GroupName, "enableMoneyTooltip",       Mod.m_Setting?.EffectiveEnableMoneyTooltip       ?? false));
            AddBinding(b_showTooltipIncome        = new ValueBinding<bool>(GroupName, "showTooltipIncome",        Mod.m_Setting?.EffectiveShowTooltipIncome        ?? false));
            AddBinding(b_showTooltipExpense       = new ValueBinding<bool>(GroupName, "showTooltipExpense",       Mod.m_Setting?.EffectiveShowTooltipExpense       ?? false));
            AddBinding(b_showTooltipNet           = new ValueBinding<bool>(GroupName, "showTooltipNet",           Mod.m_Setting?.EffectiveShowTooltipNet           ?? false));
            AddBinding(b_showTooltipHourlyValues  = new ValueBinding<bool>(GroupName, "showTooltipHourlyValues",  Mod.m_Setting?.EffectiveShowTooltipHourlyValues  ?? false));
            AddBinding(b_showTooltipMonthlyValues = new ValueBinding<bool>(GroupName, "showTooltipMonthlyValues", Mod.m_Setting?.EffectiveShowTooltipMonthlyValues ?? false));

            AddBinding(b_cityWatchdogInstalled = new ValueBinding<bool>(GroupName, "cityWatchdogInstalled", CwdCompatibility.IsCityWatchdogInstalled()));
            AddBinding(b_daysPerYear = new ValueBinding<int>(GroupName, "daysPerYear", GetDaysPerYear()));
        }

        public void UpdateBindings()
        {
            if (Mod.m_Setting == null) return;

            b_hidePopulation.Update(Mod.m_Setting.HidePopulation);
            b_hideDemand    .Update(Mod.m_Setting.HideDemand);
            b_hideDate      .Update(Mod.m_Setting.HideDate);
            b_hideTime      .Update(Mod.m_Setting.HideTime);

            // Use effective settings so hidden/disabled options cannot still inject UI.
            b_showMoneyTrendHourly .Update(Mod.m_Setting.EffectiveShowMoneyTrendHourly);
            b_showMoneyTrendMonthly.Update(Mod.m_Setting.EffectiveShowMoneyTrendMonthly);
            b_showPopTrendHourly   .Update(Mod.m_Setting.EffectiveShowPopTrendHourly);
            b_showPopTrendMonthly  .Update(Mod.m_Setting.EffectiveShowPopTrendMonthly);

            b_enableMoneyTooltip      .Update(Mod.m_Setting.EffectiveEnableMoneyTooltip);
            b_showTooltipIncome       .Update(Mod.m_Setting.EffectiveShowTooltipIncome);
            b_showTooltipExpense      .Update(Mod.m_Setting.EffectiveShowTooltipExpense);
            b_showTooltipNet          .Update(Mod.m_Setting.EffectiveShowTooltipNet);
            b_showTooltipHourlyValues .Update(Mod.m_Setting.EffectiveShowTooltipHourlyValues);
            b_showTooltipMonthlyValues.Update(Mod.m_Setting.EffectiveShowTooltipMonthlyValues);

            b_cityWatchdogInstalled.Update(CwdCompatibility.IsCityWatchdogInstalled());
            b_daysPerYear.Update(GetDaysPerYear());
        }

        private int GetDaysPerYear()
        {
            try
            {
                if (m_TimeSystem == null)
                    return 365;
                return m_TimeSystem.daysPerYear <= 0 ? 365 : m_TimeSystem.daysPerYear;
            }
            catch
            {
                return 365;
            }
        }
    }
}
