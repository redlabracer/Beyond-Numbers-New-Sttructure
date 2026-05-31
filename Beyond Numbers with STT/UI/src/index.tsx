import { ModRegistrar } from "cs2/modding";
import { StatFieldTrendExtension } from "mods/toolbar-trend";
import { DescriptionTooltipExtension } from "mods/money-tooltip";
import HideInjector from "mods/hide-injector";

const STAT_FIELD_MODULE = "game-ui/game/components/toolbar/components/stat-field/stat-field.tsx";
const STAT_FIELD_TREND_EXPORT = "StatFieldTrend";
const DESCRIPTION_TOOLTIP_MODULE = "game-ui/common/tooltip/description-tooltip/description-tooltip.tsx";
const DESCRIPTION_TOOLTIP_EXPORT = "DescriptionTooltip";

const register: ModRegistrar = (moduleRegistry) => {
    moduleRegistry.extend(STAT_FIELD_MODULE, STAT_FIELD_TREND_EXPORT, StatFieldTrendExtension);

    moduleRegistry.extend(DESCRIPTION_TOOLTIP_MODULE, DESCRIPTION_TOOLTIP_EXPORT, DescriptionTooltipExtension);

    moduleRegistry.append("Game", HideInjector);
};

export default register;
