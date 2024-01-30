import React from "react";
import { Outlet } from "react-router-dom";
import {
  BackButton,
  MainButton,
  WebAppProvider,
  useThemeParams,
} from "@vkruglikov/react-telegram-web-app";
import { ConfigProvider, theme } from "antd";

const DefaultLayout: React.FC = () => {
  const [colorScheme, themeParams] = useThemeParams();

  return (
    <ConfigProvider
      theme={{
        algorithm: [
          colorScheme === "dark" ? theme.darkAlgorithm : theme.defaultAlgorithm,
          theme.compactAlgorithm,
        ],
        token: {
          colorBgBase: themeParams.bg_color,
        },
      }}
    >
      <WebAppProvider
        options={{
          smoothButtonsTransition: true,
        }}
      >
        <Outlet />
        <MainButton />
        <BackButton />
      </WebAppProvider>
    </ConfigProvider>
  );
};

export default DefaultLayout;
