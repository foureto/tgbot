import React from "react";
import { RouteObject } from "react-router-dom";

import DefaultLayout from "../layouts/DefaultLayout";
import MainPage from "@pages/MainPage";
import ComponentsPage from "@pages/ComponentsPage";
import GlobalLoader from "@components/GlobalLoader";

const publicRoutes: RouteObject[] = [
  {
    path: "",
    element: <DefaultLayout />,
    errorElement: <GlobalLoader message="Ooops..." />,
    loader: () => {
      return true;
    },
    children: [{ index: true, path: "", element: <MainPage /> }],
  },
  {
    path: "/components",
    element: <ComponentsPage />,
  },
];

export default publicRoutes;
