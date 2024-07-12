import React from "react";
import { RouteObject } from "react-router-dom";
import GlobalLoader from "@components/GlobalLoader";
import AdminLayout from "../layouts/AdminLayout";
import ComponentsPage from "@pages/ComponentsPage";
import MainAdminPage from "@pages/Admin/MainAdminPage";
import DomansPage from "@pages/Admin/Games/DomansPage";

const adminRoutes: RouteObject[] = [
  {
    path: "adm",
    errorElement: <GlobalLoader message="Ooops..." />,
    element: <AdminLayout />,
    loader: () => {
      return true;
    },
    children: [
      { index: true, path: "", element: <MainAdminPage /> },
      { index: true, path: "main", element: <MainAdminPage /> },
      { index: true, path: "games/doman", element: <DomansPage /> },
    ],
  },
  {
    path: "/components",
    element: <ComponentsPage />,
  },
];

export default adminRoutes;
