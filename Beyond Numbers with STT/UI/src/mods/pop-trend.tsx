import React, { useEffect } from "react";
import { useValue } from "cs2/api";
import { toolbarBottom } from "cs2/bindings";
import {
    showPopTrendHourly$,
    showPopTrendMonthly$,
    daysPerYear$,
    hourlyToMonthly,
    formatNumber,
} from "../settings";

const HOURLY_CLASS = "bn-pop-delta-hourly";
const MONTHLY_CLASS = "bn-pop-delta-monthly";
const SEP_CLASS = "bn-pop-delta-sep";

const PopTrend: React.FC = () => {
    const popDelta = useValue(toolbarBottom.populationDelta$);
    const showHourly = useValue(showPopTrendHourly$);
    const showMonthly = useValue(showPopTrendMonthly$);
    const daysPerYear = useValue(daysPerYear$);

    useEffect(() => {
        const trendElements = document.querySelectorAll(".trend_IAr");
        if (trendElements.length < 1) return;
        const anchor = trendElements[0];
        const sign = popDelta < 0 ? "negative_Moc" : "positive_n5t";

        upsertAfterAnchor(anchor, HOURLY_CLASS, sign,
            showHourly ? `${formatNumber(popDelta)} /h` : null);

        const sepNeeded = showHourly && showMonthly;
        upsertAfterHourly(anchor, SEP_CLASS, "",
            sepNeeded ? " · " : null);

        const monthly = hourlyToMonthly(popDelta, daysPerYear);
        upsertAfterSep(anchor, MONTHLY_CLASS, sign,
            showMonthly ? `${formatNumber(monthly)} /M` : null);
    }, [popDelta, showHourly, showMonthly, daysPerYear]);

    return null;
};

function upsertAfterAnchor(anchor: Element, marker: string, sign: string, text: string | null) {
    let node = anchor.parentElement?.querySelector(`.${marker}`) as HTMLElement | null;
    if (text === null) { node?.remove(); return; }
    if (!node) {
        node = document.createElement("span");
        anchor.insertAdjacentElement("afterend", node);
    }
    node.className = `${marker} ${sign}`;
    node.textContent = text;
}

function upsertAfterHourly(anchor: Element, marker: string, sign: string, text: string | null) {
    const parent = anchor.parentElement;
    let node = parent?.querySelector(`.${marker}`) as HTMLElement | null;
    if (text === null) { node?.remove(); return; }
    if (!node) {
        node = document.createElement("span");
        const hourly = parent?.querySelector(`.${HOURLY_CLASS}`);
        const ref = hourly ?? anchor;
        ref.insertAdjacentElement("afterend", node);
    }
    node.className = marker;
    node.textContent = text;
    node.style.opacity = "0.5";
}

function upsertAfterSep(anchor: Element, marker: string, sign: string, text: string | null) {
    const parent = anchor.parentElement;
    let node = parent?.querySelector(`.${marker}`) as HTMLElement | null;
    if (text === null) { node?.remove(); return; }
    if (!node) {
        node = document.createElement("span");
        const sep = parent?.querySelector(`.${SEP_CLASS}`);
        const hourly = parent?.querySelector(`.${HOURLY_CLASS}`);
        const ref = sep ?? hourly ?? anchor;
        ref.insertAdjacentElement("afterend", node);
    }
    node.className = `${marker} ${sign}`;
    node.textContent = text;
}

export default PopTrend;
