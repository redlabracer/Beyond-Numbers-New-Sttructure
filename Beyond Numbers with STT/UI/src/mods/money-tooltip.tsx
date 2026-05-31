import type { ModuleRegistryExtend } from "cs2/modding";
import { Children, isValidElement, type ReactNode } from "react";
import { useValue } from "cs2/api";
import { economyBudget, toolbarBottom } from "cs2/bindings";
import {
    enableMoneyTooltip$,
    showTooltipIncome$,
    showTooltipExpense$,
    showTooltipNet$,
    showTooltipHourlyValues$,
    showTooltipMonthlyValues$,
    showPopTrendMonthly$,
    daysPerYear$,
    hourlyToMonthly,
    monthlyToHourly,
    formatNumber,
} from "../settings";

const MONEY_ICON = "Media/Game/Icons/Money.svg";
const POPULATION_ICON = "Media/Game/Icons/Citizen.svg";

const POSITIVE_COLOR = "#6dd06d";
const NEGATIVE_COLOR = "#e26b6b";

export const DescriptionTooltipExtension: ModuleRegistryExtend = (Component: any) => {
    return (props: any) => {
        if (containsIcon(props?.children, MONEY_ICON)) {
            return Component({
                ...props,
                content: (
                    <>
                        {props.content}
                        <MoneyTooltipRows />
                    </>
                ),
            });
        }

        if (containsIcon(props?.children, POPULATION_ICON)) {
            return Component({
                ...props,
                content: (
                    <>
                        {props.content}
                        <PopulationTooltipRows />
                    </>
                ),
            });
        }

        return Component(props);
    };
};

const MoneyTooltipRows = () => {
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

const PopulationTooltipRows = () => {
    const showMonthly = useValue(showPopTrendMonthly$);
    const delta = useValue(toolbarBottom.populationDelta$);
    const daysPerYear = useValue(daysPerYear$);

    if (!showMonthly) {
        return null;
    }

    const monthly = hourlyToMonthly(delta, daysPerYear);

    return (
        <Section>
            <Row label="Current monthly trend:" value={`${withSign(monthly)} /M`} color={tone(monthly)} />
        </Section>
    );
};

const Section = ({ children }: { readonly children: ReactNode }) => {
    return (
        <div
            style={{
                display: "flex",
                flexDirection: "column",
                gap: "2rem",
                marginTop: "6rem",
                paddingTop: "6rem",
                borderTop: "1px solid rgba(255,255,255,0.15)",
                whiteSpace: "nowrap",
            }}
        >
            {children}
        </div>
    );
};

const Row = ({ label, value, color }: { readonly label: string; readonly value: string; readonly color: string }) => {
    return (
        <div style={{ display: "flex", justifyContent: "space-between", gap: "14rem", fontSize: "12rem" }}>
            <span style={{ opacity: 0.85 }}>{label}</span>
            <span style={{ color }}>{value}</span>
        </div>
    );
};

const tone = (n: number): string => (n < 0 ? NEGATIVE_COLOR : POSITIVE_COLOR);

const withSign = (value: number): string => {
    const rounded = Math.round(value);
    const prefix = rounded > 0 ? "+" : rounded < 0 ? "-" : "";
    return `${prefix}${formatNumber(value)}`;
};

const containsIcon = (node: ReactNode, icon: string): boolean => {
    if (!isValidElement(node)) {
        return false;
    }

    const props = node.props as any;
    if (props?.icon === icon || props?.src === icon) {
        return true;
    }

    return Children.toArray(props?.children).some((child: ReactNode) => containsIcon(child, icon));
};
