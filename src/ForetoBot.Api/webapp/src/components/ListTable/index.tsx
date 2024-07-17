import React from "react";
import { Table } from "antd";
import { ColumnsType } from "antd/es/table";

export interface ListTableProps<T> {
  data: T[];
  columns: ColumnsType<T>;
  keyIndex?: string;
}

const ListTable = <T extends object>(props: ListTableProps<T>) => {
  const { data, columns, keyIndex } = props;
  return (
    <Table
      size="small"
      style={{ width: "100%" }}
      dataSource={data}
      columns={columns}
      pagination={{
        size: "small",
        total: data.length,
        pageSize: 20,
        pageSizeOptions: ["10", "20", "50", "100"],
        showTotal: (total) => `Total: ${total}`,
      }}
      rowKey={keyIndex ?? "id"}
    />
  );
};

export default ListTable;
