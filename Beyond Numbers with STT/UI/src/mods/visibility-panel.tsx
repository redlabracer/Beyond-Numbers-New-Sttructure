import React, { useCallback, useEffect, useState } from "react";
import { bindValue, trigger, useValue, ValueBinding } from "cs2/api";
import { getModule } from "cs2/modding";
import { Button, PanelSection, PanelSectionRow, Tooltip } from "cs2/ui";
import { useLocalization } from "cs2/l10n";
import * as s from "../settings";

const GROUP = "BeyondNumbers";
const LOC = "BeyondNumbers.VisibilityPanel.";
const ICON = "Media/Glyphs/EyeOpen.svg";
const TABS_MODULE = "game-ui/common/tabs/tabs.tsx";

const optionLabelFormat$ = bindValue<string>(GROUP, "optionLabelFormat", "");
const optionDescFormat$ = bindValue<string>(GROUP, "optionDescFormat", "");

const Checkbox: React.FC<{ checked?: boolean; onChange?: (value: boolean) => void }> =
    getModule("game-ui/common/input/toggle/checkbox/checkbox.tsx", "Checkbox");
const TabBar: React.FC<{ className?: string; children?: React.ReactNode }> = getModule(TABS_MODULE, "TabBar");
const Tab: React.FC<{ id: string; selectedId: string; onSelect: (id: string) => void; children?: React.ReactNode }> =
    getModule(TABS_MODULE, "Tab");
const PanelTitleBar: React.FC<{ children?: React.ReactNode }> =
    getModule("game-ui/common/panel/panel-title-bar.tsx", "PanelTitleBar");
const DraggablePanel: React.FC<any> = getModule("game-ui/common/panel/draggable-panel/draggable-panel.tsx", "DraggablePanel");
const iceflakeTheme = getModule("game-ui/common/panel/themes/iceflake-panel.module.scss", "classes");

let panelOpen = false;
const listeners = new Set<(open: boolean) => void>();

function setPanelOpen(open: boolean) {
    panelOpen = open;
    listeners.forEach((l) => l(open));
}

function usePanelOpen(): boolean {
    const [open, setOpen] = useState(panelOpen);
    useEffect(() => {
        listeners.add(setOpen);
        return () => {
            listeners.delete(setOpen);
        };
    }, []);
    return open;
}

interface OptionDef {
    property: string;
    binding: ValueBinding<boolean>;
    fallback: string;
}

interface TabDef {
    id: string;
    fallback: string;
    master?: OptionDef;
    options: OptionDef[];
    blockedByCityWatchdog: boolean;
}

