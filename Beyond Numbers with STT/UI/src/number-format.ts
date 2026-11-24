import { useMemo } from "react";
import { useValue } from "cs2/api";
import { LocalizedNumber, Unit, useLocalization, type Localization } from "cs2/l10n";
import { compactNumbers$ } from "./settings";

const LOC = "BeyondNumbers.NumberFormat.";
const VALUE_TOKEN = "{VALUE}";

interface CompactTier {
    readonly threshold: number;
    readonly key: string;
    readonly fallback: string;
}

// Largest first. Like City Watchdog, compacting starts at one million so everyday values stay exact.
const TIERS: readonly CompactTier[] = [
    { threshold: 1_000_000_000_000, key: LOC + "Trillion", fallback: "{VALUE}T" },
    { threshold: 1_000_000_000, key: LOC + "Billion", fallback: "{VALUE}B" },
    { threshold: 1_000_000, key: LOC + "Million", fallback: "{VALUE}M" },
];

const GROUP_DIGITS = /\B(?=(\d{3})+(?!\d))/g;
const TRAILING_ZEROS = /\.0+$|(\.\d*?[1-9])0+$/;

const translateOr = (localization: Localization, id: string, fallback: string): string => {
    try {
        return localization.translate(id, fallback) ?? fallback;
    } catch {
        return fallback;
    }
};

// Mirrors the game's own formatter, still reading the separators from the active dictionary.
const renderFallback = (localization: Localization, magnitude: number, fractionDigits: number): string => {
    let fixed = magnitude.toFixed(fractionDigits);
    if (fractionDigits > 0) {
        fixed = fixed.replace(TRAILING_ZEROS, "$1");
    }

    const [whole, fraction] = fixed.split(".");
    const grouped = whole.replace(GROUP_DIGITS, translateOr(localization, "Common.THOUSANDS_SEPARATOR", ","));
    return fraction ? grouped + translateOr(localization, "Common.DECIMAL_SEPARATOR", ".") + fraction : grouped;
};

// Game formatting follows the selected game language, which browser-style Intl formatting does not.
const renderMagnitude = (localization: Localization, magnitude: number, fractionDigits: 0 | 2): string => {
    try {
        const unit = fractionDigits === 0 ? Unit.Integer : Unit.FloatTwoFractions;
        const rendered = LocalizedNumber.renderString(localization, { value: magnitude, unit, signed: false });
        if (rendered) {
            return rendered;
        }
    } catch {
        // Fall through to the manual formatter.
    }

    return renderFallback(localization, magnitude, fractionDigits);
};

const fractionDigitsFor = (scaled: number): 0 | 2 => (scaled >= 100 ? 0 : 2);

const roundForDisplay = (scaled: number): number =>
    fractionDigitsFor(scaled) === 0 ? Math.round(scaled) : Math.round(scaled * 100) / 100;

const applyTemplate = (template: string, magnitude: string): string =>
    template.includes(VALUE_TOKEN) ? template.replace(VALUE_TOKEN, magnitude) : magnitude + template;

const formatCompact = (localization: Localization, rounded: number): string | null => {
    for (let i = 0; i < TIERS.length; i++) {
        if (rounded < TIERS[i].threshold) {
            continue;
        }

        let tier = TIERS[i];
        let scaled = rounded / tier.threshold;

        // 999,999,999 would otherwise display as "1,000M"; promote it to "1B".
        if (i > 0 && roundForDisplay(scaled) >= 1000) {
            tier = TIERS[i - 1];
            scaled = rounded / tier.threshold;
        }

        const template = translateOr(localization, tier.key, tier.fallback);
        return applyTemplate(template, renderMagnitude(localization, scaled, fractionDigitsFor(scaled)));
    }

    return null;
};

/** Unsigned, localized magnitude of `value` (whole numbers), compacted from 1M upwards when `compact` is set. */
export const formatMagnitude = (localization: Localization, value: number, compact: boolean): string => {
    const rounded = Math.round(Math.abs(Number.isFinite(value) ? value : 0));
    return (compact && formatCompact(localization, rounded)) || renderMagnitude(localization, rounded, 0);
};

/** Like {@link formatMagnitude}, prefixed with "+" or "-" based on the displayed (rounded) value. */
export const formatSigned = (localization: Localization, value: number, compact: boolean): string => {
    const rounded = Number.isFinite(value) ? Math.round(value) : 0;
    const prefix = rounded > 0 ? "+" : rounded < 0 ? "-" : "";
    return prefix + formatMagnitude(localization, value, compact);
};

export interface NumberFormatter {
    magnitude(value: number): string;
    signed(value: number): string;
}

/** Number formatter that follows the game language and the player's compact-number setting. */
export const useNumberFormatter = (): NumberFormatter => {
    const localization = useLocalization();
    const compact = useValue(compactNumbers$);

    return useMemo<NumberFormatter>(
        () => ({
            magnitude: (value) => formatMagnitude(localization, value, compact),
            signed: (value) => formatSigned(localization, value, compact),
        }),
        [localization, compact]
    );
};
