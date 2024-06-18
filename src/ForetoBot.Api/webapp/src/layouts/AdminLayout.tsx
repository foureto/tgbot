import { Layout } from "antd";
import Sider from "antd/es/layout/Sider";
import React from "react";
import { Outlet } from "react-router-dom";

const { Header, Content, Footer } = Layout;

const items = new Array(3).fill(null).map((_, index) => ({
  key: String(index + 1),
  label: `nav ${index + 1}`,
}));

const AdminLayout: React.FC = () => {
  return (
    <Layout>
      <Header>header</Header>
      <Layout>
        <Sider>left sidebar</Sider>
        <Content>
          <Outlet />
        </Content>
        <Sider>right sidebar</Sider>
      </Layout>
      <Footer>footer</Footer>
    </Layout>
  );
};
export default AdminLayout;
