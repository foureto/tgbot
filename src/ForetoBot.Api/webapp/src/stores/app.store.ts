import { combine, createEvent, createStore } from "effector";
import { ColorScheme, ThemeParams } from "@vkruglikov/react-telegram-web-app";

const themeChanged = createEvent<ColorScheme>();
const themeParamsChanged = createEvent<ThemeParams>();

const $theme = createStore<ColorScheme | null>(null).on(
  themeChanged,
  (_, e) => e
);

const $themeParams = createStore<ThemeParams | null>(null).on(
  themeParamsChanged,
  (_, e) => e
);

const $app = combine({
  theme: $theme,
  themeParams: $themeParams,
});

export { $app, themeChanged, themeParamsChanged };
