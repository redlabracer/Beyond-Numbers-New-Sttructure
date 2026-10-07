import { type ReactNode } from "react";
import { useValue } from "cs2/api";
import { economyBudget } from "cs2/bindings";
import {
    enableMoneyTooltip$,
    showTooltipIncome$,
    showTooltipExpense$,
    showTooltipNet$,
    showTooltipHourlyValues$,
    showTooltipMonthlyValues$,
    monthlyToHourly,
    formatNumber,
} from "../settings";
import { Row, Section, tone, withSign, POSITIVE_COLOR, NEGATIVE_COLOR } from "./tooltip-shared";

export const MoneyTooltipRows = () => {
    const enabled = useValue(enableMoneyTooltip$);
    const showIncome = useValue(showTooltipIncome$);
    const showExpense = useValue(showTooltipExpense$);
    const showNet = useValue(showTooltipNet$);
    const showHourly = useValue(showTooltipHourlyValues$);
    const showMonthly = useValue(showTooltipMonthlyValues$);
    const income = useValue(economyBudget.totalIncome$);
    const expense = useValue(economyBudget.totalExpenses$);

    if (!enabled) {
        return null;
    }

    const incomeM = Math.abs(income);
    const expenseM = Math.abs(expense);
    const netM = incomeM - expenseM;
    const incomeH = monthlyToHourly(incomeM);
    const expenseH = monthlyToHourly(expenseM);

    const rows: ReactNode[] = [];
    if (showNet && showMonthly) {
        rows.push(<Row key="net-m" label="Current monthly trend:" value={`${withSign(netM)} /M`} color={tone(netM)} />);
    }
    if (showIncome && showMonthly) {
        rows.push(<Row key="inc-m" label="Current monthly income:" value={`${withSign(incomeM)} /M`} color={POSITIVE_COLOR} />);
    }
    if (showExpense && showMonthly) {
        rows.push(<Row key="exp-m" label="Current monthly expenses:" value={`-${formatNumber(expenseM)} /M`} color={NEGATIVE_COLOR} />);
    }
    if (showHourly && showIncome) {
        rows.push(<Row key="inc-h" label="Current hourly income:" value={`${withSign(incomeH)} /h`} color={POSITIVE_COLOR} />);
    }
    if (showHourly && showExpense) {
        rows.push(<Row key="exp-h" label="Current hourly expenses:" value={`-${formatNumber(expenseH)} /h`} color={NEGATIVE_COLOR} />);
    }

    return <Section>{rows}</Section>;
};
