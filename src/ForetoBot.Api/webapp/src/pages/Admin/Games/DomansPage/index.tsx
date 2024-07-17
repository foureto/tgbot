import React from "react";
import { useUnit } from "effector-react";
import Page from "@components/Page";
import ListTable from "@components/ListTable";
import { $categories, columns, createNew, getData } from "./domas.store";
import { Button, Input, Modal, Space } from "antd";

const PageForm: React.FC = () => {
  const [modalOpen, setOpen] = React.useState(false);
  const [newName, setName] = React.useState("");

  return (
    <Space>
      <Button type="primary" onClick={() => setOpen(true)}>
        Создать
      </Button>
      <Modal
        open={modalOpen}
        title={"Имя категории"}
        onCancel={() => setOpen(false)}
        onOk={() => {
          createNew(newName);
          setOpen(false);
        }}
      >
        <Input onChange={(e) => setName(e.target.value)} />
      </Modal>
    </Space>
  );
};

const DomansPage: React.FC = () => {
  const { data, loading, called } = useUnit($categories);

  React.useEffect(() => {
    if (data.length === 0 && !called) {
      getData();
    }
  }, [data, called]);

  return (
    <Page title="Карточки домана" loading={loading} extra={<PageForm />}>
      <ListTable columns={columns} data={data} keyIndex="id" />
    </Page>
  );
};

export default DomansPage;
