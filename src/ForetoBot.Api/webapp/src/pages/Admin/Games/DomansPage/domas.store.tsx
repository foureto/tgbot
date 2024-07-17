import React from "react";
import {
  combine,
  createEffect,
  createEvent,
  createStore,
  sample,
} from "effector";
import TextEditor from "@components/TextEditor";
import { DomanCategory } from "@services/Games/models";
import GamesService from "@services/Games/GamesService";
import ImageEditor from "@components/ImageEditor";

const reset = createEvent();
const getData = createEvent();
const refresh = createEvent();
const createNew = createEvent<string>();

const columns = [
  {
    title: "Id",
    dataIndex: "id",
    width: 50,
  },
  {
    title: "Порядок",
    dataIndex: "order",
  },
  {
    title: "Имя",
    render: (e: DomanCategory) => (
      <TextEditor content={e.name} callback={() => refresh()} />
    ),
  },
  {
    title: "Описание",
    render: (e: DomanCategory) => (
      <TextEditor content={e.description} callback={() => refresh()} />
    ),
  },
  {
    title: "Лейбл",
    render: (e: DomanCategory) => (
      <ImageEditor small content={e.label} callback={() => refresh()} />
    ),
    width: 225,
  },
];

const getCategoriesFx = createEffect(() => GamesService.getDomanCategories());
const newCategoryFx = createEffect((name: string) =>
  GamesService.createCategory(name)
);

const $data = createStore<DomanCategory[]>([])
  .on(getCategoriesFx.doneData, (_, e) => e.data ?? [])
  .reset([getData, reset]);

const $loading = createStore<boolean>(false)
  .on(getCategoriesFx.pending, (_, e) => e)
  .reset(getData);

const $called = createStore<boolean>(false)
  .on([getCategoriesFx.done, getCategoriesFx.fail], () => true)
  .reset([getData, reset]);

sample({ clock: getData, target: getCategoriesFx });
sample({ clock: refresh, target: getData });

sample({ clock: createNew, target: newCategoryFx });
sample({ clock: newCategoryFx.doneData, target: getData });

const $categories = combine({
  data: $data,
  loading: $loading,
  called: $called,
});

export { $categories, columns, getData, refresh, reset, createNew };
