import { bindValue } from "cs2/api";
const GROUP = "BeyondNumbers";

export const hidePopulation$ = bindValue<boolean>(GROUP, "hidePopulation", false);
export const hideDemand$     = bindValue<boolean>(GROUP, "hideDemand",     false);
export const hideDate$       = bindValue<boolean>(GROUP, "hideDate",       false);
export const hideTime$       = bindValue<boolean>(GROUP, "hideTime",       false);

export const showMoneyTrendHourly$  = bindValue<boolean>(GROUP, "showMoneyTrendHourly",  true);
export const showMoneyTrendMonthly$ = bindValue<boolean>(GROUP, "showMoneyTrendMonthly", false);
export const showPopTrendHourly$    = bindValue<boolean>(GROUP, "showPopTrendHourly",    true);
export const showPopTrendMonthly$   = bindValue<boolean>(GROUP, "showPopTrendMonthly",   false);

export const enableMoneyTooltip$       = bindValue<boolean>(GROUP, "enableMoneyTooltip",       true);
export const showTooltipIncome$        = bindValue<boolean>(GROUP, "showTooltipIncome",        true);
export const showTooltipExpense$       = bindValue<boolean>(GROUP, "showTooltipExpense",       true);
export const showTooltipNet$           = bindValue<boolean>(GROUP, "showTooltipNet",           true);
export const showTooltipHourlyValues$  = bindValue<boolean>(GROUP, "showTooltipHourlyValues",  true);
export const showTooltipMonthlyValues$ = bindValue<boolean>(GROUP, "showTooltipMonthlyValues", true);

export const daysPerYear$ = bindValue<number>(GROUP, "daysPerYear", 365);

export const HOURS_PER_MONTH = 24;

export function hourlyToMonthly(hourly: number, _daysPerYear?: number): number {
    return hourly * HOURS_PER_MONTH;
}

export function monthlyToHourly(monthly: number): number {
    return monthly / HOURS_PER_MONTH;
}

export function formatNumber(value: number): string {
    const rounded = Math.round(value);
    return Math.abs(rounded).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
