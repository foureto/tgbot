import React from "react";
import { useUnit } from "effector-react";
import { Button, Upload, UploadFile } from "antd";
import { FileContent } from "@services/commons";
import { toUrl } from "@helpers/files";
import { $data, saveFile, setCallback } from "./store";
import "./editor.scss";

export interface ImageEditorProps {
  content: FileContent;
  label?: string;
  small?: boolean;
  callback?: () => void;
  onPreview?: (url?: string) => void;
}

const ImageEditor: React.FC<ImageEditorProps> = ({
  content,
  label,
  small,
  callback,
  onPreview,
}) => {
  const [fileList, setFileList] = React.useState<UploadFile[]>([]);
  const { loading } = useUnit($data);

  React.useEffect(() => {
    setCallback(callback ? () => callback() : () => {});
  }, [callback]);

  return (
    <div style={{ display: "flex", flexDirection: "column" }}>
      <div className="image-upload">
        <div className="upload-label">{label}</div>
        <Upload
          beforeUpload={() => false}
          listType="picture-card"
          fileList={fileList}
          onChange={async (e) => {
            const newList: UploadFile[] = [];
            if (e.fileList && e.fileList.length > 0) {
              const last: UploadFile | undefined =
                e.fileList[e.fileList.length - 1];
              if (last?.originFileObj && onPreview) {
                onPreview(await toUrl(last.originFileObj));
              }
              if (last) newList.push(last);
            }
            setFileList(newList);
          }}
        >
          <></>
        </Upload>
      </div>
      <Button
        loading={loading}
        disabled={fileList.length !== 1 || !fileList[0].originFileObj}
        onClick={() =>
          saveFile({
            fileId: content.id,
            file: fileList[0].originFileObj as any,
          })
        }
      >
        Сохранить
      </Button>
    </div>
  );
};

export default ImageEditor;
