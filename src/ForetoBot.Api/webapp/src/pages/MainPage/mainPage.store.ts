import {
  combine,
  createEffect,
  createEvent,
  createStore,
  sample,
} from "effector";
import TestService from "@services/Test/TestService";
import { DataItem } from "@services/Test/models";

const weatherRequested = createEvent();

const getDataFx = createEffect(() => TestService.getData());

const $weather = createStore<DataItem[]>(null as any).on(
  getDataFx.doneData,
  (_, e) => e.data,
);

const $loading = createStore<boolean>(false)
  .on(getDataFx.pending, (_, e) => e)
  .reset([getDataFx.done, getDataFx.fail]);

sample({ clock: weatherRequested, target: getDataFx });

const $data = combine({
  weather: $weather,
  loading: $loading,
});

export { $data, weatherRequested };
