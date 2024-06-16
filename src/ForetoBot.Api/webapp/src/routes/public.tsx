import React from "react";
import { RouteObject } from "react-router-dom";

import DefaultLayout from "../layouts/DefaultLayout";
import MainPage from "@pages/Client/MainPage";
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
];

export default publicRoutes;
