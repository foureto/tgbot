import React from "react";
import { Outlet } from "react-router-dom";
import { setDebug } from "@tma.js/sdk";
import {
  SDKProvider,
  DisplayGate,
  useThemeParams,
  useLaunchParams,
} from "@tma.js/sdk-react";
import { ConfigProvider, theme } from "antd";
import GlobalLoader from "@components/GlobalLoader";

const ErrorSdk: React.FC<{ error: unknown }> = () => (
  <GlobalLoader message={"error"} />
);

const LoadingSdk: React.FC = () => <GlobalLoader message={"Loading..."} />;

const InitializingSdk: React.FC = () => (
  <GlobalLoader message={"Initializing..."} />
);

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
          colorBgBase: themeparams.get("backgroundColor"),
          colorBgContainer: themeparams.get("secondaryBackgroundColor"),
          colorPrimary: themeparams.get("textColor"),
          colorText: themeparams.get("textColor"),
          colorTextBase: themeparams.get("textColor"),
        },
      }}
    >
      <div
        className="main-container"
        style={{ color: themeparams.get("textColor") }}
      >
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
    <SDKProvider
      options={{ acceptCustomStyles: true, cssVars: true, complete: true }}
    >
      <DisplayGate
        error={ErrorSdk}
        initial={InitializingSdk}
        loading={LoadingSdk}
      >
        <Root />
      </DisplayGate>
    </SDKProvider>
  );
};

export default DefaultLayout;
