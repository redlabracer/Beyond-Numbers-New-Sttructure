import React, { useEffect, useRef, useCallback } from "react";
import { useValue } from "cs2/api";
import { economyBudget, toolbarBottom } from "cs2/bindings";
import {
    cityWatchdogInstalled$,
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


const STYLE_ID = "bn-balloon-style";
const ROW_CLASS = "bn-balloon-row";
const SECTION_CLASS = "bn-balloon-section";
const POSITIVE_CLASS = "bn-balloon-positive";
const NEGATIVE_CLASS = "bn-balloon-negative";

const STYLE = `
[class*='balloon_'] {
  height: auto !important;
  max-height: none !important;
  overflow: visible !important;
}
.${SECTION_CLASS} {
  display: flex;
  flex-direction: column;
  gap: 2rem;
  margin: 6rem -8rem -4rem -8rem;
  padding: 6rem 10rem;
  border-top: 1px solid rgba(255,255,255,0.15);
  background: rgba(15, 20, 30, 0.92);
  border-radius: 0 0 4rem 4rem;
  white-space: nowrap;
}
.${ROW_CLASS} {
  display: flex;
  justify-content: space-between;
  gap: 14rem;
  font-size: 12rem;
  line-height: 1.3;
}
.${ROW_CLASS} > span:first-child { opacity: 0.85; }
.${POSITIVE_CLASS} { color: #6dd06d; }
.${NEGATIVE_CLASS} { color: #e26b6b; }
`;

interface PopState {
    enabled: boolean;
    showMonthly: boolean;
    delta: number;
    daysPerYear: number;
}

interface MoneyState {
    enabled: boolean;
    showIncome: boolean;
    showExpense: boolean;
    showNet: boolean;
    showHourly: boolean;
    showMonthly: boolean;
    delta: number;
    income: number;
    expense: number;
    daysPerYear: number;
}

const BalloonInjector: React.FC = () => {
    const cityWatchdogInstalled = useValue(cityWatchdogInstalled$);

    const popDelta = useValue(toolbarBottom.populationDelta$);
    const showPopMonthly = useValue(showPopTrendMonthly$);

    const moneyEnabled = useValue(enableMoneyTooltip$);
    const moneyDelta = useValue(toolbarBottom.moneyDelta$);
    const showIncome = useValue(showTooltipIncome$);
    const showExpense = useValue(showTooltipExpense$);
    const showNet = useValue(showTooltipNet$);
    const showHourly = useValue(showTooltipHourlyValues$);
    const showMonthly = useValue(showTooltipMonthlyValues$);
    const income = useValue(economyBudget.totalIncome$);
    const expense = useValue(economyBudget.totalExpenses$);
    const daysPerYear = useValue(daysPerYear$);

    const popStateRef = useRef<PopState | null>(null);
    const moneyStateRef = useRef<MoneyState | null>(null);
    const observerRef = useRef<MutationObserver | null>(null);

    const handleBalloon = useCallback((balloon: HTMLElement) => {
        updateBalloon(balloon, popStateRef.current, moneyStateRef.current);
    }, []);

    useEffect(() => {
        ensureStyle();

        // Master OFF or City Watchdog installed means no extra tooltip rows.
        const effectiveTooltipEnabled = moneyEnabled && !cityWatchdogInstalled;
        const effectivePopMonthly = effectiveTooltipEnabled && showPopMonthly;

        popStateRef.current = {
            enabled: effectivePopMonthly,
            showMonthly: effectivePopMonthly,
            delta: popDelta,
            daysPerYear,
        };
        moneyStateRef.current = {
            enabled: effectiveTooltipEnabled,
            showIncome,
            showExpense,
            showNet,
            showHourly,
            showMonthly,
            delta: moneyDelta,
            income,
            expense,
            daysPerYear,
        };

        if (!observerRef.current) {
            observerRef.current = new MutationObserver((mutations) => {
                for (const m of mutations) {
                    m.addedNodes.forEach((n) => {
                        if (!(n instanceof HTMLElement)) return;
                        if (isBalloon(n)) handleBalloon(n);
                        n.querySelectorAll<HTMLElement>("[class*='balloon_']").forEach(handleBalloon);
                    });
                }
            });
            observerRef.current.observe(document.body, { childList: true, subtree: true });
        }

        document.querySelectorAll<HTMLElement>("[class*='balloon_']").forEach(handleBalloon);

        return () => {
            observerRef.current?.disconnect();
            observerRef.current = null;
        };
    }, [
        cityWatchdogInstalled,
        popDelta, showPopMonthly,
        moneyEnabled, moneyDelta, showIncome, showExpense, showNet,
        showHourly, showMonthly, income, expense, daysPerYear,
        handleBalloon,
    ]);

    return null;
};

function ensureStyle() {
    if (document.getElementById(STYLE_ID)) return;
    const s = document.createElement("style");
    s.id = STYLE_ID;
    s.textContent = STYLE;
    document.head.appendChild(s);
}

function isBalloon(el: HTMLElement): boolean {
    return /\bballoon_/.test(el.className);
}

function updateBalloon(balloon: HTMLElement, pop: PopState | null, money: MoneyState | null) {
    const text = balloon.textContent ?? "";
    if (/POPULATION/i.test(text) || /citizens/i.test(text)) {
        injectPopulation(balloon, pop);
    } else if (/MONEY/i.test(text) || /available funds/i.test(text)) {
        injectMoney(balloon, money);
    }
}

function ensureSection(balloon: HTMLElement): HTMLElement {
    let section = balloon.querySelector<HTMLElement>(`.${SECTION_CLASS}`);
    if (!section) {
        section = document.createElement("div");
        section.className = SECTION_CLASS;
        const host =
            balloon.querySelector<HTMLElement>("[class*='content_']") ??
            (balloon.firstElementChild as HTMLElement | null) ??
            balloon;
        host.appendChild(section);
    }
    section.innerHTML = "";
    return section;
}

function row(label: string, value: string, signClass: string): string {
    return `<div class="${ROW_CLASS}"><span>${label}</span><span class="${signClass}">${value}</span></div>`;
}

function signOf(n: number): string {
    return n < 0 ? NEGATIVE_CLASS : POSITIVE_CLASS;
}

function withSign(n: number): string {
    const rounded = Math.round(n);
    const prefix = rounded > 0 ? "+" : rounded < 0 ? "-" : "";
    return `${prefix}${formatNumber(n)}`;
}

function injectPopulation(balloon: HTMLElement, s: PopState | null) {
    if (!s || !s.enabled || !s.showMonthly) {
        balloon.querySelector(`.${SECTION_CLASS}`)?.remove();
        return;
    }
    const section = ensureSection(balloon);
    const monthly = hourlyToMonthly(s.delta, s.daysPerYear);
    section.innerHTML = row("Current monthly trend:", `${withSign(monthly)} /M`, signOf(monthly));
}

function injectMoney(balloon: HTMLElement, s: MoneyState | null) {
    if (!s || !s.enabled) {
        balloon.querySelector(`.${SECTION_CLASS}`)?.remove();
        return;
    }
    const section = ensureSection(balloon);

    const incomeM = Math.abs(s.income);
    const expenseM = Math.abs(s.expense);
    const netM = incomeM - expenseM;
    const incomeH = monthlyToHourly(incomeM);
    const expenseH = monthlyToHourly(expenseM);

    let html = "";
    if (s.showNet && s.showMonthly) {
        html += row("Current monthly trend:", `${withSign(netM)} /M`, signOf(netM));
    }
    if (s.showIncome && s.showMonthly) {
        html += row("Current monthly income:", `${withSign(incomeM)} /M`, POSITIVE_CLASS);
    }
    if (s.showExpense && s.showMonthly) {
        html += row("Current monthly expenses:", `-${formatNumber(expenseM)} /M`, NEGATIVE_CLASS);
    }
    if (s.showHourly && s.showIncome) {
        html += row("Current hourly income:", `${withSign(incomeH)} /h`, POSITIVE_CLASS);
    }
    if (s.showHourly && s.showExpense) {
        html += row("Current hourly expenses:", `-${formatNumber(expenseH)} /h`, NEGATIVE_CLASS);
    }
    section.innerHTML = html;
}

export default BalloonInjector;
