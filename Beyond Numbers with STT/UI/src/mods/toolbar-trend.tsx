import type { ModuleRegistryExtend } from "cs2/modding";
import { cloneElement, isValidElement, type ReactElement, type ReactNode } from "react";
import { useValue } from "cs2/api";
import { toolbarBottom } from "cs2/bindings";
import {
    showMoneyTrendHourly$,
    showMoneyTrendMonthly$,
    showPopTrendHourly$,
    showPopTrendMonthly$,
    daysPerYear$,
    hourlyToMonthly,
    formatNumber,
} from "../settings";

const MONEY_ICON = "Media/Game/Icons/Money.svg";
const POPULATION_ICON = "Media/Game/Icons/Citizen.svg";

const POSITIVE_CLASS = "positive_n5t";
const NEGATIVE_CLASS = "negative_Moc";

export const StatFieldTrendExtension: ModuleRegistryExtend = (Component: any) => {
    return (props: any) => {
        const result = Component(props);

        if (props?.unlimited === true || !isValidElement(result)) {
            return result;
        }

        const extra = getTrendExtra(props);
        if (!extra) {
            return result;
        }

        return appendChild(result, extra);
    };
};

const getTrendExtra = (props: any): ReactNode | null => {
    if (props?.icon === MONEY_ICON) {
        return <MoneyTrendAmount />;
    }

    if (props?.icon === POPULATION_ICON) {
        return <PopTrendAmount />;
    }

    return null;
};

const MoneyTrendAmount = () => {
    const delta = useValue(toolbarBottom.moneyDelta$);
    const showHourly = useValue(showMoneyTrendHourly$);
    const showMonthly = useValue(showMoneyTrendMonthly$);
    const daysPerYear = useValue(daysPerYear$);

    return (
        <TrendAmount
            delta={delta}
            showHourly={showHourly}
            showMonthly={showMonthly}
            daysPerYear={daysPerYear}
        />
    );
};

const PopTrendAmount = () => {
    const delta = useValue(toolbarBottom.populationDelta$);
    const showHourly = useValue(showPopTrendHourly$);
    const showMonthly = useValue(showPopTrendMonthly$);
    const daysPerYear = useValue(daysPerYear$);

    return (
        <TrendAmount
            delta={delta}
            showHourly={showHourly}
            showMonthly={showMonthly}
            daysPerYear={daysPerYear}
        />
    );
};

const TrendAmount = ({
    delta,
    showHourly,
    showMonthly,
    daysPerYear,
}: {
    readonly delta: number;
    readonly showHourly: boolean;
    readonly showMonthly: boolean;
    readonly daysPerYear: number;
}) => {
    if (!showHourly && !showMonthly) {
        return null;
    }

    const sign = delta < 0 ? NEGATIVE_CLASS : POSITIVE_CLASS;
    const monthly = hourlyToMonthly(delta, daysPerYear);

    return (
        <>
            {showHourly && (
                <div className={sign} style={{ marginLeft: "4rem" }}>
                    {`${withSign(delta)} /h`}
                </div>
            )}
            {showHourly && showMonthly && (
                <div style={{ marginLeft: "4rem", opacity: 0.5 }}>{" · "}</div>
            )}
            {showMonthly && (
                <div className={sign} style={{ marginLeft: "4rem" }}>
                    {`${withSign(monthly)} /M`}
                </div>
            )}
        </>
    );
};

const withSign = (value: number): string => {
    const rounded = Math.round(value);
    const prefix = rounded > 0 ? "+" : rounded < 0 ? "-" : "";
    return `${prefix}${formatNumber(value)}`;
};

const appendChild = (element: ReactElement<any>, child: ReactNode): ReactElement<any> => {
    return cloneElement(
        element,
        undefined,
        <>
            {element.props.children}
            {child}
        </>
    );
};