const TABS: TabDef[] = [
    {
        id: "Visibility",
        fallback: "Visibility",
        blockedByCityWatchdog: false,
        options: [
            { property: "HidePopulation", binding: s.hidePopulation$, fallback: "Hide Population" },
            { property: "HideDemand", binding: s.hideDemand$, fallback: "Hide Demand Bars" },
            { property: "HideDate", binding: s.hideDate$, fallback: "Hide Date" },
            { property: "HideTime", binding: s.hideTime$, fallback: "Hide Time" },
        ],
    },
    {
        id: "Trends",
        fallback: "Trends",
        blockedByCityWatchdog: true,
        options: [
            { property: "ShowMoneyTrendHourly", binding: s.showMoneyTrendHourly$, fallback: "Show Money Trend (Hourly)" },
            { property: "ShowMoneyTrendMonthly", binding: s.showMoneyTrendMonthly$, fallback: "Show Money Trend (Monthly)" },
            { property: "ShowPopTrendHourly", binding: s.showPopTrendHourly$, fallback: "Show Population Trend (Hourly)" },
            { property: "ShowPopTrendMonthly", binding: s.showPopTrendMonthly$, fallback: "Show Population Trend (Monthly)" },
        ],
    },
    {
        id: "MoneyTooltip",
        fallback: "Money Tooltip",
        blockedByCityWatchdog: true,
        master: { property: "EnableMoneyTooltip", binding: s.enableMoneyTooltip$, fallback: "Enable Detailed Money Tooltip" },
        options: [
            { property: "ShowTooltipIncome", binding: s.showTooltipIncome$, fallback: "Show Income" },
            { property: "ShowTooltipExpense", binding: s.showTooltipExpense$, fallback: "Show Expense" },
            { property: "ShowTooltipNet", binding: s.showTooltipNet$, fallback: "Show Net" },
            { property: "ShowTooltipHourlyValues", binding: s.showTooltipHourlyValues$, fallback: "Show Hourly Values" },
            { property: "ShowTooltipMonthlyValues", binding: s.showTooltipMonthlyValues$, fallback: "Show Monthly Values" },
        ],
    },
    {
        id: "PopulationTooltip",
        fallback: "Population Tooltip",
        blockedByCityWatchdog: true,
        master: { property: "EnablePopTooltip", binding: s.enablePopTooltip$, fallback: "Enable Population Tooltip" },
        options: [
            { property: "PopTooltipMonthlyTrend", binding: s.popTooltipMonthlyTrend$, fallback: "Monthly Trend" },
            { property: "PopTooltipPopulation", binding: s.popTooltipPopulation$, fallback: "Population" },
            { property: "PopTooltipBirthRate", binding: s.popTooltipBirthRate$, fallback: "Birth Rate" },
            { property: "PopTooltipDeathRate", binding: s.popTooltipDeathRate$, fallback: "Death Rate" },
            { property: "PopTooltipMovedIn", binding: s.popTooltipMovedIn$, fallback: "Moved In" },
            { property: "PopTooltipMovedAway", binding: s.popTooltipMovedAway$, fallback: "Moved Away" },
            { property: "PopTooltipJobs", binding: s.popTooltipJobs$, fallback: "Jobs" },
            { property: "PopTooltipEmployed", binding: s.popTooltipEmployed$, fallback: "Employed" },
            { property: "PopTooltipUnemployment", binding: s.popTooltipUnemployment$, fallback: "Unemployment" },
            { property: "PopTooltipHomeless", binding: s.popTooltipHomeless$, fallback: "Homeless" },
            { property: "PopTooltipHomelessRate", binding: s.popTooltipHomelessRate$, fallback: "Homeless Rate" },
        ],
    },
];

const OptionRow: React.FC<{ option: OptionDef; subRow?: boolean }> = ({ option, subRow }) => {
    const { translate } = useLocalization();
    const labelFormat = useValue(optionLabelFormat$);
    const descFormat = useValue(optionDescFormat$);
    const value = useValue(option.binding);
    const onChange = useCallback((v: boolean) => trigger(GROUP, "setOption", option.property, v), [option.property]);

    const label = labelFormat ? translate(labelFormat.replace("{0}", option.property), option.fallback) : option.fallback;
    const desc = descFormat ? translate(descFormat.replace("{0}", option.property), null) : null;

    return (
        <PanelSectionRow
            subRow={subRow}
            left={label}
            tooltip={desc || undefined}
            right={<Checkbox checked={value} onChange={onChange} />}
        />
    );
};

const TabContent: React.FC<{ tab: TabDef }> = ({ tab }) => {
    const { translate } = useLocalization();
    const cityWatchdog = useValue(s.cityWatchdogInstalled$);
    const masterOn = useValue(tab.master?.binding ?? s.cityWatchdogInstalled$);

    if (tab.blockedByCityWatchdog && cityWatchdog) {
        return (
            <PanelSection>
                <PanelSectionRow left={translate(LOC + "CityWatchdog", "Disabled because City Watchdog is installed.")} />
            </PanelSection>
        );
    }

    return (
        <PanelSection>
            {tab.master && <OptionRow option={tab.master} />}
            {(!tab.master || masterOn) &&
                tab.options.map((o) => <OptionRow key={o.property} option={o} subRow={!!tab.master} />)}
        </PanelSection>
    );
};

export enum ButtonLocation {
    UniversalModMenu = 0,
    TopLeft = 1,
    TopRight = 2,
}

