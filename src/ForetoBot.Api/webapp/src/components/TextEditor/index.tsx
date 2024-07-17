import React from "react";
import { Button, Space } from "antd";
import { TextContent } from "@services/commons";
import { languages } from "@stores/references.store";
import TextEditorModal from "./modal";
import { setCurrent, setModal } from "./store";

export interface TextEditorProps {
  content: TextContent;
  label?: string;
  callback?: () => void;
}

const TextEditor: React.FC<TextEditorProps> = (props: TextEditorProps) => {
  const { content, label, callback } = props;

  const labelButton = React.useMemo(() => {
    if (!label) return null;
    return (
      <Button style={{ width: "125px", alignItems: "end" }}>{label}:</Button>
    );
  }, [label]);

  const buttons = React.useMemo(() => {
    const result = languages.map((e) => {
      const danger = !(content as any)[e.key] || (content as any)[e.key] === "";

      return (
        <Button
          key={e.key}
          danger={danger}
          onClick={() => {
            setCurrent({
              locale: e.key,
              textId: content.id,
              text: (content as any)[e.key],
              wide: content.isLong,
            });
            setModal(true);
          }}
        >
          {e.label}
        </Button>
      );
    });
    return result;
  }, [content]);

  return (
    <>
      <Space.Compact block>
        {labelButton}
        {buttons}
      </Space.Compact>
      <TextEditorModal callback={callback} />
    </>
  );
};

export default TextEditor;
