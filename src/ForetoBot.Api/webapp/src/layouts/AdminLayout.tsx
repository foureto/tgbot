import React from "react";
import { ConfigProvider, Layout } from "antd";
import { Outlet } from "react-router-dom";
import SideMenu from "@components/Admin/SideMenu";
import { appTheme } from "./theme";

const { Header, Content, Footer, Sider } = Layout;

const AdminLayout: React.FC = () => {
  return (
    <ConfigProvider theme={appTheme}>
      <Layout>
        <Header style={{ color: "#eee" }}>TG A</Header>
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
    </ConfigProvider>
  );
};
export default AdminLayout;
