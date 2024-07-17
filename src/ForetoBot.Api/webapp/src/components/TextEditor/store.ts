import ContentService from "@services/Content/ContentService";
import { UpdateTextRequest } from "@services/Content/models";
import {
  combine,
  createEffect,
  createEvent,
  createStore,
  sample,
} from "effector";

export interface ModalData {
  textId: string;
  locale: string;
  text: string;
  wide: boolean;
}

const setModal = createEvent<boolean>();
const setCurrent = createEvent<ModalData>();
const setCallback = createEvent<() => void>();
const update = createEvent<UpdateTextRequest>();

const updateFx = createEffect((req: UpdateTextRequest) =>
  // eslint-disable-next-line prettier/prettier
  ContentService.updateText(req)
);

const callbackFx = createEffect((req: () => void) => {
  req();
});

const $loading = createStore<boolean>(false).on(updateFx.pending, (_, e) => e);
const $modalOpen = createStore<boolean>(false).on(setModal, (_, e) => e);
const $currentText = createStore<ModalData>({} as ModalData).on(
  setCurrent,
  // eslint-disable-next-line prettier/prettier
  (_, e) => e
);

const $callback = createStore<() => void>(() => {}).on(
  setCallback,
  // eslint-disable-next-line prettier/prettier
  (_, e) => e
);

const $editor = combine({
  modalOpen: $modalOpen,
  currentText: $currentText,
  loading: $loading,
});

sample({ clock: update, target: updateFx });
sample({
  clock: [updateFx.done, updateFx.fail],
  fn: () => false,
  target: setModal,
});
sample({
  clock: updateFx.done,
  source: { cb: $callback },
  filter: ({ cb }) => !!cb,
  fn: ({ cb }) => cb,
  target: callbackFx,
});

export { $editor, setModal, setCurrent, setCallback, update };
