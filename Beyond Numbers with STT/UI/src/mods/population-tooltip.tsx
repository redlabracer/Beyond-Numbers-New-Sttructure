import { type ReactNode } from "react";
import { useValue } from "cs2/api";
import { toolbarBottom } from "cs2/bindings";
import {
    enablePopTooltip$,
    popTooltipMonthlyTrend$,
    popTooltipPopulation$,
    popTooltipBirthRate$,
    popTooltipDeathRate$,
    popTooltipMovedIn$,
    popTooltipMovedAway$,
    popTooltipJobs$,
    popTooltipEmployed$,
    popTooltipUnemployment$,
    popTooltipHomeless$,
    popTooltipHomelessRate$,
    daysPerYear$,
    hourlyToMonthly,
    formatNumber,
    popPopulation$,
    popBirthRate$,
    popDeathRate$,
    popMovedIn$,
    popMovedAway$,
    popJobs$,
    popEmployed$,
    popUnemployment$,
    popHomeless$,
    popHomelessRate$,
} from "../settings";
import { Row, Section, tone, withSign, formatPercent, POSITIVE_COLOR, NEGATIVE_COLOR, NEUTRAL_COLOR } from "./tooltip-shared";

export const PopulationTooltipRows = () => {
    const enabled = useValue(enablePopTooltip$);
    const showTrend = useValue(popTooltipMonthlyTrend$);
    const showPopulation = useValue(popTooltipPopulation$);
    const showBirthRate = useValue(popTooltipBirthRate$);
    const showDeathRate = useValue(popTooltipDeathRate$);
    const showMovedIn = useValue(popTooltipMovedIn$);
    const showMovedAway = useValue(popTooltipMovedAway$);
    const showJobs = useValue(popTooltipJobs$);
    const showEmployed = useValue(popTooltipEmployed$);
    const showUnemployment = useValue(popTooltipUnemployment$);
    const showHomeless = useValue(popTooltipHomeless$);
    const showHomelessRate = useValue(popTooltipHomelessRate$);

    const delta = useValue(toolbarBottom.populationDelta$);
    const daysPerYear = useValue(daysPerYear$);
    const population = useValue(popPopulation$);
    const birthRate = useValue(popBirthRate$);
    const deathRate = useValue(popDeathRate$);
    const movedIn = useValue(popMovedIn$);
    const movedAway = useValue(popMovedAway$);
    const jobs = useValue(popJobs$);
    const employed = useValue(popEmployed$);
    const unemployment = useValue(popUnemployment$);
    const homeless = useValue(popHomeless$);
    const homelessRate = useValue(popHomelessRate$);

    if (!enabled) {
        return null;
    }

    const monthly = hourlyToMonthly(delta, daysPerYear);

    const rows: ReactNode[] = [];
    if (showTrend) rows.push(<Row key="trend" label="Current monthly trend:" value={`${withSign(monthly)} /M`} color={tone(monthly)} />);
    if (showPopulation) rows.push(<Row key="pop" label="Population:" value={formatNumber(population)} color={NEUTRAL_COLOR} />);
    if (showBirthRate) rows.push(<Row key="birth" label="Birth rate:" value={`${withSign(birthRate)} /M`} color={POSITIVE_COLOR} />);
    if (showDeathRate) rows.push(<Row key="death" label="Death rate:" value={`${deathRate > 0 ? "-" : ""}${formatNumber(deathRate)} /M`} color={NEGATIVE_COLOR} />);
    if (showMovedIn) rows.push(<Row key="in" label="Moved in:" value={`${withSign(movedIn)} /M`} color={POSITIVE_COLOR} />);
    if (showMovedAway) rows.push(<Row key="away" label="Moved away:" value={`${movedAway > 0 ? "-" : ""}${formatNumber(movedAway)} /M`} color={NEGATIVE_COLOR} />);
    if (showJobs) rows.push(<Row key="jobs" label="Jobs:" value={formatNumber(jobs)} color={NEUTRAL_COLOR} />);
    if (showEmployed) rows.push(<Row key="emp" label="Employed:" value={formatNumber(employed)} color={NEUTRAL_COLOR} />);
    if (showUnemployment) rows.push(<Row key="unemp" label="Unemployment:" value={`${formatPercent(unemployment)} %`} color={NEUTRAL_COLOR} />);
    if (showHomeless) rows.push(<Row key="hl" label="Homeless:" value={formatNumber(homeless)} color={homeless > 0 ? NEGATIVE_COLOR : NEUTRAL_COLOR} />);
    if (showHomelessRate) rows.push(<Row key="hlr" label="Homeless rate:" value={`${formatPercent(homelessRate)} %`} color={homelessRate > 0 ? NEGATIVE_COLOR : NEUTRAL_COLOR} />);

    if (rows.length === 0) {
        return null;
    }

    return <Section>{rows}</Section>;
};
