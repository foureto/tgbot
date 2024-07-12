import React from "react";
import { Outlet } from "react-router-dom";
import { setDebug } from "@tma.js/sdk";
import {
  SDKProvider,
  useThemeParams,
  useLaunchParams,
} from "@tma.js/sdk-react";
import { ConfigProvider, theme } from "antd";

const Root: React.FC = () => {
  const themeparams = useThemeParams();
  const antTheme = themeparams.isDark
    ? theme.darkAlgorithm
    : theme.defaultAlgorithm;

  return (
    <ConfigProvider
      theme={{
        algorithm: [antTheme, theme.compactAlgorithm],
        token: {
          colorBgBase: themeparams.bgColor,
          colorBgContainer: themeparams.secondaryBgColor,
          colorPrimary: themeparams.textColor,
          colorText: themeparams.textColor,
          colorTextBase: themeparams.textColor,
        },
      }}
    >
      <div className="main-container" style={{ color: themeparams.textColor }}>
        <Outlet />
      </div>
    </ConfigProvider>
  );
};

const DefaultLayout: React.FC = () => {
  const launchParams = useLaunchParams();

  React.useEffect(() => {
    setDebug(true);
    import("eruda").then((lib) => lib.default.init());
  }, [launchParams]);

  return (
    <SDKProvider acceptCustomStyles={true}>
      <Root />
    </SDKProvider>
  );
};

export default DefaultLayout;
