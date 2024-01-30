import {
  combine,
  createEffect,
  createEvent,
  createStore,
  sample,
} from "effector";
import TestService from "@services/Test/TestService";
import { WeatherItem } from "@services/Test/models";

const weatherRequested = createEvent();

const getWeatherFx = createEffect(async () => await TestService.getWeather());

const $weather = createStore<WeatherItem[]>(null as any).on(
  getWeatherFx.doneData,
  (_, e) => e,
);

const $loading = createStore<boolean>(false)
  .on(getWeatherFx.pending, (_, e) => e)
  .reset([getWeatherFx.done, getWeatherFx.fail]);

sample({ clock: weatherRequested, target: getWeatherFx });

const $data = combine({
  weather: $weather,
  loading: $loading,
});

export { $data, weatherRequested };
