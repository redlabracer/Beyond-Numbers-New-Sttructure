import { ModRegistrar } from "cs2/modding";
import MoneyTrend from "mods/money-trend";
import PopTrend from "mods/pop-trend";
import BalloonInjector from "mods/balloon-injector";
import HideInjector from "mods/hide-injector";

const register: ModRegistrar = (moduleRegistry) => {
    moduleRegistry.append("Game", MoneyTrend);
    moduleRegistry.append("Game", PopTrend);
    moduleRegistry.append("Game", BalloonInjector);
    moduleRegistry.append("Game", HideInjector);
};

export default register;
