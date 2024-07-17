import { combine, createStore, createEvent, Effect, sample } from "effector";
import {
  Filter,
  HandlerProps,
  IPagedResult,
  Sort,
  transformFilter,
} from "@services/commons";
import { debounce } from "patronum/debounce";

export const tableFactory = <TR, T>(
  handler: Effect<HandlerProps<TR>, IPagedResult<T>, Error>,
  columns: any[],
  hiddenColumns: string[] = [],
  // eslint-disable-next-line prettier/prettier
  defaultFilter?: Filter
) => {
  const $data = createStore<T[]>([]);
  const $pageIndex = createStore<number>(1);
  const $pageSize = createStore<number>(20);
  const $total = createStore<number>(0);
  const $columns = createStore<any[]>(columns);
  const $hiddenColumns = createStore<string[]>(hiddenColumns);
  const $filter = createStore<Filter[]>(defaultFilter ? [defaultFilter] : []);
  const $sort = createStore<Sort[]>([]);
  const $isTableRendered = createStore<boolean>(false);
  const $dataIsLoading = handler.pending;
  //event for unmount effect
  const tableUnmounted = createEvent();
  //event for the first render
  const tableRendered = createEvent();

  const $visibleColumns = combine(
    $columns,
    $hiddenColumns,
    (originalColumns, hiddenColumns) =>
      originalColumns.filter((f) => !hiddenColumns.includes(f.dataIndex))
  );
  const hiddenColumnsChanged = createEvent<string[]>();

  $hiddenColumns.on(hiddenColumnsChanged, (_, state) => state);
  $data.on(tableUnmounted, () => []);
  $data.on(handler.doneData, (_, payload) => payload.data);
  $total.on(handler.doneData, (_, payload) => payload.total);
  $pageIndex.on(handler.doneData, (_, payload) => payload.page);
  $pageSize.on(handler.doneData, (_, payload) => payload.count);

  sample({
    clock: tableRendered,
    source: {
      pageIndex: $pageIndex,
      pageSize: $pageSize,
      filter: $filter,
      listSort: $sort,
      isTableRendered: $isTableRendered,
    },
    filter: ({ isTableRendered }) => !isTableRendered,
    fn({ pageIndex, pageSize, filter, listSort }) {
      return { pageIndex, pageSize, filter: transformFilter(filter), listSort };
    },
    target: handler,
  });

  $isTableRendered.on(handler.doneData, () => true);
  $isTableRendered.on(tableUnmounted, () => false);

  // event to fetch data
  const fetchData = createEvent();
  sample({
    clock: fetchData,
    source: {
      pageIndex: $pageIndex,
      pageSize: $pageSize,
      filter: $filter,
      listSort: $sort,
    },
    fn({ pageIndex, pageSize, filter, listSort }) {
      return { pageIndex, pageSize, filter: transformFilter(filter), listSort };
    },
    target: handler,
  });

  const sortChanged = createEvent<{ listSort: Sort[] }>();
  const dirtySortChanged = createEvent<string>();
  $sort.on(sortChanged, (_, state) => state.listSort);
  $sort.on(dirtySortChanged, (prev, state) => {
    if (state) {
      const sorts = state.split(";");
      return sorts.map((val) => {
        const srt = val.split("_");
        return {
          field: srt[0],
          order: Number(srt[1]),
        };
      });
    }
    return prev;
  });

  // one filter changed with debounce
  const oneFilterChanged = createEvent<Filter[]>();
  const oneFilterChangedDebounced = debounce({
    source: oneFilterChanged,
    timeout: 1000,
  });

  $filter.on(oneFilterChangedDebounced, (prev, state) => {
    const prevFilters = prev.filter(
      (f) => state.find((s) => s.field === f.field) === undefined
    );
    const stateFilters = state.filter((f) => f.value !== "");
    return [...prevFilters, ...stateFilters];
  });

  sample({
    clock: oneFilterChangedDebounced,
    source: {
      pageIndex: $pageIndex,
      pageSize: $pageSize,
      listSort: $sort,
      oldFilter: $filter,
    },
    fn({ pageIndex, pageSize, listSort, oldFilter }, payload) {
      const oldFilters = oldFilter.filter(
        (f) => payload.find((s) => s.field === f.field) === undefined
      );
      const payloadFilters = payload.filter((f) => f.value !== "");
      return {
        pageIndex,
        pageSize,
        listSort,
        filter: transformFilter([...oldFilters, ...payloadFilters]),
      };
    },
    target: [handler],
  });

  // filter changed without debounce
  const filterChanged = createEvent<Filter[]>();

  $filter.on(filterChanged, (prev, state) => {
    const prevFilters = prev.filter(
      (f) => state.find((s) => s.field === f.field) === undefined
    );
    const stateFilters = state.filter((f) => f.value !== "");
    return [...prevFilters, ...stateFilters];
  });

  // all filters changed
  const filtersChanged = createEvent<Filter[]>();
  const filtersChangedDebounced = debounce({
    source: filtersChanged,
    timeout: 1000,
  });
  const dirtyFilterChanged = createEvent<string>();

  $filter.on(filtersChangedDebounced, (prev, state) => state);
  $filter.on(dirtyFilterChanged, (prev, state) => {
    if (state) {
      const filters = state.split(";");
      return filters.map((val) => {
        const srt = val.split("_");

        const transformDirtyValue = (value: any) => {
          if (srt[1] === "true") return true;
          if (srt[1] === "false") return false;

          return value;
        };

        return {
          field: srt[0],
          value: transformDirtyValue(srt[1]),
        };
      });
    }
    return prev;
  });

  sample({
    clock: filtersChangedDebounced,
    source: {
      pageIndex: $pageIndex,
      pageSize: $pageSize,
      listSort: $sort,
    },
    fn({ pageIndex, pageSize, listSort }, payload) {
      return {
        pageIndex,
        pageSize,
        listSort,
        filter: transformFilter(payload),
      };
    },
    target: [handler],
  });

  const paginationChanged = createEvent<{
    pageIndex: number;
    pageSize: number;
    sort: Sort[];
  }>();
  sample({
    clock: paginationChanged,
    source: {
      filter: $filter,
    },
    fn({ filter }, payload) {
      return {
        pageIndex: payload.pageIndex,
        pageSize: payload.pageSize,
        filter: transformFilter(filter),
        listSort: payload.sort,
      };
    },
    target: [handler, sortChanged],
  });

  const $grid = combine({
    data: $data,
    columns: $visibleColumns,
    total: $total,
    pageIndex: $pageIndex,
    pageSize: $pageSize,
    filter: $filter,
    sort: $sort,
    loading: $dataIsLoading,
  });

  return {
    $grid,
    $data,
    $total,
    $pageIndex,
    $pageSize,
    $sort,
    $filter,
    $visibleColumns,
    $hiddenColumns,
    $columns,
    $dataIsLoading,
    paginationChanged,
    tableRendered,
    tableUnmounted,
    hiddenColumnsChanged,
    filtersChanged,
    filterChanged,
    oneFilterChanged,
    fetchData,
  };
};