const buttonLocation$ = bindValue<number>(GROUP, "buttonLocation", ButtonLocation.UniversalModMenu);

const MenuButton: React.FC<{ location: ButtonLocation }> = ({ location }) => {
    const { translate } = useLocalization();
    const open = usePanelOpen();
    const current = useValue(buttonLocation$);

    if (current !== location) {
        return null;
    }

    return (
        <Tooltip tooltip={translate(LOC + "Button", "Beyond Numbers Settings")}>
            <Button variant="floating" src={ICON} selected={open} onSelect={() => setPanelOpen(!open)} />
        </Tooltip>
    );
};

export const VisibilityMenuButton: React.FC = () => <MenuButton location={ButtonLocation.UniversalModMenu} />;
export const TopLeftMenuButton: React.FC = () => <MenuButton location={ButtonLocation.TopLeft} />;
export const TopRightMenuButton: React.FC = () => <MenuButton location={ButtonLocation.TopRight} />;

const TAB_BAR_STYLE: React.CSSProperties = {
    display: "flex",
    flexDirection: "row",
    alignItems: "flex-end",
    padding: "0 10rem",
    borderBottomStyle: "solid",
    borderBottomWidth: "var(--stroke2)",
    borderBottomColor: "var(--accentColorDark)",
};

const TabButton: React.FC<{ selected: boolean; first: boolean; onSelect: () => void; children?: React.ReactNode }> = ({
    selected,
    first,
    onSelect,
    children,
}) => {
    const [hover, setHover] = useState(false);
    const style: React.CSSProperties = {
        flex: "1 1 0",
        minWidth: 0,
        marginLeft: first ? 0 : "var(--gap4)",
        padding: "7rem 10rem 5rem",
        borderTopLeftRadius: "9rem",
        borderTopRightRadius: "9rem",
        textAlign: "center",
        fontSize: "var(--fontSizeM)",
        textTransform: "uppercase",
        overflow: "hidden",
        textOverflow: "ellipsis",
        whiteSpace: "nowrap",
        color: selected ? "#fff" : "rgba(255,255,255,.8)",
        backgroundColor: selected ? "transparent" : hover ? "rgba(255,255,255,.15)" : "rgba(255,255,255,.1)",
        backgroundImage: selected ? "linear-gradient(var(--accentColorNormal), var(--accentColorDark))" : undefined,
    };

    return (
        <div
            style={style}
            onMouseEnter={() => setHover(true)}
            onMouseLeave={() => setHover(false)}
            onMouseDown={(e) => e.stopPropagation()}
            onClick={() => {
                if (!selected) {
                    trigger("audio", "playSound", "select-item", 1);
                    onSelect();
                }
            }}
        >
            {children}
        </div>
    );
};

export const VisibilityPanel: React.FC = () => {
    const { translate } = useLocalization();
    const open = usePanelOpen();
    const [selectedTab, setSelectedTab] = useState(TABS[0].id);

    if (!open) {
        return null;
    }

    const tab = TABS.find((t) => t.id === selectedTab) ?? TABS[0];

    return (
        <DraggablePanel
            initialPosition={{ x: 0.8, y: 0.3 }}
            style={{ width: "600rem" }}
            theme={iceflakeTheme}
            header={
                <div style={{ width: "100%", overflow: "hidden" }}>
                    <PanelTitleBar>{translate(LOC + "Title", "Beyond Numbers")}</PanelTitleBar>
                    <div style={TAB_BAR_STYLE}>
                        {TABS.map((t, i) => (
                            <TabButton
                                key={t.id}
                                selected={t.id === tab.id}
                                first={i === 0}
                                onSelect={() => setSelectedTab(t.id)}
                            >
                                {translate(LOC + "Tab." + t.id, t.fallback)}
                            </TabButton>
                        ))}
                    </div>
                </div>
            }
            onClose={() => setPanelOpen(false)}
        >
            <TabContent tab={tab} />
        </DraggablePanel>
    );
};
