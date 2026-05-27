using Colossal.UI.Binding;
using Game.Simulation;
using Game.UI;

namespace Beyond_Numbers_with_STT.Systems
{
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

        private ValueBinding<int> b_daysPerYear;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_TimeSystem = World.GetOrCreateSystemManaged<TimeSystem>();

            AddBinding(b_hidePopulation = new ValueBinding<bool>(GroupName, "hidePopulation", Mod.m_Setting?.HidePopulation ?? false));
            AddBinding(b_hideDemand     = new ValueBinding<bool>(GroupName, "hideDemand",     Mod.m_Setting?.HideDemand     ?? false));
            AddBinding(b_hideDate       = new ValueBinding<bool>(GroupName, "hideDate",       Mod.m_Setting?.HideDate       ?? false));
            AddBinding(b_hideTime       = new ValueBinding<bool>(GroupName, "hideTime",       Mod.m_Setting?.HideTime       ?? false));

            AddBinding(b_showMoneyTrendHourly  = new ValueBinding<bool>(GroupName, "showMoneyTrendHourly",  Mod.m_Setting?.ShowMoneyTrendHourly  ?? true));
            AddBinding(b_showMoneyTrendMonthly = new ValueBinding<bool>(GroupName, "showMoneyTrendMonthly", Mod.m_Setting?.ShowMoneyTrendMonthly ?? false));
            AddBinding(b_showPopTrendHourly    = new ValueBinding<bool>(GroupName, "showPopTrendHourly",    Mod.m_Setting?.ShowPopTrendHourly    ?? true));
            AddBinding(b_showPopTrendMonthly   = new ValueBinding<bool>(GroupName, "showPopTrendMonthly",   Mod.m_Setting?.ShowPopTrendMonthly   ?? false));

            AddBinding(b_enableMoneyTooltip       = new ValueBinding<bool>(GroupName, "enableMoneyTooltip",       Mod.m_Setting?.EnableMoneyTooltip       ?? true));
            AddBinding(b_showTooltipIncome        = new ValueBinding<bool>(GroupName, "showTooltipIncome",        Mod.m_Setting?.ShowTooltipIncome        ?? true));
            AddBinding(b_showTooltipExpense       = new ValueBinding<bool>(GroupName, "showTooltipExpense",       Mod.m_Setting?.ShowTooltipExpense       ?? true));
            AddBinding(b_showTooltipNet           = new ValueBinding<bool>(GroupName, "showTooltipNet",           Mod.m_Setting?.ShowTooltipNet           ?? true));
            AddBinding(b_showTooltipHourlyValues  = new ValueBinding<bool>(GroupName, "showTooltipHourlyValues",  Mod.m_Setting?.ShowTooltipHourlyValues  ?? true));
            AddBinding(b_showTooltipMonthlyValues = new ValueBinding<bool>(GroupName, "showTooltipMonthlyValues", Mod.m_Setting?.ShowTooltipMonthlyValues ?? true));

            AddBinding(b_daysPerYear = new ValueBinding<int>(GroupName, "daysPerYear", GetDaysPerYear()));
        }

        public void UpdateBindings()
        {
            if (Mod.m_Setting == null) return;

            b_hidePopulation.Update(Mod.m_Setting.HidePopulation);
            b_hideDemand    .Update(Mod.m_Setting.HideDemand);
            b_hideDate      .Update(Mod.m_Setting.HideDate);
            b_hideTime      .Update(Mod.m_Setting.HideTime);

            b_showMoneyTrendHourly .Update(Mod.m_Setting.ShowMoneyTrendHourly);
            b_showMoneyTrendMonthly.Update(Mod.m_Setting.ShowMoneyTrendMonthly);
            b_showPopTrendHourly   .Update(Mod.m_Setting.ShowPopTrendHourly);
            b_showPopTrendMonthly  .Update(Mod.m_Setting.ShowPopTrendMonthly);

            b_enableMoneyTooltip      .Update(Mod.m_Setting.EnableMoneyTooltip);
            b_showTooltipIncome       .Update(Mod.m_Setting.ShowTooltipIncome);
            b_showTooltipExpense      .Update(Mod.m_Setting.ShowTooltipExpense);
            b_showTooltipNet          .Update(Mod.m_Setting.ShowTooltipNet);
            b_showTooltipHourlyValues .Update(Mod.m_Setting.ShowTooltipHourlyValues);
            b_showTooltipMonthlyValues.Update(Mod.m_Setting.ShowTooltipMonthlyValues);

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
