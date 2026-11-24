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
} from "../settings";
import { useNumberFormatter } from "../number-format";
import { Row, Section, tone, POSITIVE_COLOR, NEGATIVE_COLOR } from "./tooltip-shared";

export const MoneyTooltipRows = () => {
    const enabled = useValue(enableMoneyTooltip$);
    const showIncome = useValue(showTooltipIncome$);
    const showExpense = useValue(showTooltipExpense$);
    const showNet = useValue(showTooltipNet$);
    const showHourly = useValue(showTooltipHourlyValues$);
    const showMonthly = useValue(showTooltipMonthlyValues$);
    const income = useValue(economyBudget.totalIncome$);
    const expense = useValue(economyBudget.totalExpenses$);
    const format = useNumberFormatter();

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
        rows.push(<Row key="net-m" label="Current monthly trend:" value={`${format.signed(netM)} /M`} color={tone(netM)} />);
    }
    if (showIncome && showMonthly) {
        rows.push(<Row key="inc-m" label="Current monthly income:" value={`${format.signed(incomeM)} /M`} color={POSITIVE_COLOR} />);
    }
    if (showExpense && showMonthly) {
        rows.push(<Row key="exp-m" label="Current monthly expenses:" value={`-${format.magnitude(expenseM)} /M`} color={NEGATIVE_COLOR} />);
    }
    if (showHourly && showIncome) {
        rows.push(<Row key="inc-h" label="Current hourly income:" value={`${format.signed(incomeH)} /h`} color={POSITIVE_COLOR} />);
    }
    if (showHourly && showExpense) {
        rows.push(<Row key="exp-h" label="Current hourly expenses:" value={`-${format.magnitude(expenseH)} /h`} color={NEGATIVE_COLOR} />);
    }

    return <Section>{rows}</Section>;
};
