import React from "react";
import { useValue } from "cs2/api";
import { hidePopulation$, hideDemand$, hideDate$, hideTime$ } from "../settings";

/**
 * Re-implements the original Beyond Numbers "Hide Population" /
 * "Hide Demand" behavior purely from the UI side (no C# ExecuteScript).
 *
 * Instead of mutating the DOM (document.createElement / appendChild /
 * body.classList.toggle), it renders a <style> element native React way.
 * builds the CSS conditionally from the bindings.
 */
const HIDE_POP_CSS = `
.container_Kmm > button:nth-child(1) .value_ruP,
.container_Kmm > div[class*='field_']:nth-child(1) .value_ruP {
    opacity: 0 !important;
    transition: opacity 0.3s ease-in-out;
}

.container_Kmm > button:nth-child(1):hover .value_ruP,
.container_Kmm > div[class*='field_']:nth-child(1):hover .value_ruP {
    opacity: 1 !important;
}
`;

const HIDE_DEMAND_CSS = `
div[class*='city-info-field_'] > div[class*='field-new_'] > svg,
div[class*='container_'] > div[class*='field_'] > div[class*='content_'] > svg {
    opacity: 0 !important;
    transition: opacity 0.3s ease-in-out;
}

div[class*='city-info-field_']:hover > div[class*='field-new_'] > svg,
div[class*='container_'] > div[class*='field_']:hover > div[class*='content_'] > svg {
    opacity: 1 !important;
}
`;

const HIDE_TIME_CSS = `
div[class*='time-hours_'],
div[class*='time-colon_'],
div[class*='time-minutes_'] {
    opacity: 0 !important;
    transition: opacity 0.3s ease-in-out;
}

div[class*='date-time_']:hover div[class*='time-hours_'],
div[class*='date-time_']:hover div[class*='time-colon_'],
div[class*='date-time_']:hover div[class*='time-minutes_'] {
    opacity: 1 !important;
}
`;

const HIDE_DATE_CSS = `
div[class*='date_'] {
    opacity: 0 !important;
    transition: opacity 0.3s ease-in-out;
}

div[class*='date-time_']:hover div[class*='date_'] {
    opacity: 1 !important;
}
`;

const HideInjector: React.FC = () => {
    const hidePop = useValue(hidePopulation$);
    const hideDemand = useValue(hideDemand$);
    const hideDate = useValue(hideDate$);
    const hideTime = useValue(hideTime$);

    const css = [
        hidePop ? HIDE_POP_CSS : "",
        hideDemand ? HIDE_DEMAND_CSS : "",
        hideTime ? HIDE_TIME_CSS : "",
        hideDate ? HIDE_DATE_CSS : "",
    ].join("");

    if (!css) {
        return null;
    }

    return <style>{css}</style>;
};

export default HideInjector;
