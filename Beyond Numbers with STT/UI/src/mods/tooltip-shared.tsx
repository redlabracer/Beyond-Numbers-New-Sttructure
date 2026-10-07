import { Children, isValidElement, type ReactNode } from "react";
import { formatNumber } from "../settings";

export const POSITIVE_COLOR = "#6dd06d";
export const NEGATIVE_COLOR = "#e26b6b";
export const NEUTRAL_COLOR = "#ffffff";

export const Section = ({ children }: { readonly children: ReactNode }) => {
    return (
        <div
            style={{
                display: "flex",
                flexDirection: "column",
                gap: "2rem",
                marginTop: "6rem",
                paddingTop: "6rem",
                borderTop: "1px solid rgba(255,255,255,0.15)",
                whiteSpace: "nowrap",
            }}
        >
            {children}
        </div>
    );
};

export const Row = ({ label, value, color }: { readonly label: string; readonly value: string; readonly color: string }) => {
    return (
        <div style={{ display: "flex", justifyContent: "space-between", gap: "14rem", fontSize: "12rem" }}>
            <span style={{ opacity: 0.85 }}>{label}</span>
            <span style={{ color }}>{value}</span>
        </div>
    );
};

export const tone = (n: number): string => (n < 0 ? NEGATIVE_COLOR : POSITIVE_COLOR);

export const withSign = (value: number): string => {
    const rounded = Math.round(value);
    const prefix = rounded > 0 ? "+" : rounded < 0 ? "-" : "";
    return `${prefix}${formatNumber(value)}`;
};

export const formatPercent = (value: number): string => (Math.round(value * 10) / 10).toFixed(1);

export const containsIcon = (node: ReactNode, icon: string): boolean => {
    if (!isValidElement(node)) {
        return false;
    }

    const props = node.props as any;
    if (props?.icon === icon || props?.src === icon) {
        return true;
    }

    return Children.toArray(props?.children).some((child: ReactNode) => containsIcon(child, icon));
};
