import React from "react";
import { Layout } from "antd";
import { Outlet } from "react-router-dom";
import SideMenu from "@components/Admin/SideMenu";

const { Header, Content, Footer, Sider } = Layout;

const AdminLayout: React.FC = () => {
  return (
    <Layout>
      <Header style={{ color: "#eee" }}>qwe</Header>
      <Layout>
        <Sider style={{ padding: "10px" }} width={250}>
          <SideMenu />
        </Sider>
        <Content style={{ padding: "15px" }}>
          <Outlet />
        </Content>
        <Sider></Sider>
      </Layout>
      <Footer>(c) 2024</Footer>
    </Layout>
  );
};
export default AdminLayout;
