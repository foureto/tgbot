import React from "react";
import {RouteObject} from "react-router-dom";

import DefaultLayout from "../layouts/DefaultLayout";
import MainPage from "../pages/MainPage";

const publicRoutes: RouteObject[] = [
  {
    path: "",
    element: <DefaultLayout/>,
    errorElement: "loading...",
    loader: ({request}) => {
      return true;
    },
    children: [
      {index: true, path: "", element: <MainPage/>},
    ],
  },
]

export default publicRoutes;