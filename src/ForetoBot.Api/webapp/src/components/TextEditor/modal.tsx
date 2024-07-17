import React from "react";
import { useUnit } from "effector-react";
import { Form, Input, Modal } from "antd";
import { languages } from "@stores/references.store";
import { $editor, setCallback, setModal, update } from "./store";

export interface TextEditorModalProps {
  callback?: () => void;
}

const TextEditorModal: React.FC<TextEditorModalProps> = ({ callback }) => {
  const { modalOpen, currentText, loading } = useUnit($editor);
  const [form] = Form.useForm();

  React.useEffect(() => {
    form.setFieldsValue(currentText);
  }, [currentText]);

  React.useEffect(() => {
    setCallback(callback ? () => callback() : () => {});
  }, [callback]);

  const formElement = (
    <Form form={form}>
      <Form.Item name="text">
        {currentText?.wide ? <Input.TextArea rows={20} /> : <Input />}
      </Form.Item>
    </Form>
  );

  return (
    <Modal
      title={`Редактировать текст (${languages.find((e) => e.key === currentText.locale)?.label})`}
      width={750}
      height={450}
      loading={loading}
      open={modalOpen}
      onCancel={() => setModal(false)}
      onOk={() => {
        const req: any = { textId: currentText.textId, texts: {} };
        req.texts[currentText.locale] = form.getFieldValue("text");
        update(req);
      }}
    >
      {formElement}
    </Modal>
  );
};

export default TextEditorModal;
