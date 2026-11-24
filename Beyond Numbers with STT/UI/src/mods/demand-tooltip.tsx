import { isValidElement, type ReactElement, type ReactNode } from "react";
import { useValue, type ValueBinding } from "cs2/api";
import { cityInfo } from "cs2/bindings";
import { getModule } from "cs2/modding";
import { useLocalization } from "cs2/l10n";
import {
    enableDemandTooltip$,
    demandTooltipDemandValues$,
    demandTooltipFactors$,
    demandTooltipFactorWeights$,
} from "../settings";
import { useNumberFormatter } from "../number-format";
import { POSITIVE_COLOR, NEGATIVE_COLOR, NEUTRAL_COLOR } from "./tooltip-shared";

const DEMAND_PAGE_MODULE = "game-ui/game/components/city-info-panel/city-info-demand/demand-page.tsx";
const POSITIVE_ICON = "Media/Game/Misc/DemandFactorPositive.svg";
const NEGATIVE_ICON = "Media/Game/Misc/DemandFactorNegative.svg";

// Index matches the game's DemandType enum (None = 0, ResidentialLow = 1, ... Office = 6).
const FALLBACK_COLORS = ["", "#8BDB46", "#00AE1A", "#2B7600", "#46B2DB", "#DBC246", "#8D44AD"];

const demandColors: string[] = (() => {
    try {
        const colors = getModule(DEMAND_PAGE_MODULE, "demandColors");
        return Array.isArray(colors) && colors.length >= FALLBACK_COLORS.length ? colors : FALLBACK_COLORS;
    } catch {
        return FALLBACK_COLORS;
    }
})();

export interface DemandBarsProps {
    resMedLocked?: boolean;
    resHighLocked?: boolean;
    officeLocked?: boolean;
}

export const isDemandBarsElement = (node: ReactNode): node is ReactElement<DemandBarsProps> => {
    if (!isValidElement(node)) {
        return false;
    }

    const props = node.props as any;
    return props != null && typeof props === "object" && "resMedLocked" in props;
};

interface ZoneDef {
    id: string;
    typeIndex: number;
    demand$: ValueBinding<number>;
    factors$: ValueBinding<cityInfo.Factor[]>;
    isLocked: (locks: DemandBarsProps) => boolean;
}

const ZONES: ZoneDef[] = [
    { id: "ResidentialLow", typeIndex: 1, demand$: cityInfo.residentialLowDemand$, factors$: cityInfo.residentialLowFactors$, isLocked: () => false },
    { id: "ResidentialMedium", typeIndex: 2, demand$: cityInfo.residentialMediumDemand$, factors$: cityInfo.residentialMediumFactors$, isLocked: (l) => !!l.resMedLocked },
    { id: "ResidentialHigh", typeIndex: 3, demand$: cityInfo.residentialHighDemand$, factors$: cityInfo.residentialHighFactors$, isLocked: (l) => !!l.resHighLocked },
    { id: "Commercial", typeIndex: 4, demand$: cityInfo.commercialDemand$, factors$: cityInfo.commercialFactors$, isLocked: () => false },
    { id: "Industrial", typeIndex: 5, demand$: cityInfo.industrialDemand$, factors$: cityInfo.industrialFactors$, isLocked: () => false },
    { id: "Office", typeIndex: 6, demand$: cityInfo.officeDemand$, factors$: cityInfo.officeFactors$, isLocked: (l) => !!l.officeLocked },
];

const splitCamelCase = (value: string): string => value.replace(/([a-z])([A-Z])/g, "$1 $2");

const DemandValue = ({ binding }: { readonly binding: ValueBinding<number> }) => {
    const demand = useValue(binding);
    return <span style={{ color: NEUTRAL_COLOR }}>{`${Math.round(demand * 100)} %`}</span>;
};

const ZoneFactors = ({ binding, showWeights }: { readonly binding: ValueBinding<cityInfo.Factor[]>; readonly showWeights: boolean }) => {
    const { translate } = useLocalization();
    const factors = useValue(binding);
    const format = useNumberFormatter();

    const rows = (factors ?? []).filter((f) => f != null && f.factor != null && f.weight !== 0);
    if (rows.length === 0) {
        return null;
    }

    return (
        <>
            {rows.map((f) => {
                const negative = f.weight < 0;
                const key = `CityInfoPanel.DEMAND_FACTOR_${negative ? "NEGATIVE" : "POSITIVE"}[${f.factor}]`;
                const label = translate(key, splitCamelCase(f.factor)) ?? splitCamelCase(f.factor);

                return (
                    <div
                        key={f.factor}
                        style={{ display: "flex", justifyContent: "space-between", alignItems: "center", gap: "14rem", paddingLeft: "16rem", fontSize: "11rem" }}
                    >
                        <span style={{ display: "flex", alignItems: "center", opacity: 0.85 }}>
                            <img src={negative ? NEGATIVE_ICON : POSITIVE_ICON} style={{ width: "12rem", height: "12rem", marginRight: "6rem" }} />
                            {label}
                        </span>
                        {showWeights && <span style={{ color: negative ? NEGATIVE_COLOR : POSITIVE_COLOR }}>{format.signed(f.weight)}</span>}
                    </div>
                );
            })}
        </>
    );
};

const ZoneBlock = ({ zone, showValues, showFactors, showWeights }: {
    readonly zone: ZoneDef;
    readonly showValues: boolean;
    readonly showFactors: boolean;
    readonly showWeights: boolean;
}) => {
    const { translate } = useLocalization();
    const title = translate(`CityInfoPanel.DEMAND_TITLE[${zone.id}]`, splitCamelCase(zone.id)) ?? splitCamelCase(zone.id);

    return (
        <div style={{ display: "flex", flexDirection: "column", gap: "2rem" }}>
            <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", gap: "14rem", fontSize: "12rem" }}>
                <span style={{ display: "flex", alignItems: "center" }}>
                    <span
                        style={{
                            width: "10rem",
                            height: "10rem",
                            borderRadius: "2rem",
                            marginRight: "6rem",
                            backgroundColor: demandColors[zone.typeIndex],
                        }}
                    />
                    {title}
                </span>
                {showValues && <DemandValue binding={zone.demand$} />}
            </div>
            {showFactors && <ZoneFactors binding={zone.factors$} showWeights={showWeights} />}
        </div>
    );
};

export const DemandTooltipContent = ({ locks, fallback }: { readonly locks: DemandBarsProps; readonly fallback: ReactNode }) => {
    const enabled = useValue(enableDemandTooltip$);
    const showValues = useValue(demandTooltipDemandValues$);
    const showFactors = useValue(demandTooltipFactors$);
    const showWeights = useValue(demandTooltipFactorWeights$);

    if (!enabled) {
        return <>{fallback}</>;
    }

    return (
        <div style={{ display: "flex", flexDirection: "column", gap: "6rem", whiteSpace: "nowrap" }}>
            {ZONES.filter((zone) => !zone.isLocked(locks)).map((zone) => (
                <ZoneBlock key={zone.id} zone={zone} showValues={showValues} showFactors={showFactors} showWeights={showWeights} />
            ))}
        </div>
    );
};
