import React from "react";
import { RouteObject } from "react-router-dom";
import GlobalLoader from "@components/GlobalLoader";
import AdminLayout from "../layouts/AdminLayout";
import ComponentsPage from "@pages/ComponentsPage";
import MainAdminPage from "@pages/Admin/MainAdminPage";
import DomansPage from "@pages/Admin/Games/DomansPage";
import DetailsPage from "@pages/Admin/Games/DomansPage/DetailsPage";

const adminRoutes: RouteObject[] = [
  {
    path: "adm",
    errorElement: <GlobalLoader message="Ooops..." />,
    element: <AdminLayout />,
    loader: () => {
      return true;
    },
    children: [
      { path: "", element: <MainAdminPage /> },
      { path: "main", element: <MainAdminPage /> },
      { path: "games/doman", element: <DomansPage /> },
      { path: "games/doman/:id", element: <DetailsPage /> },
    ],
  },
  {
    path: "/components",
    element: <ComponentsPage />,
  },
];

export default adminRoutes;
