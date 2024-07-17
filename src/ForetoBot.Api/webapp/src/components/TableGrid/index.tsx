import React from "react";
import { Table } from "antd";
import GlobalLoader from "@components/GlobalLoader";

const transformSorter = (toTransform: any) => {
  return {
    field: toTransform.field,
    order: toTransform.order === "ascend" ? 1 : 2,
  };
};

export interface TableGridProps {
  data: any;
  columns: any;
  isLoading: boolean;
  total?: number;
  pageIndex?: number;
  pageSize?: number;
  onChange?: any;
  sortChanged?: any;
  rowKey?: string;
  selectColumn?: any;
  fixedHeight?: number;
}

const TableGrid: React.FC<TableGridProps> = (props: TableGridProps) => {
  const {
    data,
    total,
    pageIndex,
    pageSize,
    onChange,
    rowKey,
    selectColumn,
    fixedHeight,
    columns,
    isLoading = false,
  } = props;

  const tableRef = React.useRef<any>(null);
  const getTableScroll = (conf: { extraHeight?: any }) => {
    if (fixedHeight) return `${fixedHeight}px`;
    let { extraHeight } = conf;
    if (typeof extraHeight == "undefined") {
      //   Default bottom pagination 64 +  Margin 10
      extraHeight = 64;
    }
    let tHeaderBottom = 190;
    let tHeader = null;

    if (tableRef) {
      tHeader =
        tableRef?.current?.nativeElement.getElementsByClassName(
          "ant-table-thead"
        )[0];
    }
    if (tHeader) {
      tHeaderBottom = tHeader.getBoundingClientRect().bottom;
    }

    // Form height - The height of the top of the table content - The height of the bottom of the table content
    // let height = document.body.clientHeight - tHeaderBottom - extraHeight
    return `calc(100vh - ${tHeaderBottom + extraHeight}px)`;
  };

  return (
    <Table
      tableLayout="fixed"
      ref={tableRef}
      size="small"
      loading={isLoading ? { indicator: <GlobalLoader size="50px" /> } : false}
      bordered
      rowKey={rowKey ?? "id"}
      columns={selectColumn ? [...columns, selectColumn] : columns}
      dataSource={data}
      pagination={
        onChange
          ? {
              size: "small",
              total: total,
              current: pageIndex,
              pageSize: pageSize,
              defaultPageSize: 10,
              pageSizeOptions: ["10", "20", "50", "100"],
              showSizeChanger: true,
              showTotal: (total) => `Total: ${total}`,
            }
          : false
      }
      onChange={(pagination, _, sorter: any) => {
        onChange({
          pageIndex: pagination.current,
          pageSize: pagination.pageSize,
          sort: sorter.order ? [transformSorter(sorter)] : [],
        });
      }}
      scroll={{ y: getTableScroll({}), x: "max-content" }}
    />
  );
};

export default TableGrid;
