import React, { useEffect } from "react";
import { useValue } from "cs2/api";
import { hidePopulation$, hideDemand$, hideDate$, hideTime$ } from "../settings";

/**
 * Re-implements the original Beyond Numbers "Hide Population" /
 * "Hide Demand" behavior purely from the UI side (no C# ExecuteScript).
 *
 * It injects a <style> tag with the same CSS rules the original mod used,
 * and toggles two body classes based on the bindings.
 */
const STYLE_ID = "beyond-numbers-style";
const CSS = `
body.beyond-numbers-hide-pop .container_Kmm > button:nth-child(1) .value_ruP,
body.beyond-numbers-hide-pop .container_Kmm > div[class*='field_']:nth-child(1) .value_ruP {
    opacity: 0 !important;
    transition: opacity 0.3s ease-in-out;
}

body.beyond-numbers-hide-pop .container_Kmm > button:nth-child(1):hover .value_ruP,
body.beyond-numbers-hide-pop .container_Kmm > div[class*='field_']:nth-child(1):hover .value_ruP {
    opacity: 1 !important;
}

body.beyond-numbers-hide-demand div[class*='city-info-field_'] > div[class*='field-new_'] > svg,
body.beyond-numbers-hide-demand div[class*='container_'] > div[class*='field_'] > div[class*='content_'] > svg {
    opacity: 0 !important;
    transition: opacity 0.3s ease-in-out;
}

body.beyond-numbers-hide-demand div[class*='city-info-field_']:hover > div[class*='field-new_'] > svg,
body.beyond-numbers-hide-demand div[class*='container_'] > div[class*='field_']:hover > div[class*='content_'] > svg {
    opacity: 1 !important;
}

body.beyond-numbers-hide-time div[class*='time-hours_'],
body.beyond-numbers-hide-time div[class*='time-colon_'],
body.beyond-numbers-hide-time div[class*='time-minutes_'] {
    opacity: 0 !important;
    transition: opacity 0.3s ease-in-out;
}

body.beyond-numbers-hide-time div[class*='date-time_']:hover div[class*='time-hours_'],
body.beyond-numbers-hide-time div[class*='date-time_']:hover div[class*='time-colon_'],
body.beyond-numbers-hide-time div[class*='date-time_']:hover div[class*='time-minutes_'] {
    opacity: 1 !important;
}

body.beyond-numbers-hide-date div[class*='date_'] {
    opacity: 0 !important;
    transition: opacity 0.3s ease-in-out;
}

body.beyond-numbers-hide-date div[class*='date-time_']:hover div[class*='date_'] {
    opacity: 1 !important;
}
`;

function ensureStyle() {
    let el = document.getElementById(STYLE_ID) as HTMLStyleElement | null;
    if (!el) {
        el = document.createElement("style");
        el.id = STYLE_ID;
        el.innerHTML = CSS;
        document.head.appendChild(el);
    } else if (el.innerHTML !== CSS) {
        el.innerHTML = CSS;
    }
}

const HideInjector: React.FC = () => {
    const hidePop = useValue(hidePopulation$);
    const hideDemand = useValue(hideDemand$);
    const hideDate = useValue(hideDate$);
    const hideTime = useValue(hideTime$);

    useEffect(() => {
        ensureStyle();
        document.body.classList.toggle("beyond-numbers-hide-pop", hidePop);
        document.body.classList.toggle("beyond-numbers-hide-demand", hideDemand);
        document.body.classList.toggle("beyond-numbers-hide-date", hideDate);
        document.body.classList.toggle("beyond-numbers-hide-time", hideTime);
    }, [hidePop, hideDemand, hideDate, hideTime]);

    return null;
};

export default HideInjector;
