import type { ModuleRegistryExtend } from "cs2/modding";
import { useValue } from "cs2/api";
import { cityWatchdogInstalled$ } from "../settings";
import { containsIcon } from "./tooltip-shared";
import { MoneyTooltipRows } from "./money-tooltip";
import { PopulationTooltipRows } from "./population-tooltip";
import { DemandTooltipContent, isDemandBarsElement } from "./demand-tooltip";

const MONEY_ICON = "Media/Game/Icons/Money.svg";
const POPULATION_ICON = "Media/Game/Icons/Citizen.svg";

export const DescriptionTooltipExtension: ModuleRegistryExtend = (Component: any) => {
    return (props: any) => {
        const cityWatchdogInstalled = useValue(cityWatchdogInstalled$);

        // City Watchdog does not extend the demand bars, so this one is not gated on it.
        if (isDemandBarsElement(props?.children)) {
            return Component({
                ...props,
                content: <DemandTooltipContent locks={props.children.props} fallback={props.content} />,
            });
        }

        if (cityWatchdogInstalled) {
            return Component(props);
        }

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
