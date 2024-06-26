import React from "react";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { createRoot } from "react-dom/client";
import publicRoutes from "./routes/public";
import adminRoutes from "./routes/admin";

import "./app.scss";

const container = document.getElementById("app");
// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
const root = createRoot(container!);

const router = createBrowserRouter([...publicRoutes, ...adminRoutes]);

root.render(<RouterProvider router={router} />);
