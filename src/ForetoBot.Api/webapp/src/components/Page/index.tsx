import React, { ReactNode } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Col, Divider, Row, Space } from "antd";
import { ArrowLeftOutlined } from "@ant-design/icons";
import GlobalLoader from "@components/GlobalLoader";

export interface PageProps {
  loading?: boolean;
  extra?: ReactNode | ReactNode[];
  title?: string;
  backPath?: string;
  children: ReactNode | ReactNode[] | undefined;
}

const Page: React.FC<PageProps> = (props: PageProps) => {
  const { loading, extra, title, children, backPath } = props;

  const navigate = useNavigate();

  React.useEffect(() => {
    document.title = `TG A${title ? ` - ${title}` : ""}`;
  }, [title]);

  return (
    <Row gutter={[16, 16]}>
      <Col span={24}>
        <Row>
          <Col span={12}>
            <Space size="middle" key="1">
              <Button
                type="primary"
                shape="circle"
                icon={<ArrowLeftOutlined />}
                onClick={() => (backPath ? navigate(backPath) : navigate(-1))}
              />
              <span style={{ fontWeight: 300 }}>{title ?? "Page"}</span>
            </Space>
          </Col>
          <Col span={12} style={{ textAlign: "right" }}>
            {extra}
          </Col>
        </Row>
        <Divider />
        <Row>
          <div style={{ width: "100%" }}>
            {loading !== undefined && loading ? (
              <GlobalLoader size="150px" message="Загрузка..." />
            ) : (
              children
            )}
          </div>
        </Row>
      </Col>
    </Row>
  );
};

export default Page;
