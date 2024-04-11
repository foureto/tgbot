import React from "react";
import { Outlet } from "react-router-dom";
import {
  BackButton,
  MainButton,
  WebAppProvider,
  useThemeParams,
  useWebApp,
} from "@vkruglikov/react-telegram-web-app";
import { ConfigProvider, theme } from "antd";
import { themeChanged, themeParamsChanged } from "@stores/app.store";

const DefaultLayout: React.FC = () => {
  const WebApp = useWebApp();
  const [colorScheme, themeParams] = useThemeParams();

  React.useEffect(() => {
    if (colorScheme && themeParams) {
      alert(1);
      themeChanged(colorScheme);
      themeParamsChanged(themeParams);
    }
  }, [colorScheme, themeParams]);

  const antTheme =
    colorScheme === "dark" ? theme.darkAlgorithm : theme.defaultAlgorithm;

  return (
    <ConfigProvider
      theme={
        themeParams?.text_color
          ? {
              algorithm: [theme.compactAlgorithm, antTheme],
              token: {
                colorBgBase: themeParams.bg_color,
              },
            }
          : undefined
      }
    >
      <WebAppProvider
        options={{
          smoothButtonsTransition: true,
        }}
      >
        <div className="main-container">
          {WebApp.version}
          <Outlet />
        </div>
        <MainButton />
        <BackButton />
      </WebAppProvider>
    </ConfigProvider>
  );
};

export default DefaultLayout;
