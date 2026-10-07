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

export const enablePopTooltip$       = bindValue<boolean>(GROUP, "enablePopTooltip",       true);
export const popTooltipMonthlyTrend$ = bindValue<boolean>(GROUP, "popTooltipMonthlyTrend", true);
export const popTooltipPopulation$   = bindValue<boolean>(GROUP, "popTooltipPopulation",   true);
export const popTooltipBirthRate$    = bindValue<boolean>(GROUP, "popTooltipBirthRate",    true);
export const popTooltipDeathRate$    = bindValue<boolean>(GROUP, "popTooltipDeathRate",    true);
export const popTooltipMovedIn$      = bindValue<boolean>(GROUP, "popTooltipMovedIn",      true);
export const popTooltipMovedAway$    = bindValue<boolean>(GROUP, "popTooltipMovedAway",    true);
export const popTooltipJobs$         = bindValue<boolean>(GROUP, "popTooltipJobs",         true);
export const popTooltipEmployed$     = bindValue<boolean>(GROUP, "popTooltipEmployed",     true);
export const popTooltipUnemployment$ = bindValue<boolean>(GROUP, "popTooltipUnemployment", true);
export const popTooltipHomeless$     = bindValue<boolean>(GROUP, "popTooltipHomeless",     true);
export const popTooltipHomelessRate$ = bindValue<boolean>(GROUP, "popTooltipHomelessRate", true);

export const cityWatchdogInstalled$ = bindValue<boolean>(GROUP, "cityWatchdogInstalled", false);

export const daysPerYear$ = bindValue<number>(GROUP, "daysPerYear", 365);

export const popPopulation$     = bindValue<number>(GROUP, "popPopulation",     0);
export const popBirthRate$      = bindValue<number>(GROUP, "popBirthRate",      0);
export const popDeathRate$      = bindValue<number>(GROUP, "popDeathRate",      0);
export const popMovedIn$        = bindValue<number>(GROUP, "popMovedIn",        0);
export const popMovedAway$      = bindValue<number>(GROUP, "popMovedAway",      0);
export const popJobs$           = bindValue<number>(GROUP, "popJobs",           0);
export const popEmployed$       = bindValue<number>(GROUP, "popEmployed",       0);
export const popUnemployment$   = bindValue<number>(GROUP, "popUnemployment",   0);
export const popHomeless$       = bindValue<number>(GROUP, "popHomeless",       0);
export const popHomelessRate$   = bindValue<number>(GROUP, "popHomelessRate",   0);

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
