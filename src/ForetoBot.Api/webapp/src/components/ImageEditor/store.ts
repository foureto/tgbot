import ContentService from "@services/Content/ContentService";
import { UploadFileRequest } from "@services/Content/models";
import {
  combine,
  createEffect,
  createEvent,
  createStore,
  sample,
} from "effector";

const saveFile = createEvent<UploadFileRequest>();
const setCallback = createEvent<() => void>();

const saveFileFx = createEffect((req: UploadFileRequest) =>
  ContentService.uploadFile(req)
);
const callbackFx = createEffect((req: () => void) => req());

const $loading = createStore<boolean>(false).on(
  saveFileFx.pending,
  (_, e) => e
);

const $callback = createStore<() => void>(() => {}).on(
  setCallback,
  // eslint-disable-next-line prettier/prettier
  (_, e) => e
);

sample({ clock: saveFile, target: saveFileFx });
sample({
  clock: saveFileFx.done,
  source: { cb: $callback },
  filter: ({ cb }) => !!cb,
  fn: ({ cb }) => cb,
  target: callbackFx,
});

const $data = combine({
  loading: $loading,
});

export { $data, saveFile, setCallback };
